using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


/// <summary>
/// 改变字体颜色
/// </summary>
public class ChangeTextColor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text text;
    Color color;
    private void Start()
    {
        color = text.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = Color.black;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = color;
    }


}
