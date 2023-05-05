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
       duration--计时间隔时间
       elapseTimer--当前流逝时间
     */
    public float duration = 1.0f;
    public float elapseTimer = 0;
    public STATE state;
    //时间状态
    public bool EndState = true;
    public bool StartState = true;

    /// <summary>
    /// 计时器
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
    /// 计时启动函数
    /// </summary>
    public void GoTimer()
    {
        elapseTimer = 0;
        state = STATE.RUN;
    }

    /// <summary>
    /// 计时器
    /// </summary>
    /// <param name="_elpasetime">实时时间</param>
    /// <param name="duration">持续时间</param>
    public void TimerJudge(float _elpasetime, float duration)
    {
        float elapsetime = 0;
        elapsetime += _elpasetime;
        if (_elpasetime >= duration)
        {
            /*
             EndState--结束状态一
             StartState--开始状态
             */
            EndState = false;
            StartState = false;
        }
    }
}
