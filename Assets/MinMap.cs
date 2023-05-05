using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 小地图
/// </summary>
public class MinMap : MonoBehaviour
{
    public Transform PlayerHandle;      //玩家位置
    public GameObject MinMapImage;      //小地图显示区域

    private void LateUpdate()
    {
        Vector3 newPosition = PlayerHandle.position;
        newPosition.y = transform.position.y;       //获取上一次摄像机y轴位置
        transform.position = newPosition;       //实时跟踪玩家位置

        transform.rotation = Quaternion.Euler(90f, PlayerHandle.eulerAngles.y, 0f);     //摄像机跟随玩家旋转

    }
}
