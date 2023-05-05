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
            TaskList.ElapseTime = 0;        //任务计时清零
            TaskList.Count++;       //显示下一步任务提示
            Collider.enabled = false;
        }
    }
}
