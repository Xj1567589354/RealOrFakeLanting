using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    /*
     单例模式
     */
    public static InventoryManager instance;

    public Inventory playerBag;         //玩家背包
    public GameObject InventorySlotGrid;        //背包网格
    public Slot slotPrefab;         //网格子对象
    public Text InventorySlotInfo;      //物品描述
    public RawImage InventorySlotImage;     //物品图像
    public InventoryMenu Menu;
    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }
    private void Start()
    {
        instance.playerBag.itemList.Clear();        //初始化背包
        RefreshItem();
    }

    private void OnEnable()     //初始状态展示当前背包列表物品
    {

        //for (int i = 0; i < instance.playerBag.itemList.Count; i++)
        //{
        //    if (instance.playerBag.itemList[i] != null)     //如果不为空的话，背包图片描述展示初始化为背包列表第一个元素
        //    {
        //        instance.InventorySlotInfo.text = instance.playerBag.itemList[i].itemInfo;
        //        instance.InventorySlotImage.texture = instance.playerBag.itemList[i].itemImage;
        //        break;
        //    }
        //    else   //如果为空。背包图片描述展示显示为空白
        //    {
        instance.InventorySlotInfo.text = "";
        instance.InventorySlotImage.texture = null;
        //    }
        //}
    }

    /// <summary>
    /// 物品描述事件
    /// </summary>
    /// <param name="_ItemDescription">描述内容</param>
    public static void UpdateItemInfo(string _ItemDescription, Texture _ItemImage, int _Number)
    {
        instance.InventorySlotInfo.text = _ItemDescription;
        instance.InventorySlotImage.texture = _ItemImage;

        instance.Menu.SlotNumber = _Number;
    }

    /// <summary>
    /// 创建背包物品事件
    /// </summary>
    /// <param name="_item">物品</param>
    public static void CreateNewItem(Item _item)
    {
        Slot newItem = Instantiate(instance.slotPrefab, instance.InventorySlotGrid.transform.position, Quaternion.identity);        //实例化
        newItem.gameObject.transform.SetParent(instance.InventorySlotGrid.transform, false);       //设置新物品的父级
        newItem.slotItem = _item;
        newItem.slotName.text = _item.itemName;
        newItem.slotNumber.text = "×" + _item.itemNumber.ToString();
    }

    /// <summary>
    /// 更新背包事件
    /// </summary>
    public static void RefreshItem()
    {
        //清空网格所有子对象
        for (int i = 0; i < instance.InventorySlotGrid.transform.childCount; i++)
        {
            if (instance.InventorySlotGrid.transform.childCount == 0) break;
            Destroy(instance.InventorySlotGrid.transform.GetChild(i).gameObject);
        }

        //扫描背包列表并添加所有列表子对象
        for (int i = 0; i < instance.playerBag.itemList.Count; i++)
        {
            CreateNewItem(instance.playerBag.itemList[i]);
        }
    }
}
