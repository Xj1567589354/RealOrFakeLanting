using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMOnEnter : StateMachineBehaviour
{
    public string[] onEnterMessages;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (var mes in onEnterMessages)
        {
            /*
             * SendMessageUpwards---向父物体(上一级)传消息
             * SendMessage---向自己传消息
             */
            animator.gameObject.SendMessageUpwards(mes);
            //animator.gameObject.SendMessage(mes);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    foreach (var mes in onExitMessages)
    //    {
    //        animator.gameObject.SendMessageUpwards(mes);
    //    }
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
