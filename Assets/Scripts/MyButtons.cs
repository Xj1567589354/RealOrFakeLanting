using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyButtons
{
    /*
        IsPressing--按钮正在被按
        OnPressed--按钮刚刚被按下
        OnReleased--按钮刚刚呗释放
        IsExtending--退出扩展时间
        IsDelaying--进入延迟时间
    */
    public bool IsPressing = false;
    public bool OnPressed = false;
    public bool OnReleased = false;
    public bool IsExtending = false;
    public bool IsDelaying = false;

    public float extendingDuration = 0.15f;
    public float delayingDuration =0.15f;
    /*
        CurState--当前状态
        LastState--上一次状态
     */
    private bool CurState = false;
    private bool LastState = false;

    private Timer extTimer = new Timer();
    private Timer delayTimer = new Timer();

    /// <summary>
    /// 按钮状态判断函数
    /// </summary>
    /// <param name="Input">当前按钮状态</param>
    public void Tick(bool Input)
    {
        extTimer.Tick();
        delayTimer.Tick();

        CurState = Input;
        IsPressing = CurState;      //设置当前按钮运行状态

        /*
             下次方法调用时，
            OnPressed与OnReleased初始化
         */
        OnPressed = false;
        OnReleased = false;
        IsExtending = false;
        IsDelaying = false;

        if (CurState != LastState)
        {
            //判断是否按钮刚刚按下
            if (CurState == true)
            {
                OnPressed = true;
                StartTimer(delayTimer, delayingDuration);
            }
            //判断是否按钮刚刚释放
            else
            {
                OnReleased = true;
                StartTimer(extTimer, extendingDuration);     //第一次按钮释放后，开始计时，并设置IsExtending为True
            }
        }
        LastState = CurState;   //更新上一次状态
        
        if (extTimer.state == Timer.STATE.RUN)
        {
            IsExtending = true;
        }

        if (delayTimer.state == Timer.STATE.RUN)
        {
            IsDelaying = true;
        }


        /// <summary>
        /// 开始计时函数
        /// </summary>
        /// <param name="timer">计时器</param>
        /// <param name="duration">计时时间</param>
         void StartTimer(Timer timer, float duration)
        {
            timer.duration = duration;
            timer.GoTimer();
        }
    }
}
