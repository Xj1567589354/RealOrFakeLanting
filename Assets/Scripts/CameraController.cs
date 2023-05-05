using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CameraController : MonoBehaviour
{
    public float HorizontalSpeed = 20.0f;
    public float VerticalSpeed = 20.0f;
    public float CameraDampValue = 0.5f;
    public float RayDistance = 2.0f;
    public float tempEulerX;
    /*
     lockDot--������ͷ
     RayDot--���߷����ͷ
     */
    public Image lockDot;
    public Image RayDot;
    public Text TextHint;
    public Text TextKey_E;      //�ռ����߶Ի�
    public Text TextKey_T;      //����
    public Text Hint_Interact;      //��Ʒ������ʾ
    string dic_text;
    public bool Interact_State;
    private bool CameraState;
    public GameObject target;
    public bool Quit_Dlo;       //�˳��Ի�
    public GameObject PlayerModel;
    public GameObject DialogueBox;
    public TaskList TaskList;


    public InventoryMenu Menu;
    public float FishCount;           //��
    public float BeastCount;           //Ұ��


    public float ElapseTime;    //��ʱ
    Timer timer;        //��ʱ��

    Dictionary<string, string> dictionary;
    public bool LockState;
    public IUserInput Pi;
    private GameObject PlayerHandle;
    private GameObject CameraHandle;
    private GameObject Model;
    private new GameObject camera;
    private Vector3 cameraDampVelocity;
    [SerializeField]
    private LockTarget locktarget = null;

    public RawImage Half_Lanting;       //���š���ͤ��
    public RawImage LiHe_Letter;       //�������
    public RawImage Poes;                //����ʫ
    public RawImage False_Lanting;                //����ʫ
    public RawImage Lanting_1;      //����ͤ����Ƭ
    public RawImage Lanting_2;
    public RawImage Lanting_3;
    public RawImage Lanting_4;
    public Text ShowText;       //��Ʒ��ʾ
    public Text ShowText2;
    public Show show;
    KeyBoardInput KBI;       //��λ����
    public int ShowNumber = 0;        //��Ʒ���

    public GameObject BagSystem;        //����ϵͳ
    public bool isOpen;     //��ⱳ���Ƿ��
    public bool BagOpenState;     //��ⱳ���Ƿ��
    public ItemOnWorld Lanting_Fregment;
    public ItemOnWorld True_Lanting;

    /*
     Ŀ�������ȡ��Ƭ���
     */
    private bool HXZ_LantingState;
    private bool CM_LantingState;
    private bool BM_LantingState;

    public Other_Narrator Narrator_Success;
    public Other_Narrator Narrator_LookLanting;
    public TaskList taskList;

    public Show _Show;

    public GameObject SubMenu;      //���˵�
    public bool isOpenMenu;
    public Animator animator;
    public Animator animator_2;     //��ȶ�����
    public Animator animator_3;     //��������
    public Animator animator_4;     //�̷�������
    public Animator animator_5;     //Ŀ���߶�����
    public Animator animator_6;     //��̫�ڶ�����
    public Animator animator_7;     //�����Ӷ�����

    public GameObject BlurImage_SubMenu;     //���˵�����
    public GameObject BlurImage_ThingShow;     //��Ʒչʾ����
    public GameObject BlurImage_Bag;     //��������

    public MinMap MinMap;  //С��ͼ




    void Start()
    {
        KBI = FindObjectOfType<KeyBoardInput>();
        Quit_Dlo = false;
        CameraState = false;
        Menu.MoneyNumber = 0;

        HXZ_LantingState = true;
        CM_LantingState = true;
        BM_LantingState = true;

        FishCount = 0;
        BeastCount = 0;


        //�ֵ�洢��Ʒ��Ϣ
        dictionary = new Dictionary<string, string>();
        dictionary.Add("WaterBox", "ˮͰ");
        dictionary.Add("Handle", "����");
        dictionary.Add("Bowl", "�ֹ�");
        dictionary.Add("Bucket", "ľͰ");
        dictionary.Add("FatPot", "Բ���մ�");
        dictionary.Add("LongPot", "�����մ�");
        dictionary.Add("RoundMouthPot", "Բ���մ�");
        dictionary.Add("LongBasket", "��������");
        dictionary.Add("RoundBasket", "Բ������");
        dictionary.Add("Letter_Lanting", "���š���ͤ��");
        dictionary.Add("Letter_Poes", "һ��ʫ");
        dictionary.Add("Letter", "һ����");
        dictionary.Add("Lanting_Fragment", "����ͤ����Ƭ");
        dictionary.Add("Lanting", "����ͤ���漣");

        dictionary.Add("PeiDu", "���");
        dictionary.Add("CuiMiao", "���");
        dictionary.Add("JiaChang", "�ֲ�");
        dictionary.Add("Xiaoyi", "����");
        dictionary.Add("BusinessMan", "�̷�");
        dictionary.Add("LiHe", "���ʬ��");
        dictionary.Add("HanXiangZi", "������");
        dictionary.Add("ErGaZi", "������");
        dictionary.Add("Student", "��Ա");
        dictionary.Add("Monk_BianCai", "ɮ�˱��");
        dictionary.Add("SmallMonk_Achen", "����");
        dictionary.Add("EyeWitnesser", "Ŀ����");
        dictionary.Add("TangTaiZhong", "��̫��");
        dictionary.Add("WuYuanHeng", "��Ԫ��ʬ��");

        dictionary.Add("Rabbit", "Ұ��");
        dictionary.Add("Frog", "Ұ������");
        dictionary.Add("Crocodile", "����");
        dictionary.Add("Fish", "��");
        dictionary.Add("Deer", "Ұ��¹");
        dictionary.Add("Chicken", "��");
        dictionary.Add("Cattle", "ţ");
        dictionary.Add("Goose", "Ѽ");
        dictionary.Add("Boar", "Ұ��");
        dictionary.Add("Bear", "Ұ����");
        dictionary.Add("Salamander", "�ڻ�");
        dictionary.Add("Goat", "Ұ��ɽ��");
        dictionary.Add("Wolf", "�����");

        TextHint.enabled = false;
        TextKey_T.enabled = false;
        Hint_Interact.enabled = false;
        Interact_State = false;

        ElapseTime = 0;
        timer = new Timer();

        CameraHandle = transform.parent.gameObject; // ��һ��
        PlayerHandle = CameraHandle.transform.parent.gameObject;
        tempEulerX = 20.0f;
        ActorController ac = PlayerHandle.GetComponent<ActorController>();
        Model = ac.Model;
        Pi = ac.Pi;
        lockDot.enabled = false;
        RayDot.enabled = true;
        LockState = false;
        ///<summary>
        /// ��ȡ�������
        /// </summary>
        camera = Camera.main.gameObject;
    }
    private void FixedUpdate()
    {
        Vector3 ModelRay_pos = Model.transform.position + new Vector3(0, 1.5f, 0);
        Vector3 ModelRay_forward = Model.transform.TransformDirection(transform.forward) * 10.0f;
        RaycastHit hit;
        bool RayCol = Physics.Raycast(ModelRay_pos, transform.forward, out hit, RayDistance, LayerMask.GetMask("Enemy"));

        TextHint.enabled = false;
        TextKey_E.enabled = false;
        TextKey_T.enabled = false;
        if (RayCol == true)
        {
            foreach (string key in dictionary.Keys)
            {
                if (hit.collider.gameObject.name == key)
                {
                    TextHint.text = dictionary[key];
                    TextHint.enabled = true;
                    if (hit.collider.gameObject.tag.Contains("Things"))     //��Ʒ����      ����ӱ���ϵͳ������
                    {
                        target = hit.collider.gameObject;
                        print(target);
                        TextKey_E.text = "E) �ռ�";
                        TextKey_E.enabled = true;      //��̬��������
                        if (Interact_State)
                        {
                            //GetObject(hit.collider.gameObject);     //������Ʒ
                            Hint_Interact.text = dictionary[key] + "���ռ�";   //��̬������Ʒ��ʾ��Ϣ
                            Hint_Interact.enabled = true;
                            Interact_State = false;     //���ý���

                            Destroy(target);     //�ݻ�Ŀ����Ʒ
                            target.GetComponent<ItemOnWorld>().AddNewItem();       //��Ŀ����Ʒ��ӵ�����ϵͳ��

                            switch (hit.collider.gameObject.name)
                            {
                                case "Letter_Lanting":
                                    Half_Lanting.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);
                                    ShowText.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);
                                    ShowNumber = 1;

                                    KBI.mouseEnable = false;     //��������ͷ��ת
                                    KBI.KeyEnable = false;       //���ü�λ����

                                    BlurImage_ThingShow.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver); //��Ʒ������ʾ
                                    break;
                                case "Letter":
                                    TaskList.ElapseTime = 0;        //�����ʱ����
                                    TaskList.Count++;       //��ʾ��һ��������ʾ

                                    LiHe_Letter.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);
                                    ShowText.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);

                                    ShowNumber = 2;
                                    KBI.mouseEnable = false;     //��������ͷ��ת
                                    KBI.KeyEnable = false;       //���ü�λ����

                                    BlurImage_ThingShow.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);
                                    break;
                                case "Letter_Poes":
                                    TaskList.ElapseTime = 0;        //�����ʱ����
                                    TaskList.Count++;       //��ʾ��һ��������ʾ

                                    Poes.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);
                                    ShowText.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);

                                    ShowNumber = 3;
                                    KBI.mouseEnable = false;     //��������ͷ��ת
                                    KBI.KeyEnable = false;       //���ü�λ����

                                    BlurImage_ThingShow.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);
                                    break;
                                case "Lanting_Fragment":
                                    TaskList.ElapseTime = 0;        //�����ʱ����
                                    TaskList.Count++;       //��ʾ��һ��������ʾ

                                    Lanting_4.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);
                                    ShowText2.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);

                                    ShowNumber = 7;
                                    KBI.mouseEnable = false;     //��������ͷ��ת
                                    KBI.KeyEnable = false;       //���ü�λ����

                                    BlurImage_ThingShow.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);

                                    break;
                                case "Lanting":
                                    TaskList.ElapseTime = 0;        //�����ʱ����
                                    TaskList.Count++;       //��ʾ��һ��������ʾ

                                    False_Lanting.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);
                                    ShowText.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);

                                    ShowNumber = 25;     //�˳��鿴״̬
                                    KBI.mouseEnable = false;     //��������ͷ��ת
                                    KBI.KeyEnable = false;       //���ü�λ����

                                    BlurImage_ThingShow.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);

                                    break;
                                default:
                                    break;
                            }
                        }
                    }       //��Ʒ����
                    else if (hit.collider.gameObject.tag.Contains("NPC"))      //NPC����      ����һ��Boolֵ���жԻ�ϵͳ
                    {
                        target = hit.collider.gameObject;
                        TextKey_E.text = "E) �Ի�";
                        TextKey_E.enabled = true;      //��̬��������
                        if (hit.collider.gameObject.name == "BusinessMan")
                        {
                            TextKey_T.enabled = true;
                            if (Input.GetKeyDown("t") && Menu.MoneyNumber >= 50 && BM_LantingState)
                            {
                                Hint_Interact.text = "��á���ͤ����Ƭһö";
                                Hint_Interact.enabled = true;
                                //��Ƭ1���뵭��
                                Lanting_1.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);
                                ShowText2.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);

                                BlurImage_ThingShow.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver); //��Ʒ������ʾ

                                Menu.MoneyNumber -= 50;        //������50
                                Menu.MoneyShow.text = Menu.MoneyNumber.ToString();     //���µ�ǰ��������
                                ShowNumber = 4;

                                KBI.mouseEnable = false;     //��������ͷ��ת
                                KBI.KeyEnable = false;       //���ü�λ����

                                TaskList.ElapseTime = 0;        //�����ʱ����
                                TaskList.Count++;       //��ʾ��һ��������ʾ

                                Lanting_Fregment.AddNewItem();       //���̷�����ͤ����Ƭ��ӵ�����ϵͳ��
                                BM_LantingState = false;
                            }
                            else if (Input.GetKeyDown("t") && Menu.MoneyNumber < 30.0f && BM_LantingState)
                            {
                                if (Input.GetKeyDown("t"))
                                {
                                    Hint_Interact.text = "��������";
                                    Hint_Interact.enabled = true;
                                }
                            }
                            else if (!BM_LantingState && Input.GetKeyDown("t"))
                            {
                                Hint_Interact.text = "�̷�����Ƭ���Ѿ���ã�";
                                Hint_Interact.enabled = true;
                            }
                        }
                        else if (hit.collider.gameObject.name == "HanXiangZi")
                        {
                            TextKey_T.text = "T) ����";
                            TextKey_T.enabled = true;
                            if (Input.GetKeyDown("t") && HXZ_LantingState)
                            {
                                for (int i = 0; i < InventoryManager.instance.playerBag.itemList.Count; i++)
                                {
                                    if (InventoryManager.instance.playerBag.itemList[i].itemName == "����")
                                    {
                                        if (InventoryManager.instance.playerBag.itemList[i].itemNumber > 1)
                                        {
                                            InventoryManager.instance.playerBag.itemList[i].itemNumber -= 1;
                                        }
                                        else
                                        {
                                            InventoryManager.instance.playerBag.itemList.RemoveAt(i);
                                        }
                                        Hint_Interact.text = "��á���ͤ����Ƭһö";
                                        Hint_Interact.enabled = true;
                                        //��Ƭ2���뵭��
                                        Lanting_2.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);
                                        ShowText2.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);
                                        ShowNumber = 5;

                                        BlurImage_ThingShow.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);

                                        KBI.mouseEnable = false;     //��������ͷ��ת
                                        KBI.KeyEnable = false;       //���ü�λ����

                                        TaskList.ElapseTime = 0;        //�����ʱ����
                                        TaskList.Count++;       //��ʾ��һ��������ʾ

                                        Lanting_Fregment.AddNewItem();       //���̷�����ͤ����Ƭ��ӵ�����ϵͳ��

                                        InventoryManager.RefreshItem();     //ˢ�±���
                                        HXZ_LantingState = false;
                                        return;
                                    }
                                    else
                                    {
                                        Hint_Interact.text = "�޷����ף�";   //��̬������Ʒ��ʾ��Ϣ
                                        Hint_Interact.enabled = true;
                                    }
                                }
                            }
                            else if (!HXZ_LantingState && Input.GetKeyDown("t"))
                            {
                                Hint_Interact.text = "�����ӵ���Ƭ���Ѿ���ã�";
                                Hint_Interact.enabled = true;
                            }
                        }
                        else if (hit.collider.gameObject.name == "CuiMiao")
                        {
                            TextKey_T.text = "T) ����";
                            TextKey_T.enabled = true;
                            if (Input.GetKeyDown("t") && CM_LantingState)
                            {
                                for (int i = 0; i < InventoryManager.instance.playerBag.itemList.Count; i++)
                                {
                                    if (InventoryManager.instance.playerBag.itemList[i].itemName == "Ұ����")
                                    {
                                        if (InventoryManager.instance.playerBag.itemList[i].itemNumber > 1)
                                        {
                                            InventoryManager.instance.playerBag.itemList[i].itemNumber -= 1;
                                        }
                                        else
                                        {
                                            InventoryManager.instance.playerBag.itemList.RemoveAt(i);
                                        }

                                        Hint_Interact.text = "��á���ͤ����Ƭһö";
                                        Hint_Interact.enabled = true;
                                        //��Ƭ3���뵭��
                                        Lanting_3.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);
                                        ShowText2.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);
                                        ShowNumber = 6;

                                        BlurImage_ThingShow.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);

                                        KBI.mouseEnable = false;     //��������ͷ��ת
                                        KBI.KeyEnable = false;       //���ü�λ����

                                        TaskList.ElapseTime = 0;        //�����ʱ����
                                        TaskList.Count++;       //��ʾ��һ��������ʾ

                                        Lanting_Fregment.AddNewItem();       //���̷�����ͤ����Ƭ��ӵ�����ϵͳ��

                                        InventoryManager.RefreshItem();     //ˢ�±���
                                        CM_LantingState = false;
                                        return;
                                    }
                                    else
                                    {
                                        Hint_Interact.text = "�޷����ף�";   //��̬������Ʒ��ʾ��Ϣ
                                        Hint_Interact.enabled = true;
                                    }
                                }
                            }
                            else if (!CM_LantingState && Input.GetKeyDown("t"))
                            {
                                Hint_Interact.text = "������Ƭ���Ѿ���ã�";
                                Hint_Interact.enabled = true;
                            }
                        }
                        else if (hit.collider.gameObject.name == "Monk_BianCai")
                        {
                            TextKey_T.text = "T) �黹";
                            TextKey_T.enabled = true;
                            if (Input.GetKeyDown("t"))
                            {
                                for (int i = 0; i < InventoryManager.instance.playerBag.itemList.Count; i++)
                                {
                                    if (InventoryManager.instance.playerBag.itemList[i].itemName == "��ͤ���漣")
                                    {
                                        InventoryManager.instance.playerBag.itemList.RemoveAt(i);

                                        Hint_Interact.text = "�黹�ɹ���";   //��̬������Ʒ��ʾ��Ϣ
                                        Hint_Interact.enabled = true;
                                        InventoryManager.RefreshItem();     //ˢ�±���

                                        Narrator_Success.StartNarrator(Narrator_Success._Narrator);     //��ʼ��ʾ��Ϸ�����԰�
                                        BlurImage_ThingShow.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);       //�԰�������ʾ
                                    }
                                    else
                                    {
                                        Hint_Interact.text = "��û����ͤ���漣��";   //��̬������Ʒ��ʾ��Ϣ
                                        Hint_Interact.enabled = true;
                                    }
                                }
                            }
                        }

                        if (Interact_State)
                        {
                            target.GetComponent<DialogueTrigger>().TriggerDialogue();   //�����Ի�����
                            CameraState = true;
                            Interact_State = false;     //��ֹ�źŵ���
                        }
                    }       //NPC����
                }
            }
        }
    }
    void Update()
    {
        OpenBagSystem();        //��ⱳ��ϵͳ�Ƿ񱻴�
        OpenSubMenu();      //��⸱�˵��Ƿ񱻴�
        bool _Space = Input.GetKeyDown("space");

        switch (ShowNumber)
        {
            case 1:
                if (_Space)
                {
                    show.Fade(Half_Lanting);
                    BlurImage_ThingShow.gameObject.SendMessage("UIHide", SendMessageOptions.DontRequireReceiver); //������Ʒ����
                }
                break;
            case 2:
                if (_Space)
                {
                    show.Fade(LiHe_Letter);
                    BlurImage_ThingShow.gameObject.SendMessage("UIHide", SendMessageOptions.DontRequireReceiver);
                }
                break;
            case 3:
                if (_Space)
                {
                    show.Fade(Poes);
                    BlurImage_ThingShow.gameObject.SendMessage("UIHide", SendMessageOptions.DontRequireReceiver);
                }
                break;
            case 4:
                if (_Space)
                {
                    show.Fade(Lanting_1);
                    KBI.mouseEnable = true;     //��������ͷ��ת
                    KBI.KeyEnable = true;       //���ü�λ����

                    BlurImage_ThingShow.gameObject.SendMessage("UIHide", SendMessageOptions.DontRequireReceiver);
                }
                break;
            case 5:
                if (_Space)
                {
                    show.Fade(Lanting_2);
                    KBI.mouseEnable = true;     //��������ͷ��ת
                    KBI.KeyEnable = true;       //���ü�λ����

                    BlurImage_ThingShow.gameObject.SendMessage("UIHide", SendMessageOptions.DontRequireReceiver);
                }
                break;
            case 6:
                if (_Space)
                {
                    show.Fade(Lanting_3);
                    KBI.mouseEnable = true;     //��������ͷ��ת
                    KBI.KeyEnable = true;       //���ü�λ����

                    BlurImage_ThingShow.gameObject.SendMessage("UIHide", SendMessageOptions.DontRequireReceiver);
                }
                break;
            case 7:
                if (_Space)
                {
                    show.Fade(Lanting_4);

                    BlurImage_ThingShow.gameObject.SendMessage("UIHide", SendMessageOptions.DontRequireReceiver);
                }
                break;
            case 24:
                if (_Space)
                {
                    show.Fade(False_Lanting);
                    Narrator_LookLanting.StartNarrator(Narrator_LookLanting._Narrator);     //��ʼ��ʾ����ͤ�������԰�
                }
                break;
            case 25:
                if (_Space)
                {
                    show.Fade(False_Lanting);
                    BlurImage_ThingShow.gameObject.SendMessage("UIHide", SendMessageOptions.DontRequireReceiver);
                }
                break;
            default:
                break;
        }
        /*
         ִ�м�ʱ����3�����Ʒ��ʾ��ʧ
         */
        if (Hint_Interact.enabled == true)
        {
            ElapseTime += Time.deltaTime;
            //print(ElapseTime);
            timer.TimerJudge(ElapseTime, 3.0f);   //��ʱ1s
        }
        if (timer.EndState == false)
        {
            Hint_Interact.enabled = false;
            timer.EndState = true;
            ElapseTime = 0;     //��ʱ����
        }

        /*
         ��������
         */
        if (locktarget == null)
        {
            Vector3 tempModelEuler = Model.transform.eulerAngles;
            ///<summary>
            /// �ӽ�ˮƽ��ת 
            /// Rotate---��Ԫ��
            /// </summary>
            PlayerHandle.transform.Rotate(Vector3.up, Pi.Jright * HorizontalSpeed * Time.deltaTime);
            tempEulerX -= Pi.Jup * VerticalSpeed * Time.deltaTime;
            ///<summary>
            /// �ӽǴ�ֱ��ת
            /// localEulerAngles---ת�������ŷ����
            /// EulerAngles---ת���������ŷ����
            /// Clamp---����ŷ����
            /// </summary>
            tempEulerX = Mathf.Clamp(tempEulerX, -40, 60);
            CameraHandle.transform.localEulerAngles = new Vector3(tempEulerX, 0, 0);
            ///<summary>
            ///�ƶ��ӽ����������ת
            /// </summary>
            Model.transform.eulerAngles = tempModelEuler;
        }
        else      //�����������
        {
            //���˵���������ת������Ļ������ϵ��˰�߸���������ͷ
            lockDot.rectTransform.position = Camera.main.WorldToScreenPoint(locktarget.obj.transform.position + new Vector3(0, locktarget.halfHeight, 0));
            CameraHandle.transform.LookAt(locktarget.obj.transform);        //�������ע�ӵ��˵Ľ�
            float TrackDistance = Vector3.Distance(Model.transform.position, locktarget.obj.transform.position);    //�������ߵľ���
            if (TrackDistance > 10.0f)      //�����Ҿ������̫Զ�Զ�����
            {
                locktarget = null;
                lockDot.enabled = false;
                RayDot.enabled = true;
                LockState = false;
            }
            if (locktarget != null)
            {
                Vector3 tempForward = locktarget.obj.transform.position - Model.transform.position;
                tempForward.y = 0;
                PlayerHandle.transform.forward = tempForward;
            }
        }
        ///<summary>
        /// �����ʵʱ�������
        /// </summary>
        if (CameraState == false)
        {
            camera.transform.position = Vector3.SmoothDamp(camera.transform.position, transform.position, ref cameraDampVelocity,
                CameraDampValue * Time.deltaTime);
            //camera.transform.eulerAngles = transform.eulerAngles;
            camera.transform.LookAt(CameraHandle.transform.position);    //����������ֶ���
        }
        else      //�Ի�ϵͳ ���������NPC
        {
            MinMap.MinMapImage.SetActive(false);        //����С��ͼ
            camera.transform.position = Vector3.SmoothDamp(camera.transform.position, target.transform.position + new Vector3(0, 1, 0) + target.transform.forward * 1.4f, ref cameraDampVelocity,
              CameraDampValue * Time.deltaTime);
            camera.transform.LookAt(target.transform.position + new Vector3(0, 1, 0));    //����������ֶ���

            PlayerModel.SetActive(false);
            animator_2.SetBool("IsFocus", true);
            animator_3.SetBool("IsPray", true);
            animator_4.SetBool("IsBuy", true);
            animator_5.SetBool("IsSad", true);
            animator_6.SetBool("IsStretch", true);
            animator_7.SetBool("IsGestrue", true);

            TextHint.enabled = false;       //�������ｻ����Ϣ
            /*
             ����NPC��ʾ��Ϣ
             */
            TextKey_E.enabled = false;
            TextKey_T.enabled = false;
            if (_Space == true)     //�Ի�ϵͳ ������һ��Ի�����
            {
                CameraHandle.GetComponent<DialogueManager>().DisplayNextSentences();
                _Space = false;
            }

            if (Quit_Dlo)   //�˳��Ի�ϵͳ
            {
                MinMap.MinMapImage.SetActive(true);
                //DialogueBox.SetActive(false);       //���ضԻ���
                CameraState = false;
                Quit_Dlo = false;
                PlayerModel.SetActive(true);    //�������
                TextHint.enabled = true;        //�������ｻ����Ϣ
                TextKey_E.enabled = true;
                CameraHandle.GetComponent<DialogueManager>().animator.SetBool("IsOpen", false);

                TaskList.ElapseTime = 0;        //�����ʱ����
                TaskList.Count++;       //��ʾ��һ��������ʾ

                animator_2.SetBool("IsFocus", false);
                animator_3.SetBool("IsPray", false);
                animator_4.SetBool("IsBuy", false);
                animator_5.SetBool("IsSad", false);
                animator_6.SetBool("IsStretch", false);
                animator_7.SetBool("IsGestrue", false);
            }
        }
        if (!isOpen && _Show.StartGame == true)
        {
            Cursor.lockState = CursorLockMode.Locked;       //�������������
        }
    }

    /// <summary>
    /// ������������¼�
    /// </summary>
    public void LockUnLock()
    {
        /*
         ��������
         overlapbox����ʵ�����ǰ���ͷ�һ����ײ������
         */
        Vector3 ModelOrigin_one = Model.transform.position;
        Vector3 ModelOrigin_two = ModelOrigin_one + new Vector3(0, 1, 0);
        Vector3 BoxCenter = ModelOrigin_two + Model.transform.forward * 5.0f;
        Collider[] cols = Physics.OverlapBox(BoxCenter, new Vector3(0.5f, 0.5f, 5.0f), Model.transform.rotation, LayerMask.GetMask("Enemy"));
        if (cols.Length == 0)
        {
            locktarget = null;
            lockDot.enabled = false;
            RayDot.enabled = true;
            LockState = false;
        }
        else
        {
            foreach (var col in cols)
            {
                if (locktarget != null && locktarget.obj == col.gameObject)   //�ж������Ƿ�Ϊͬһ���󣬷������
                {
                    locktarget = null;
                    lockDot.enabled = false;
                    RayDot.enabled = true;
                    LockState = false;
                    break;
                }
                locktarget = new LockTarget(col.gameObject, col.bounds.extents.y);   //������˼���߶�
                lockDot.enabled = true;
                RayDot.enabled = false;
                LockState = true;
                break;
            }
        }
    }

    /// <summary>
    /// �Զ������˼���߶��¼���
    /// </summary>
    private class LockTarget
    {
        public GameObject obj;
        public float halfHeight;

        public LockTarget(GameObject _obj, float _halfHeight)
        {
            obj = _obj;
            halfHeight = _halfHeight;
        }
    }

    /// <summary>
    /// ���������¼�
    /// </summary>
    public void SetInteract()
    {
        if (TextHint.enabled == true)
        {
            Interact_State = true;       //�����״̬
        }
    }
    /// <summary>
    /// ��ȡ��Ʒ�¼�
    /// </summary>
    /// <param name="_obj">��Ʒ����</param>
    public void GetObject(GameObject _obj)
    {
        GameObject obj;
        obj = _obj;
        obj.SetActive(false);
    }
    /// <summary>
    /// ����ϵͳ�����¼�
    /// </summary>
    void OpenBagSystem()
    {
        if (Input.GetKeyDown("b"))
        {
            isOpen = !isOpen;
            BagSystem.SetActive(isOpen);
            KBI.mouseEnable = !isOpen;        //��������ͷ�ƶ�
            KBI.KeyEnable = !isOpen;          //���ü�λ����
            BlurImage_Bag.SetActive(isOpen);
            if (isOpen)
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = isOpen;
            }
        }
    }
    /// <summary>
    /// �򿪸��˵�
    /// </summary>
    public void OpenSubMenu()
    {
        if (Input.GetKeyDown("escape"))
        {
            isOpenMenu = !isOpenMenu;
            //SubMenu.SetActive(isOpenMenu);
            KBI.mouseEnable = !isOpenMenu;        //��������ͷ�ƶ�
            KBI.KeyEnable = !isOpenMenu;          //���ü�λ����
            BlurImage_SubMenu.SetActive(isOpenMenu);     //��ʾ��������
            if (isOpenMenu)     //��ʾ���
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = isOpenMenu;
                isOpen = true;
                animator.SetBool("isOpen", true);

            }
            else if (!isOpenMenu)
            {
                isOpen = false;
                animator.SetBool("isOpen", false);
            }
        }
    }
}
