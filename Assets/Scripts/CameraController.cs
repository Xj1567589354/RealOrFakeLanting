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
     lockDot--锁定箭头
     RayDot--射线发射箭头
     */
    public Image lockDot;
    public Image RayDot;
    public Text TextHint;
    public Text TextKey_E;      //收集或者对话
    public Text TextKey_T;      //购买
    public Text Hint_Interact;      //物品交互提示
    string dic_text;
    public bool Interact_State;
    private bool CameraState;
    public GameObject target;
    public bool Quit_Dlo;       //退出对话
    public GameObject PlayerModel;
    public GameObject DialogueBox;
    public TaskList TaskList;


    public InventoryMenu Menu;
    public float FishCount;           //鱼
    public float BeastCount;           //野兽


    public float ElapseTime;    //计时
    Timer timer;        //计时器

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

    public RawImage Half_Lanting;       //半张《兰亭序》
    public RawImage LiHe_Letter;       //李贺书信
    public RawImage Poes;                //五言诗
    public RawImage False_Lanting;                //五言诗
    public RawImage Lanting_1;      //《兰亭序》碎片
    public RawImage Lanting_2;
    public RawImage Lanting_3;
    public RawImage Lanting_4;
    public Text ShowText;       //物品提示
    public Text ShowText2;
    public Show show;
    KeyBoardInput KBI;       //键位控制
    public int ShowNumber = 0;        //物品序号

    public GameObject BagSystem;        //背包系统
    public bool isOpen;     //检测背包是否打开
    public bool BagOpenState;     //检测背包是否打开
    public ItemOnWorld Lanting_Fregment;
    public ItemOnWorld True_Lanting;

    /*
     目标人物获取碎片情况
     */
    private bool HXZ_LantingState;
    private bool CM_LantingState;
    private bool BM_LantingState;

    public Other_Narrator Narrator_Success;
    public Other_Narrator Narrator_LookLanting;
    public TaskList taskList;

    public Show _Show;

    public GameObject SubMenu;      //副菜单
    public bool isOpenMenu;
    public Animator animator;
    public Animator animator_2;     //裴度动画器
    public Animator animator_3;     //萧翼动画器
    public Animator animator_4;     //商贩动画器
    public Animator animator_5;     //目击者动画器
    public Animator animator_6;     //唐太宗动画器
    public Animator animator_7;     //韩湘子动画器

    public GameObject BlurImage_SubMenu;     //副菜单遮罩
    public GameObject BlurImage_ThingShow;     //物品展示遮罩
    public GameObject BlurImage_Bag;     //背包遮罩

    public MinMap MinMap;  //小地图




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


        //字典存储物品信息
        dictionary = new Dictionary<string, string>();
        dictionary.Add("WaterBox", "水桶");
        dictionary.Add("Handle", "敌人");
        dictionary.Add("Bowl", "钢锅");
        dictionary.Add("Bucket", "木桶");
        dictionary.Add("FatPot", "圆形陶瓷");
        dictionary.Add("LongPot", "长条陶瓷");
        dictionary.Add("RoundMouthPot", "圆嘴陶瓷");
        dictionary.Add("LongBasket", "方形竹篮");
        dictionary.Add("RoundBasket", "圆形竹篮");
        dictionary.Add("Letter_Lanting", "半张《兰亭序》");
        dictionary.Add("Letter_Poes", "一首诗");
        dictionary.Add("Letter", "一封信");
        dictionary.Add("Lanting_Fragment", "《兰亭序》碎片");
        dictionary.Add("Lanting", "《兰亭序》真迹");

        dictionary.Add("PeiDu", "裴度");
        dictionary.Add("CuiMiao", "崔淼");
        dictionary.Add("JiaChang", "贾昌");
        dictionary.Add("Xiaoyi", "萧翼");
        dictionary.Add("BusinessMan", "商贩");
        dictionary.Add("LiHe", "李贺尸体");
        dictionary.Add("HanXiangZi", "韩湘子");
        dictionary.Add("ErGaZi", "二嘎子");
        dictionary.Add("Student", "生员");
        dictionary.Add("Monk_BianCai", "僧人辩才");
        dictionary.Add("SmallMonk_Achen", "阿尘");
        dictionary.Add("EyeWitnesser", "目击者");
        dictionary.Add("TangTaiZhong", "唐太宗");
        dictionary.Add("WuYuanHeng", "武元衡尸体");

        dictionary.Add("Rabbit", "野兔");
        dictionary.Add("Frog", "野生青蛙");
        dictionary.Add("Crocodile", "鳄鱼");
        dictionary.Add("Fish", "鱼");
        dictionary.Add("Deer", "野生鹿");
        dictionary.Add("Chicken", "鸡");
        dictionary.Add("Cattle", "牛");
        dictionary.Add("Goose", "鸭");
        dictionary.Add("Boar", "野猪");
        dictionary.Add("Bear", "野生熊");
        dictionary.Add("Salamander", "壁虎");
        dictionary.Add("Goat", "野生山羊");
        dictionary.Add("Wolf", "大灰狼");

        TextHint.enabled = false;
        TextKey_T.enabled = false;
        Hint_Interact.enabled = false;
        Interact_State = false;

        ElapseTime = 0;
        timer = new Timer();

        CameraHandle = transform.parent.gameObject; // 上一级
        PlayerHandle = CameraHandle.transform.parent.gameObject;
        tempEulerX = 20.0f;
        ActorController ac = PlayerHandle.GetComponent<ActorController>();
        Model = ac.Model;
        Pi = ac.Pi;
        lockDot.enabled = false;
        RayDot.enabled = true;
        LockState = false;
        ///<summary>
        /// 获取主摄像机
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
                    if (hit.collider.gameObject.tag.Contains("Things"))     //物品交互      可添加背包系统！！！
                    {
                        target = hit.collider.gameObject;
                        print(target);
                        TextKey_E.text = "E) 收集";
                        TextKey_E.enabled = true;      //动态更新文字
                        if (Interact_State)
                        {
                            //GetObject(hit.collider.gameObject);     //隐藏物品
                            Hint_Interact.text = dictionary[key] + "已收集";   //动态更改物品提示信息
                            Hint_Interact.enabled = true;
                            Interact_State = false;     //禁用交互

                            Destroy(target);     //摧毁目标物品
                            target.GetComponent<ItemOnWorld>().AddNewItem();       //将目标物品添加到背包系统中

                            switch (hit.collider.gameObject.name)
                            {
                                case "Letter_Lanting":
                                    Half_Lanting.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);
                                    ShowText.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);
                                    ShowNumber = 1;

                                    KBI.mouseEnable = false;     //禁用摄像头旋转
                                    KBI.KeyEnable = false;       //禁用键位输入

                                    BlurImage_ThingShow.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver); //物品遮罩显示
                                    break;
                                case "Letter":
                                    TaskList.ElapseTime = 0;        //任务计时清零
                                    TaskList.Count++;       //显示下一步任务提示

                                    LiHe_Letter.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);
                                    ShowText.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);

                                    ShowNumber = 2;
                                    KBI.mouseEnable = false;     //禁用摄像头旋转
                                    KBI.KeyEnable = false;       //禁用键位输入

                                    BlurImage_ThingShow.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);
                                    break;
                                case "Letter_Poes":
                                    TaskList.ElapseTime = 0;        //任务计时清零
                                    TaskList.Count++;       //显示下一步任务提示

                                    Poes.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);
                                    ShowText.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);

                                    ShowNumber = 3;
                                    KBI.mouseEnable = false;     //禁用摄像头旋转
                                    KBI.KeyEnable = false;       //禁用键位输入

                                    BlurImage_ThingShow.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);
                                    break;
                                case "Lanting_Fragment":
                                    TaskList.ElapseTime = 0;        //任务计时清零
                                    TaskList.Count++;       //显示下一步任务提示

                                    Lanting_4.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);
                                    ShowText2.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);

                                    ShowNumber = 7;
                                    KBI.mouseEnable = false;     //禁用摄像头旋转
                                    KBI.KeyEnable = false;       //禁用键位输入

                                    BlurImage_ThingShow.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);

                                    break;
                                case "Lanting":
                                    TaskList.ElapseTime = 0;        //任务计时清零
                                    TaskList.Count++;       //显示下一步任务提示

                                    False_Lanting.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);
                                    ShowText.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);

                                    ShowNumber = 25;     //退出查看状态
                                    KBI.mouseEnable = false;     //禁用摄像头旋转
                                    KBI.KeyEnable = false;       //禁用键位输入

                                    BlurImage_ThingShow.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);

                                    break;
                                default:
                                    break;
                            }
                        }
                    }       //物品交互
                    else if (hit.collider.gameObject.tag.Contains("NPC"))      //NPC交互      定义一个Bool值进行对话系统
                    {
                        target = hit.collider.gameObject;
                        TextKey_E.text = "E) 对话";
                        TextKey_E.enabled = true;      //动态更新文字
                        if (hit.collider.gameObject.name == "BusinessMan")
                        {
                            TextKey_T.enabled = true;
                            if (Input.GetKeyDown("t") && Menu.MoneyNumber >= 50 && BM_LantingState)
                            {
                                Hint_Interact.text = "获得《兰亭序》碎片一枚";
                                Hint_Interact.enabled = true;
                                //碎片1淡入淡出
                                Lanting_1.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);
                                ShowText2.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);

                                BlurImage_ThingShow.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver); //物品遮罩显示

                                Menu.MoneyNumber -= 50;        //银两减50
                                Menu.MoneyShow.text = Menu.MoneyNumber.ToString();     //更新当前银两数量
                                ShowNumber = 4;

                                KBI.mouseEnable = false;     //禁用摄像头旋转
                                KBI.KeyEnable = false;       //禁用键位输入

                                TaskList.ElapseTime = 0;        //任务计时清零
                                TaskList.Count++;       //显示下一步任务提示

                                Lanting_Fregment.AddNewItem();       //将商贩的兰亭序碎片添加到背包系统中
                                BM_LantingState = false;
                            }
                            else if (Input.GetKeyDown("t") && Menu.MoneyNumber < 30.0f && BM_LantingState)
                            {
                                if (Input.GetKeyDown("t"))
                                {
                                    Hint_Interact.text = "银两不够";
                                    Hint_Interact.enabled = true;
                                }
                            }
                            else if (!BM_LantingState && Input.GetKeyDown("t"))
                            {
                                Hint_Interact.text = "商贩的碎片您已经获得！";
                                Hint_Interact.enabled = true;
                            }
                        }
                        else if (hit.collider.gameObject.name == "HanXiangZi")
                        {
                            TextKey_T.text = "T) 交换";
                            TextKey_T.enabled = true;
                            if (Input.GetKeyDown("t") && HXZ_LantingState)
                            {
                                for (int i = 0; i < InventoryManager.instance.playerBag.itemList.Count; i++)
                                {
                                    if (InventoryManager.instance.playerBag.itemList[i].itemName == "草鱼")
                                    {
                                        if (InventoryManager.instance.playerBag.itemList[i].itemNumber > 1)
                                        {
                                            InventoryManager.instance.playerBag.itemList[i].itemNumber -= 1;
                                        }
                                        else
                                        {
                                            InventoryManager.instance.playerBag.itemList.RemoveAt(i);
                                        }
                                        Hint_Interact.text = "获得《兰亭序》碎片一枚";
                                        Hint_Interact.enabled = true;
                                        //碎片2淡入淡出
                                        Lanting_2.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);
                                        ShowText2.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);
                                        ShowNumber = 5;

                                        BlurImage_ThingShow.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);

                                        KBI.mouseEnable = false;     //禁用摄像头旋转
                                        KBI.KeyEnable = false;       //禁用键位输入

                                        TaskList.ElapseTime = 0;        //任务计时清零
                                        TaskList.Count++;       //显示下一步任务提示

                                        Lanting_Fregment.AddNewItem();       //将商贩的兰亭序碎片添加到背包系统中

                                        InventoryManager.RefreshItem();     //刷新背包
                                        HXZ_LantingState = false;
                                        return;
                                    }
                                    else
                                    {
                                        Hint_Interact.text = "无法交易！";   //动态更改物品提示信息
                                        Hint_Interact.enabled = true;
                                    }
                                }
                            }
                            else if (!HXZ_LantingState && Input.GetKeyDown("t"))
                            {
                                Hint_Interact.text = "韩湘子的碎片您已经获得！";
                                Hint_Interact.enabled = true;
                            }
                        }
                        else if (hit.collider.gameObject.name == "CuiMiao")
                        {
                            TextKey_T.text = "T) 交换";
                            TextKey_T.enabled = true;
                            if (Input.GetKeyDown("t") && CM_LantingState)
                            {
                                for (int i = 0; i < InventoryManager.instance.playerBag.itemList.Count; i++)
                                {
                                    if (InventoryManager.instance.playerBag.itemList[i].itemName == "野生熊")
                                    {
                                        if (InventoryManager.instance.playerBag.itemList[i].itemNumber > 1)
                                        {
                                            InventoryManager.instance.playerBag.itemList[i].itemNumber -= 1;
                                        }
                                        else
                                        {
                                            InventoryManager.instance.playerBag.itemList.RemoveAt(i);
                                        }

                                        Hint_Interact.text = "获得《兰亭序》碎片一枚";
                                        Hint_Interact.enabled = true;
                                        //碎片3淡入淡出
                                        Lanting_3.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);
                                        ShowText2.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);
                                        ShowNumber = 6;

                                        BlurImage_ThingShow.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);

                                        KBI.mouseEnable = false;     //禁用摄像头旋转
                                        KBI.KeyEnable = false;       //禁用键位输入

                                        TaskList.ElapseTime = 0;        //任务计时清零
                                        TaskList.Count++;       //显示下一步任务提示

                                        Lanting_Fregment.AddNewItem();       //将商贩的兰亭序碎片添加到背包系统中

                                        InventoryManager.RefreshItem();     //刷新背包
                                        CM_LantingState = false;
                                        return;
                                    }
                                    else
                                    {
                                        Hint_Interact.text = "无法交易！";   //动态更改物品提示信息
                                        Hint_Interact.enabled = true;
                                    }
                                }
                            }
                            else if (!CM_LantingState && Input.GetKeyDown("t"))
                            {
                                Hint_Interact.text = "崔淼的碎片您已经获得！";
                                Hint_Interact.enabled = true;
                            }
                        }
                        else if (hit.collider.gameObject.name == "Monk_BianCai")
                        {
                            TextKey_T.text = "T) 归还";
                            TextKey_T.enabled = true;
                            if (Input.GetKeyDown("t"))
                            {
                                for (int i = 0; i < InventoryManager.instance.playerBag.itemList.Count; i++)
                                {
                                    if (InventoryManager.instance.playerBag.itemList[i].itemName == "兰亭序真迹")
                                    {
                                        InventoryManager.instance.playerBag.itemList.RemoveAt(i);

                                        Hint_Interact.text = "归还成功！";   //动态更改物品提示信息
                                        Hint_Interact.enabled = true;
                                        InventoryManager.RefreshItem();     //刷新背包

                                        Narrator_Success.StartNarrator(Narrator_Success._Narrator);     //开始显示游戏结束旁白
                                        BlurImage_ThingShow.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);       //旁白遮罩显示
                                    }
                                    else
                                    {
                                        Hint_Interact.text = "您没有兰亭序真迹！";   //动态更改物品提示信息
                                        Hint_Interact.enabled = true;
                                    }
                                }
                            }
                        }

                        if (Interact_State)
                        {
                            target.GetComponent<DialogueTrigger>().TriggerDialogue();   //触发对话内容
                            CameraState = true;
                            Interact_State = false;     //防止信号叠加
                        }
                    }       //NPC交互
                }
            }
        }
    }
    void Update()
    {
        OpenBagSystem();        //检测背包系统是否被打开
        OpenSubMenu();      //检测副菜单是否被打开
        bool _Space = Input.GetKeyDown("space");

        switch (ShowNumber)
        {
            case 1:
                if (_Space)
                {
                    show.Fade(Half_Lanting);
                    BlurImage_ThingShow.gameObject.SendMessage("UIHide", SendMessageOptions.DontRequireReceiver); //淡出物品遮罩
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
                    KBI.mouseEnable = true;     //启用摄像头旋转
                    KBI.KeyEnable = true;       //启用键位输入

                    BlurImage_ThingShow.gameObject.SendMessage("UIHide", SendMessageOptions.DontRequireReceiver);
                }
                break;
            case 5:
                if (_Space)
                {
                    show.Fade(Lanting_2);
                    KBI.mouseEnable = true;     //启用摄像头旋转
                    KBI.KeyEnable = true;       //启用键位输入

                    BlurImage_ThingShow.gameObject.SendMessage("UIHide", SendMessageOptions.DontRequireReceiver);
                }
                break;
            case 6:
                if (_Space)
                {
                    show.Fade(Lanting_3);
                    KBI.mouseEnable = true;     //启用摄像头旋转
                    KBI.KeyEnable = true;       //启用键位输入

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
                    Narrator_LookLanting.StartNarrator(Narrator_LookLanting._Narrator);     //开始显示真兰亭序线索旁白
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
         执行计时器，3秒后物品提示消失
         */
        if (Hint_Interact.enabled == true)
        {
            ElapseTime += Time.deltaTime;
            //print(ElapseTime);
            timer.TimerJudge(ElapseTime, 3.0f);   //计时1s
        }
        if (timer.EndState == false)
        {
            Hint_Interact.enabled = false;
            timer.EndState = true;
            ElapseTime = 0;     //计时归零
        }

        /*
         敌人锁定
         */
        if (locktarget == null)
        {
            Vector3 tempModelEuler = Model.transform.eulerAngles;
            ///<summary>
            /// 视角水平旋转 
            /// Rotate---四元数
            /// </summary>
            PlayerHandle.transform.Rotate(Vector3.up, Pi.Jright * HorizontalSpeed * Time.deltaTime);
            tempEulerX -= Pi.Jup * VerticalSpeed * Time.deltaTime;
            ///<summary>
            /// 视角垂直旋转
            /// localEulerAngles---转动本身的欧拉角
            /// EulerAngles---转动父对象的欧拉角
            /// Clamp---限制欧拉角
            /// </summary>
            tempEulerX = Mathf.Clamp(tempEulerX, -40, 60);
            CameraHandle.transform.localEulerAngles = new Vector3(tempEulerX, 0, 0);
            ///<summary>
            ///移动视角锁定玩家旋转
            /// </summary>
            Model.transform.eulerAngles = tempModelEuler;
        }
        else      //玩家锁定对象
        {
            //敌人的世界坐标转换成屏幕坐标加上敌人半高赋给锁定箭头
            lockDot.rectTransform.position = Camera.main.WorldToScreenPoint(locktarget.obj.transform.position + new Vector3(0, locktarget.halfHeight, 0));
            CameraHandle.transform.LookAt(locktarget.obj.transform);        //玩家视线注视敌人的脚
            float TrackDistance = Vector3.Distance(Model.transform.position, locktarget.obj.transform.position);    //计算两者的距离
            if (TrackDistance > 10.0f)      //如果玩家距离敌人太远自动解锁
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
        /// 摄像机实时跟踪玩家
        /// </summary>
        if (CameraState == false)
        {
            camera.transform.position = Vector3.SmoothDamp(camera.transform.position, transform.position, ref cameraDampVelocity,
                CameraDampValue * Time.deltaTime);
            //camera.transform.eulerAngles = transform.eulerAngles;
            camera.transform.LookAt(CameraHandle.transform.position);    //减少相机部分抖动
        }
        else      //对话系统 摄像机面向NPC
        {
            MinMap.MinMapImage.SetActive(false);        //隐藏小地图
            camera.transform.position = Vector3.SmoothDamp(camera.transform.position, target.transform.position + new Vector3(0, 1, 0) + target.transform.forward * 1.4f, ref cameraDampVelocity,
              CameraDampValue * Time.deltaTime);
            camera.transform.LookAt(target.transform.position + new Vector3(0, 1, 0));    //减少相机部分抖动

            PlayerModel.SetActive(false);
            animator_2.SetBool("IsFocus", true);
            animator_3.SetBool("IsPray", true);
            animator_4.SetBool("IsBuy", true);
            animator_5.SetBool("IsSad", true);
            animator_6.SetBool("IsStretch", true);
            animator_7.SetBool("IsGestrue", true);

            TextHint.enabled = false;       //隐藏人物交互信息
            /*
             隐藏NPC提示信息
             */
            TextKey_E.enabled = false;
            TextKey_T.enabled = false;
            if (_Space == true)     //对话系统 播放下一句对话内容
            {
                CameraHandle.GetComponent<DialogueManager>().DisplayNextSentences();
                _Space = false;
            }

            if (Quit_Dlo)   //退出对话系统
            {
                MinMap.MinMapImage.SetActive(true);
                //DialogueBox.SetActive(false);       //隐藏对话框
                CameraState = false;
                Quit_Dlo = false;
                PlayerModel.SetActive(true);    //隐藏玩家
                TextHint.enabled = true;        //开启人物交互信息
                TextKey_E.enabled = true;
                CameraHandle.GetComponent<DialogueManager>().animator.SetBool("IsOpen", false);

                TaskList.ElapseTime = 0;        //任务计时清零
                TaskList.Count++;       //显示下一步任务提示

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
            Cursor.lockState = CursorLockMode.Locked;       //锁定并隐藏鼠标
        }
    }

    /// <summary>
    /// 敌人锁定检测事件
    /// </summary>
    public void LockUnLock()
    {
        /*
         尝试锁定
         overlapbox——实现玩家前方释放一个碰撞检测盒子
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
                if (locktarget != null && locktarget.obj == col.gameObject)   //判断锁定是否为同一对象，否则解锁
                {
                    locktarget = null;
                    lockDot.enabled = false;
                    RayDot.enabled = true;
                    LockState = false;
                    break;
                }
                locktarget = new LockTarget(col.gameObject, col.bounds.extents.y);   //引入敌人及其高度
                lockDot.enabled = true;
                RayDot.enabled = false;
                LockState = true;
                break;
            }
        }
    }

    /// <summary>
    /// 自动检测敌人及其高度事件类
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
    /// 交互激活事件
    /// </summary>
    public void SetInteract()
    {
        if (TextHint.enabled == true)
        {
            Interact_State = true;       //激活交互状态
        }
    }
    /// <summary>
    /// 获取物品事件
    /// </summary>
    /// <param name="_obj">物品对象</param>
    public void GetObject(GameObject _obj)
    {
        GameObject obj;
        obj = _obj;
        obj.SetActive(false);
    }
    /// <summary>
    /// 背包系统启用事件
    /// </summary>
    void OpenBagSystem()
    {
        if (Input.GetKeyDown("b"))
        {
            isOpen = !isOpen;
            BagSystem.SetActive(isOpen);
            KBI.mouseEnable = !isOpen;        //禁用摄像头移动
            KBI.KeyEnable = !isOpen;          //禁用键位输入
            BlurImage_Bag.SetActive(isOpen);
            if (isOpen)
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = isOpen;
            }
        }
    }
    /// <summary>
    /// 打开副菜单
    /// </summary>
    public void OpenSubMenu()
    {
        if (Input.GetKeyDown("escape"))
        {
            isOpenMenu = !isOpenMenu;
            //SubMenu.SetActive(isOpenMenu);
            KBI.mouseEnable = !isOpenMenu;        //禁用摄像头移动
            KBI.KeyEnable = !isOpenMenu;          //禁用键位输入
            BlurImage_SubMenu.SetActive(isOpenMenu);     //显示画面遮罩
            if (isOpenMenu)     //显示鼠标
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
