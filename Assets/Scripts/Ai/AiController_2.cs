using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// AI控制器--鹿熊猪
/// </summary>
public class AiController_2 : MonoBehaviour
{
    Animator animator;      //动物动画器
    NavMeshAgent agent;     //寻路代理  
    Transform PlayerHande;      //玩家
    float distance;     //距离
    public enum AnimalState
    {
        idle,       //休闲状态
        run,       //逃跑状态
    }

    public AnimalState CurrentState = AnimalState.idle;     //获取当前状态

    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        PlayerHande = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        distance = Vector3.Distance(PlayerHande.position, transform.position);      //计算目标与玩家之间的距离

        switch (CurrentState)
        {
            case AnimalState.idle:
                if (distance >= 2 && distance <= 5)
                {
                    CurrentState = AnimalState.run;     //距离在2到5之间目标执行逃跑状态
                }
                agent.isStopped = true;     //停止寻路
                animator.SetBool("IsRun", false);       //停止播放逃跑动画
                break;
            case AnimalState.run:
                animator.SetBool("IsRun", true);
                if (distance > 5 || distance < 2)
                {
                    CurrentState = AnimalState.idle;        //距离大于2或者大于5执行休闲状态
                }
                agent.isStopped = false;
                agent.SetDestination(PlayerHande.position + new Vector3(10, 0, 0));     //设置目标逃跑位置朝玩家xz
                break;
        }
    }
}
