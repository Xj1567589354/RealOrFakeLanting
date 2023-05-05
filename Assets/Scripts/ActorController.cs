using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player.IP;


public class ActorController : MonoBehaviour
{
    public GameObject Model;
    public PlayerInertia Pi_Inertia;
    public CameraController camcon;
    public IUserInput Pi;    //自定义脚本
    public float WalkSpeed = 2.0f;      //定于速度解决动画与模型移动速度不一致问题
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

    [SerializeField]    //私有界面化
    private Animator anim;
    public bool lockplaner = false;
    public bool trackDirection = false;
    private bool CanAttack;        //能否攻击
    private CapsuleCollider col;
    private float LerpTarget;

    float AttackTime = 0;
    bool isAttackState;
    public AudioManager AM;


    /*
     Awake---当一个脚本实例被载入时被调用
     Start---仅在Update函数第一次被调用前调用
     */
    void Awake()
    {
        anim = Model.GetComponent<Animator>();
        IUserInput[] Inputs = GetComponents<IUserInput>();
        foreach (var input in Inputs)           //判断哪个键位输入脚本被勾选
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
                FindObjectOfType<AudioManager>().Play("攻击-3", true);
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
        if (Pi.Lockon)      //实现镜头锁定
        {
            camcon.LockUnLock();
        }
        if (Pi.Roll || rigid.velocity.magnitude > 9.0f)       //速度大于9实现翻滚
        {
            anim.SetTrigger("roll");
            CanAttack = false;
        }
        if (Pi.Jump)        //实现跳跃控制
        {
            anim.SetTrigger("jump");
            CanAttack = false;

            //FindObjectOfType<AudioManager>().Play("走路");
        }

        if (Pi.Attack && CheckState("Ground") && CanAttack)        //实现攻击控制
        {
            anim.SetTrigger("attack");
        }
        if (camcon.LockState == false)
        {
            /*
              插值实现walk缓动转向run
            dmap--数值，没有方向
             */
            anim.SetFloat("forward", Pi.dmap * Mathf.Lerp(anim.GetFloat("forward"), ((Pi.Run) ? 2.0f : 1.0f), 0.5f));
            anim.SetFloat("right", 0);
            if (Pi.dmap > 0.1f)
            {
                Model.transform.forward = Vector3.Slerp(Model.transform.forward, Pi.dvec, 0.2f);        //弧形插值实现缓动效果
            }

            //锁定玩家移动初始状态
            if (lockplaner == false)
            {
                planervec = Pi.dmap * Model.transform.forward * WalkSpeed * ((Pi.Run) ? runmutiplier : 1.0f);      //存储玩家模型移动大小与方向
            }
        }
        else   //锁定玩家始终面朝敌人
        {
            if (trackDirection == false)
            {
                Model.transform.forward = transform.forward;        //没有最终始终面向前方
            }
            else
            {
                Model.transform.forward = planervec.normalized;     //标准化，始终是单位向量
            }
            /*
             将全局坐标转换成局部坐标，相对于玩家来说
            localDvec--三维向量，有方向
            Dvec--三维向量，有方向
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
     * 物理引擎都要放在FixedUpdated里
     */
    private void FixedUpdate()
    {
        rigid.position += deltaPos;     //使动画按照原始根运动变换位置
        //赋予玩家模型刚体属性,以及刷新玩家出现的位置和速度方向
        rigid.velocity = new Vector3(planervec.x, rigid.velocity.y, planervec.z) + thrustvec;
        thrustvec = Vector3.zero;
        deltaPos = Vector3.zero;        //等待下次动画根运动累加
    }

    //判断层级当前状态
    private bool CheckState(string statename, string layername = "Base Layer")
    {
        int layerIndex = anim.GetLayerIndex(layername);
        bool result = anim.GetCurrentAnimatorStateInfo(layerIndex).IsName(statename);
        return result;
    }

    /* 
        接受FSM消息执行函数
     */
    public void OnJumpEnter()
    {
        Pi_Inertia.Inertia();
        thrustvec = new Vector3(0, jumpvelocity, 0);                //添加一个y分量上的力,向上
        trackDirection = true;

        FindObjectOfType<AudioManager>().Play("跳跃", true);
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

    //碰撞检测
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
        col.material = FractionZero;            //跳跃使摩擦力为零，减少摩擦力影响
        AM.isGround = false;
    }

    public void OnEnterFall()
    {
        Pi_Inertia.Inertia();
    }


    public void OnUpdateEnter()
    {
        thrustvec = Model.transform.forward * anim.GetFloat("jabvelocity");           //添加一个相对于向前矢量的向后矢量
    }

    /*
     分别对应一段攻击、二段攻击和三段攻击
     */
    public void OnEnterAttack1hA()
    {
        Pi.InputEnabled = false;
        LerpTarget = 1.0f;
        FindObjectOfType<AudioManager>().Play("攻击-1", true);

        FindObjectOfType<AudioManager>().Play("跑", false);
    }

    public void OnEnterAttack2hA()
    {
        Pi.InputEnabled = false;
        LerpTarget = 1.0f;
        FindObjectOfType<AudioManager>().Play("攻击-2", true);
    }

    public void OnEnterAttack3hA()
    {
        Pi.InputEnabled = false;
        LerpTarget = 1.0f;

        isAttackState = true;

    }

    public void OnAttack1hAUpdate()
    {
        thrustvec = Model.transform.forward * anim.GetFloat("attack1hAVelocity");       //给一段攻击添加一个向前移动向量,使玩家完成一段攻击能够向前移动
        //进入Attack层的权重缓动增加到1，使动画变得流畅
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
        //退出Attack层的权重缓动增加到1，使动画变得流畅
        float CurrentWeight = anim.GetLayerWeight(anim.GetLayerIndex("Attack"));
        CurrentWeight = Mathf.Lerp(CurrentWeight, LerpTarget, 0.3f);
        anim.SetLayerWeight(anim.GetLayerIndex("Attack"), CurrentWeight);
    }
    /// <summary>
    /// 动画自带根节点运动
    /// </summary>
    /// <param name="_deltaPos">运动距离</param>
    void OnUpdateRootMotion(object _deltaPos)
    {
        if (CheckState("Attack1hC", "Attack"))
        {
            deltaPos += 0.4f * deltaPos + 0.6f * (Vector3)_deltaPos;      //削弱第三段攻击的震动
        }
    }
}
