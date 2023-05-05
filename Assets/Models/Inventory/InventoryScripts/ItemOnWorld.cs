using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnWorld : MonoBehaviour
{
    public Item thisItem;
    public Inventory playerBag;
    public KeyBoardInput KBI;

    public void Start()
    {
        thisItem.itemNumber = 1;       //初始化状态物品数量为1
    }
    /// <summary>
    /// 添加事件
    /// </summary>
    public void AddNewItem()
    {

        if (!playerBag.itemList.Contains(thisItem))     //如果玩家背包没有目标物品
        {
            playerBag.itemList.Add(thisItem);       //添加目标物品到背包数据库里
        }
        else
        {
            thisItem.itemNumber += 1;       //物品数量自增一
        }

        InventoryManager.RefreshItem();     //更新背包物品
    }
}
