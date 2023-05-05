using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeBTextColor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text SlotName;
    public Text SlotNumber;
    public Color color;


    private void Start()
    {
        color = SlotName.color;    //保存物品列表文字初试颜色
        SlotName.color = color;
        SlotNumber.color = color;
    }
    private void Update()
    {

    }

    /// <summary>
    /// 鼠标渐入事件
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        /*
          物品列表文字变黑
          */
        SlotName.color = Color.black;
        SlotNumber.color = Color.black;
    }

    /// <summary>
    /// 鼠标渐出事件
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        /*
         物品列表文字恢复原来颜色
         */
        SlotName.color = color;
        SlotNumber.color = color;
    }
}
