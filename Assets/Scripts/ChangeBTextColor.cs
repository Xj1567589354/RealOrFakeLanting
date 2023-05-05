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
        color = SlotName.color;    //������Ʒ�б����ֳ�����ɫ
        SlotName.color = color;
        SlotNumber.color = color;
    }
    private void Update()
    {

    }

    /// <summary>
    /// ��꽥���¼�
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        /*
          ��Ʒ�б����ֱ��
          */
        SlotName.color = Color.black;
        SlotNumber.color = Color.black;
    }

    /// <summary>
    /// ��꽥���¼�
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        /*
         ��Ʒ�б����ָֻ�ԭ����ɫ
         */
        SlotName.color = color;
        SlotNumber.color = color;
    }
}
