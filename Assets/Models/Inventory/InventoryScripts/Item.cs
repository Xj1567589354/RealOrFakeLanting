using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New Item")]
public class Item : ScriptableObject
{
    public string itemName;     //��Ʒ����
    public int itemNumber;       //��Ʒ����
    [TextArea(5, 10)]
    public string itemInfo;         //��Ʒ����
    public Texture itemImage;      //��Ʒ����
    public int Number;      //��Ʒ���

    public bool check;      //�鿴
    public bool merge;      //�ϲ�
    public bool sell;      //����
    public bool discard;      //����
}
