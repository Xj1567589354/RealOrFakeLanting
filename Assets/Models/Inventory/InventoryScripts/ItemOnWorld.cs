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
        thisItem.itemNumber = 1;       //��ʼ��״̬��Ʒ����Ϊ1
    }
    /// <summary>
    /// ����¼�
    /// </summary>
    public void AddNewItem()
    {

        if (!playerBag.itemList.Contains(thisItem))     //�����ұ���û��Ŀ����Ʒ
        {
            playerBag.itemList.Add(thisItem);       //���Ŀ����Ʒ���������ݿ���
        }
        else
        {
            thisItem.itemNumber += 1;       //��Ʒ��������һ
        }

        InventoryManager.RefreshItem();     //���±�����Ʒ
    }
}
