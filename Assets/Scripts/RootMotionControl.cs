using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotionControl : MonoBehaviour
{
    private Animator anim;
    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    
        // modifying root motion
    void OnAnimatorMove()   
    {
            /*
             (object) A---拆箱，将A转换成Object类型
             */
        SendMessageUpwards("OnUpdateRootMotion", (object)anim.deltaPosition);
    }
}
