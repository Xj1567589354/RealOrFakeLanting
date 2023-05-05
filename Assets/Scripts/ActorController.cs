using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player.IP;


public class ActorController : MonoBehaviour
{
    public GameObject Model;
    public PlayerInertia Pi_Inertia;
    public CameraController camcon;
    public IUserInput Pi;    //�Զ���ű�
    public float WalkSpeed = 2.0f;      //�����ٶȽ��������ģ���ƶ��ٶȲ�һ������
    public float jumpvelocity = 5.0f;
    public float rollvelocity = 3.0f;
    public float runmutiplier = 2.0f;

    private Vector3 planervec;
    private Vector3 thrustvec;
    private Vector3 deltaPos;
    private Rigidbody rigid;

    [Space(5)]
    [Header("=====  Fraction Settings   =====")]
    public PhysicMaterial FractionOne;
    public PhysicMaterial FractionZero;

    [SerializeField]    //˽�н��滯
    private Animator anim;
    public bool lockplaner = false;
    public bool trackDirection = false;
    private bool CanAttack;        //�ܷ񹥻�
    private CapsuleCollider col;
    private float LerpTarget;

    float AttackTime = 0;
    bool isAttackState;
    public AudioManager AM;


    /*
     Awake---��һ���ű�ʵ��������ʱ������
     Start---����Update������һ�α�����ǰ����
     */
    void Awake()
    {
        anim = Model.GetComponent<Animator>();
        IUserInput[] Inputs = GetComponents<IUserInput>();
        foreach (var input in Inputs)           //�ж��ĸ���λ����ű�����ѡ
        {
            if (input.enabled == true)
            {
                Pi = input;
                break;
            }
        }
        rigid = GetComponent<Rigidbody>();
        Pi_Inertia = GetComponent<PlayerInertia>();
        col = GetComponent<CapsuleCollider>();
    }


    void Update()
    {
        if (isAttackState)
        {
            AttackTime += Time.deltaTime;
            if (AttackTime >= 0.8f)
            {
                FindObjectOfType<AudioManager>().Play("����-3", true);
                AttackTime = 0;
                isAttackState = false;
            }
        }
        anim.SetBool("defense", Pi.Defense);

        if (Pi.Quit_Dialogue)
        {
            camcon.Quit_Dlo = true;
        }
        if (Pi.Interact)
        {
            camcon.SetInteract();
        }
        if (Pi.Lockon)      //ʵ�־�ͷ����
        {
            camcon.LockUnLock();
        }
        if (Pi.Roll || rigid.velocity.magnitude > 9.0f)       //�ٶȴ���9ʵ�ַ���
        {
            anim.SetTrigger("roll");
            CanAttack = false;
        }
        if (Pi.Jump)        //ʵ����Ծ����
        {
            anim.SetTrigger("jump");
            CanAttack = false;

            //FindObjectOfType<AudioManager>().Play("��·");
        }

        if (Pi.Attack && CheckState("Ground") && CanAttack)        //ʵ�ֹ�������
        {
            anim.SetTrigger("attack");
        }
        if (camcon.LockState == false)
        {
            /*
              ��ֵʵ��walk����ת��run
            dmap--��ֵ��û�з���
             */
            anim.SetFloat("forward", Pi.dmap * Mathf.Lerp(anim.GetFloat("forward"), ((Pi.Run) ? 2.0f : 1.0f), 0.5f));
            anim.SetFloat("right", 0);
            if (Pi.dmap > 0.1f)
            {
                Model.transform.forward = Vector3.Slerp(Model.transform.forward, Pi.dvec, 0.2f);        //���β�ֵʵ�ֻ���Ч��
            }

            //��������ƶ���ʼ״̬
            if (lockplaner == false)
            {
                planervec = Pi.dmap * Model.transform.forward * WalkSpeed * ((Pi.Run) ? runmutiplier : 1.0f);      //�洢���ģ���ƶ���С�뷽��
            }
        }
        else   //�������ʼ���泯����
        {
            if (trackDirection == false)
            {
                Model.transform.forward = transform.forward;        //û������ʼ������ǰ��
            }
            else
            {
                Model.transform.forward = planervec.normalized;     //��׼����ʼ���ǵ�λ����
            }
            /*
             ��ȫ������ת���ɾֲ����꣬����������˵
            localDvec--��ά�������з���
            Dvec--��ά�������з���
             */
            Vector3 localDvec = transform.InverseTransformVector(Pi.dvec);
            anim.SetFloat("forward", localDvec.z * ((Pi.Run) ? 2.0f : 1.0f));
            anim.SetFloat("right", localDvec.x * ((Pi.Run) ? 2.0f : 1.0f));
            if (lockplaner == false)
            {
                planervec = Pi.dvec * WalkSpeed * ((Pi.Run) ? runmutiplier : 1.0f);
            }
        }
    }


    /*
     * �������涼Ҫ����FixedUpdated��
     */
    private void FixedUpdate()
    {
        rigid.position += deltaPos;     //ʹ��������ԭʼ���˶��任λ��
        //�������ģ�͸�������,�Լ�ˢ����ҳ��ֵ�λ�ú��ٶȷ���
        rigid.velocity = new Vector3(planervec.x, rigid.velocity.y, planervec.z) + thrustvec;
        thrustvec = Vector3.zero;
        deltaPos = Vector3.zero;        //�ȴ��´ζ������˶��ۼ�
    }

    //�жϲ㼶��ǰ״̬
    private bool CheckState(string statename, string layername = "Base Layer")
    {
        int layerIndex = anim.GetLayerIndex(layername);
        bool result = anim.GetCurrentAnimatorStateInfo(layerIndex).IsName(statename);
        return result;
    }

    /* 
        ����FSM��Ϣִ�к���
     */
    public void OnJumpEnter()
    {
        Pi_Inertia.Inertia();
        thrustvec = new Vector3(0, jumpvelocity, 0);                //���һ��y�����ϵ���,����
        trackDirection = true;

        FindObjectOfType<AudioManager>().Play("��Ծ", true);
    }

    public void OnRollEnter()
    {
        Pi_Inertia.Inertia();
        thrustvec = new Vector3(0, rollvelocity, 0);
        trackDirection = true;
    }


    public void OnJabEnter()
    {
        Pi_Inertia.Inertia();
    }

    //��ײ���
    public void IsGround()
    {
        anim.SetBool("isground", true);
    }

    public void IsNotGround()
    {
        anim.SetBool("isground", false);
    }

    public void OnEnterGround()
    {
        Pi.InputEnabled = true;
        lockplaner = false;
        CanAttack = true;
        col.material = FractionOne;
        trackDirection = false;
        AM.isGround = true;
    }

    public void OnExitGround()
    {
        col.material = FractionZero;            //��ԾʹĦ����Ϊ�㣬����Ħ����Ӱ��
        AM.isGround = false;
    }

    public void OnEnterFall()
    {
        Pi_Inertia.Inertia();
    }


    public void OnUpdateEnter()
    {
        thrustvec = Model.transform.forward * anim.GetFloat("jabvelocity");           //���һ���������ǰʸ�������ʸ��
    }

    /*
     �ֱ��Ӧһ�ι��������ι��������ι���
     */
    public void OnEnterAttack1hA()
    {
        Pi.InputEnabled = false;
        LerpTarget = 1.0f;
        FindObjectOfType<AudioManager>().Play("����-1", true);

        FindObjectOfType<AudioManager>().Play("��", false);
    }

    public void OnEnterAttack2hA()
    {
        Pi.InputEnabled = false;
        LerpTarget = 1.0f;
        FindObjectOfType<AudioManager>().Play("����-2", true);
    }

    public void OnEnterAttack3hA()
    {
        Pi.InputEnabled = false;
        LerpTarget = 1.0f;

        isAttackState = true;

    }

    public void OnAttack1hAUpdate()
    {
        thrustvec = Model.transform.forward * anim.GetFloat("attack1hAVelocity");       //��һ�ι������һ����ǰ�ƶ�����,ʹ������һ�ι����ܹ���ǰ�ƶ�
        //����Attack���Ȩ�ػ������ӵ�1��ʹ�����������
        float CurrentWeight = anim.GetLayerWeight(anim.GetLayerIndex("Attack"));
        CurrentWeight = Mathf.Lerp(CurrentWeight, LerpTarget, 0.3f);
        anim.SetLayerWeight(anim.GetLayerIndex("Attack"), CurrentWeight);
    }

    public void OnAttackIdleEnter()
    {
        Pi.InputEnabled = true;
        LerpTarget = 0;
    }

    void OnAttackIdleUpdate()
    {
        //�˳�Attack���Ȩ�ػ������ӵ�1��ʹ�����������
        float CurrentWeight = anim.GetLayerWeight(anim.GetLayerIndex("Attack"));
        CurrentWeight = Mathf.Lerp(CurrentWeight, LerpTarget, 0.3f);
        anim.SetLayerWeight(anim.GetLayerIndex("Attack"), CurrentWeight);
    }
    /// <summary>
    /// �����Դ����ڵ��˶�
    /// </summary>
    /// <param name="_deltaPos">�˶�����</param>
    void OnUpdateRootMotion(object _deltaPos)
    {
        if (CheckState("Attack1hC", "Attack"))
        {
            deltaPos += 0.4f * deltaPos + 0.6f * (Vector3)_deltaPos;      //���������ι�������
        }
    }
}
