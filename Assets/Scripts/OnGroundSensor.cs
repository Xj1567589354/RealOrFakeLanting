using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundSensor : MonoBehaviour
{
    public CapsuleCollider CapsuleCollider;         //胶囊碰撞器
    public float offset = 0.1f;
    private Vector3 point1;
    private Vector3 point2;
    private float radius;
    // Start is called before the first frame update
    private void Awake()
    {
        /*
         获取胶囊碰撞器的半径
         减小两圆半径
         */
        radius = CapsuleCollider.radius - 0.05f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
            /*
             point1---获取胶囊碰撞器底部球体的球心
             point2---获取胶囊碰撞器顶部球体的球心
             减去offset使两圆位置向下沉陷，避免触碰不到的问题
             */
        point1 = transform.position + transform.up * (radius-offset);
        point2 = transform.position + transform.up * (CapsuleCollider.height-offset) - transform.up * radius;

        //碰撞检测附带"Ground"层的物体 
        Collider[] outputCols = Physics.OverlapCapsule(point1, point2, radius,LayerMask.GetMask("Ground"));          
        if (outputCols.Length !=0)
        {
            SendMessageUpwards("IsGround");
        }
        else
        {
            SendMessageUpwards("IsNotGround");
        }
    }
}
