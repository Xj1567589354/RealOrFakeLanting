using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenu : MonoBehaviour
{
    public static InventoryMenu instance;
    public CameraController camcon;
    public int SlotNumber;
    public int MoneyNumber;      //银两
    public Text MoneyShow;
    public Item item;       //当前物品
    public ItemOnWorld RightHalfLanting;        //右半边兰亭序
    public ItemOnWorld False_Lanting;     //假兰亭序
    public KeyBoardInput KBI;
    public GameObject BlurImage_Bag;

    private void Awake()
    {
        if (instance != null) Destroy(this);
        instance = this;
    }
    private void Start()
    {
        MoneyNumber = 20;
        item = new Item();

        MoneyShow.text = MoneyNumber.ToString();
    }
    public void Update()
    {
        print(SlotNumber);
        if (Input.GetKeyDown("v"))      //查看当前物品
        {
            if (item.check)
            {

                ItemCheck();
                camcon.isOpen = false;      //隐藏背包
                camcon.BagSystem.SetActive(camcon.isOpen);

                camcon.ShowNumber = SlotNumber;  //退出查看状态

                camcon.BlurImage_Bag.SetActive(false);      //背包遮罩消失
                camcon.BlurImage_ThingShow.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver); //物品遮罩显示
            }
            else
            {
                camcon.Hint_Interact.text = "此物品不可查看！";   //动态更改物品提示信息
                camcon.Hint_Interact.enabled = true;
            }
        }
        else if (Input.GetKeyDown("g"))         //出售当前物品
        {
            if (item.sell)
            {
                ItemSell();
                MoneyShow.text = MoneyNumber.ToString();     //更新当前银两数量
                camcon.Hint_Interact.text = item.itemName + "出售成功！";   //动态更改物品提示信息
                camcon.Hint_Interact.enabled = true;
            }
            else
            {
                camcon.Hint_Interact.text = "此物品不可出售！";   //动态更改物品提示信息
                camcon.Hint_Interact.enabled = true;
            }
        }
        else if (Input.GetKeyDown("r"))         //丢弃当前物品
        {
            if (item.discard)
            {
                ItemDiscard();
                camcon.Hint_Interact.text = item.itemName + "已丢弃！";   //动态更改物品提示信息
                camcon.Hint_Interact.enabled = true;
                InventoryManager.instance.InventorySlotInfo.text = "";       //文本描述为空
                InventoryManager.instance.InventorySlotImage.texture = null;        //图片展示为空
            }
            else
            {
                camcon.Hint_Interact.text = "此物品不可丢弃！";   //动态更改物品提示信息
                camcon.Hint_Interact.enabled = true;
            }
        }
        else if (Input.GetKeyDown("h"))     //合并当前物品
        {
            if (item.merge)
            {
                ItemMerge();
            }
            else
            {
                camcon.Hint_Interact.text = "此物品不可合并！";   //动态更改物品提示信息
                camcon.Hint_Interact.enabled = true;
            }
        }
    }
    /// <summary>
    /// 物品查看事件
    /// </summary>
    public void ItemCheck()
    {
        switch (SlotNumber)
        {
            case 1:
                camcon.Half_Lanting.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);
                camcon.ShowText.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);
                break;
            case 2:
                camcon.LiHe_Letter.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);
                camcon.ShowText.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);
                break;
            case 3:
                camcon.Poes.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);
                camcon.ShowText.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);
                break;
            case 24:        //查看假兰亭序
                camcon.False_Lanting.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);
                camcon.ShowText.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);

                camcon.ShowNumber = 24;     //退出查看状态
                break;
            case 25:        //查看真兰亭序
                camcon.False_Lanting.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);
                camcon.ShowText.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);

                camcon.ShowNumber = 25;     //退出查看状态

                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 物品出售事件
    /// </summary>
    public void ItemSell()
    {
        for (int i = 0; i < InventoryManager.instance.playerBag.itemList.Count; i++)
        {
            if (SlotNumber == InventoryManager.instance.playerBag.itemList[i].Number)
            {
                if (item.itemNumber > 1)        //物品数量大于1则自减1
                {
                    item.itemNumber -= 1;
                }
                else  //物品数量等于1则销毁背包系统当前物品
                {
                    InventoryManager.instance.playerBag.itemList.RemoveAt(i);
                }
            }
        }
        InventoryManager.RefreshItem();     //刷新背包
        MoneyNumber += 5;        //银两自增5

    }
    /// <summary>
    /// 物品合并事件
    /// </summary>
    public void ItemMerge()
    {
        for (int i = 0; i < InventoryManager.instance.playerBag.itemList.Count; i++)
        {
            if (SlotNumber == InventoryManager.instance.playerBag.itemList[i].Number)
            {
                if (item.itemNumber == 4)
                {
                    InventoryManager.instance.playerBag.itemList.RemoveAt(i);
                    RightHalfLanting.AddNewItem();

                    camcon.Hint_Interact.text = "获得半张兰亭序字迹";   //动态更改物品提示信息
                    camcon.Hint_Interact.enabled = true;
                    return;
                }
                else if (item.itemNumber == 2)
                {
                    InventoryManager.instance.playerBag.itemList.RemoveAt(i);
                    False_Lanting.AddNewItem();

                    camcon.Hint_Interact.text = "获得兰亭序临摹版";   //动态更改物品提示信息
                    camcon.Hint_Interact.enabled = true;

                    camcon.TaskList.ElapseTime = 0;        //任务计时清零
                    camcon.TaskList.Count++;       //显示下一步任务提示

                    camcon.BagSystem.SetActive(false);      //隐藏背包

                    BlurImage_Bag.SetActive(false);     //背景虚化关闭
                    camcon.isOpen = false;          //防止打开背包出问题

                    KBI.mouseEnable = true;        //启用摄像头移动
                    KBI.KeyEnable = true;          //启用键位输入



                    Cursor.lockState = CursorLockMode.Confined;     //开启鼠标显示
                    Cursor.visible = true; ;
                    return;
                }
                else
                {
                    camcon.Hint_Interact.text = "数量不够！";   //动态更改物品提示信息
                    camcon.Hint_Interact.enabled = true;
                }
            }
        }
        InventoryManager.RefreshItem();     //刷新背包
    }
    /// <summary>
    /// 物品丢弃事件
    /// </summary>
    public void ItemDiscard()
    {
        for (int i = 0; i < InventoryManager.instance.playerBag.itemList.Count; i++)
        {
            if (SlotNumber == InventoryManager.instance.playerBag.itemList[i].Number)
            {
                InventoryManager.instance.playerBag.itemList.RemoveAt(i);
            }
        }
        InventoryManager.RefreshItem();     //刷新背包
    }
}
