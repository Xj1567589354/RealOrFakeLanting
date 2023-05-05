using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


/// <summary>
/// MainMenuʵ��ͼ�굼��
/// </summary>
public class BTChangeText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RawImage Arraw;
    private void Start()
    {
        Arraw.enabled = false;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Arraw.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Arraw.enabled = false;
    }
}
