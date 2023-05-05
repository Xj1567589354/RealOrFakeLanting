using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{

    public enum STATE
    {
        IDLE,
        RUN,
        FINISHED
    }
    /*
       duration--��ʱ���ʱ��
       elapseTimer--��ǰ����ʱ��
     */
    public float duration = 1.0f;
    public float elapseTimer = 0;
    public STATE state;
    //ʱ��״̬
    public bool EndState = true;
    public bool StartState = true;

    /// <summary>
    /// ��ʱ��
    /// </summary>
    public void Tick()
    {
        switch (state)
        {
            case STATE.IDLE:
                break;
            case STATE.RUN:
                elapseTimer += Time.deltaTime;
                if (elapseTimer >= duration)
                {
                    state = STATE.FINISHED;
                }
                break;
            case STATE.FINISHED:
                break;
            default:
                Debug.Log("Timer error");
                break;
        }
    }

    /// <summary>
    /// ��ʱ��������
    /// </summary>
    public void GoTimer()
    {
        elapseTimer = 0;
        state = STATE.RUN;
    }

    /// <summary>
    /// ��ʱ��
    /// </summary>
    /// <param name="_elpasetime">ʵʱʱ��</param>
    /// <param name="duration">����ʱ��</param>
    public void TimerJudge(float _elpasetime, float duration)
    {
        float elapsetime = 0;
        elapsetime += _elpasetime;
        if (_elpasetime >= duration)
        {
            /*
             EndState--����״̬һ
             StartState--��ʼ״̬
             */
            EndState = false;
            StartState = false;
        }
    }
}
