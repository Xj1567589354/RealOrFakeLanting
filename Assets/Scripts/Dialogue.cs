using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string Name;     //NPC����

    [TextArea(10, 20)]
    public string[] Sentences;  //�ַ�������洢����
}
