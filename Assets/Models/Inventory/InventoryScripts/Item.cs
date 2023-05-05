using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New Item")]
public class Item : ScriptableObject
{
    public string itemName;     //物品名称
    public int itemNumber;       //物品数量
    [TextArea(5, 10)]
    public string itemInfo;         //物品描述
    public Texture itemImage;      //物品纹理
    public int Number;      //物品编号

    public bool check;      //查看
    public bool merge;      //合并
    public bool sell;      //出售
    public bool discard;      //出售
}
