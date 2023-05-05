using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// AI������--¹����
/// </summary>
public class AiController_2 : MonoBehaviour
{
    Animator animator;      //���ﶯ����
    NavMeshAgent agent;     //Ѱ·����  
    Transform PlayerHande;      //���
    float distance;     //����
    public enum AnimalState
    {
        idle,       //����״̬
        run,       //����״̬
    }

    public AnimalState CurrentState = AnimalState.idle;     //��ȡ��ǰ״̬

    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        PlayerHande = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        distance = Vector3.Distance(PlayerHande.position, transform.position);      //����Ŀ�������֮��ľ���

        switch (CurrentState)
        {
            case AnimalState.idle:
                if (distance >= 2 && distance <= 5)
                {
                    CurrentState = AnimalState.run;     //������2��5֮��Ŀ��ִ������״̬
                }
                agent.isStopped = true;     //ֹͣѰ·
                animator.SetBool("IsRun", false);       //ֹͣ�������ܶ���
                break;
            case AnimalState.run:
                animator.SetBool("IsRun", true);
                if (distance > 5 || distance < 2)
                {
                    CurrentState = AnimalState.idle;        //�������2���ߴ���5ִ������״̬
                }
                agent.isStopped = false;
                agent.SetDestination(PlayerHande.position + new Vector3(10, 0, 0));     //����Ŀ������λ�ó����xz
                break;
        }
    }
}
