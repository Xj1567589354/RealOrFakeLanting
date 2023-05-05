using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerController : MonoBehaviour
{
    private Animator anim;
    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void ResetTrigger(string triggername)
    {
        anim.ResetTrigger(triggername);
    }
}
