using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tasks_AutoCount : MonoBehaviour
{
    public TaskList TaskList;
    public GameObject PlayerHandle;
    public Collider Collider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == PlayerHandle)
        {
            TaskList.ElapseTime = 0;        //�����ʱ����
            TaskList.Count++;       //��ʾ��һ��������ʾ
            Collider.enabled = false;
        }
    }
}
