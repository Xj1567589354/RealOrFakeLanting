using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item slotItem;       //Ԥ����
    public Text slotName;       //Ԥ������Ʒ����
    public Text slotNumber;     //Ԥ������Ʒ����
    public bool itemSell;
    public bool itemCheck;

    /// <summary>
    /// ������Ʒ�����¼�
    /// </summary>
    public void ItemOnClicked()
    {
        InventoryManager.UpdateItemInfo(slotItem.itemInfo, slotItem.itemImage, slotItem.Number);     //��ʾ�������ݺ�ͼƬ
        InventoryMenu.instance.item = slotItem;     //��ȡ��ǰ��Ʒ
    }
}
