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
    /// 自定义左臂动画旋转函数
    /// </summary>
    /// <param name="layerIndex">动画层级索引值</param>
    private void OnAnimatorIK(int layerIndex)
    {
        if (anim.GetBool("defense") == false)
        {
            Transform leftLowerArm = anim.GetBoneTransform(HumanBodyBones.LeftLowerArm);        //获取左臂骨头形变量
            leftLowerArm.localEulerAngles += 0.75f * EulerVariable;
            /*
            设置对象骨头旋转量
            Quaternion.Euler()---转换成四元数
             */
            anim.SetBoneLocalRotation(HumanBodyBones.LeftLowerArm, Quaternion.Euler(leftLowerArm.localEulerAngles));
        }
    }
}
