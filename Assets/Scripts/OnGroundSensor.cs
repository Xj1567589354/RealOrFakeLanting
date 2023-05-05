using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundSensor : MonoBehaviour
{
    public CapsuleCollider CapsuleCollider;         //������ײ��
    public float offset = 0.1f;
    private Vector3 point1;
    private Vector3 point2;
    private float radius;
    // Start is called before the first frame update
    private void Awake()
    {
        /*
         ��ȡ������ײ���İ뾶
         ��С��Բ�뾶
         */
        radius = CapsuleCollider.radius - 0.05f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
            /*
             point1---��ȡ������ײ���ײ����������
             point2---��ȡ������ײ���������������
             ��ȥoffsetʹ��Բλ�����³��ݣ����ⴥ������������
             */
        point1 = transform.position + transform.up * (radius-offset);
        point2 = transform.position + transform.up * (CapsuleCollider.height-offset) - transform.up * radius;

        //��ײ��⸽��"Ground"������� 
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
