using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item slotItem;       //预制体
    public Text slotName;       //预制体物品名称
    public Text slotNumber;     //预制体物品数量
    public bool itemSell;
    public bool itemCheck;

    /// <summary>
    /// 背包物品单击事件
    /// </summary>
    public void ItemOnClicked()
    {
        InventoryManager.UpdateItemInfo(slotItem.itemInfo, slotItem.itemImage, slotItem.Number);     //显示描述内容和图片
        InventoryMenu.instance.item = slotItem;     //获取当前物品
    }
}
