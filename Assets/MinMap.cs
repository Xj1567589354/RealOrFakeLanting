using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// С��ͼ
/// </summary>
public class MinMap : MonoBehaviour
{
    public Transform PlayerHandle;      //���λ��
    public GameObject MinMapImage;      //С��ͼ��ʾ����

    private void LateUpdate()
    {
        Vector3 newPosition = PlayerHandle.position;
        newPosition.y = transform.position.y;       //��ȡ��һ�������y��λ��
        transform.position = newPosition;       //ʵʱ�������λ��

        transform.rotation = Quaternion.Euler(90f, PlayerHandle.eulerAngles.y, 0f);     //��������������ת

    }
}
