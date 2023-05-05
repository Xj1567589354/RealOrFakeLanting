using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftArmAnimFix : MonoBehaviour
{
    private Animator anim;
    public Vector3 EulerVariable;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    /// <summary>
    /// �Զ�����۶�����ת����
    /// </summary>
    /// <param name="layerIndex">�����㼶����ֵ</param>
    private void OnAnimatorIK(int layerIndex)
    {
        if (anim.GetBool("defense") == false)
        {
            Transform leftLowerArm = anim.GetBoneTransform(HumanBodyBones.LeftLowerArm);        //��ȡ��۹�ͷ�α���
            leftLowerArm.localEulerAngles += 0.75f * EulerVariable;
            /*
            ���ö����ͷ��ת��
            Quaternion.Euler()---ת������Ԫ��
             */
            anim.SetBoneLocalRotation(HumanBodyBones.LeftLowerArm, Quaternion.Euler(leftLowerArm.localEulerAngles));
        }
    }
}
