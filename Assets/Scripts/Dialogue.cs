using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string Name;     //NPC名字

    [TextArea(10, 20)]
    public string[] Sentences;  //字符串数组存储内容
}
