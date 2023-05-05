using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyButtons
{
    /*
        IsPressing--��ť���ڱ���
        OnPressed--��ť�ոձ�����
        OnReleased--��ť�ո����ͷ�
        IsExtending--�˳���չʱ��
        IsDelaying--�����ӳ�ʱ��
    */
    public bool IsPressing = false;
    public bool OnPressed = false;
    public bool OnReleased = false;
    public bool IsExtending = false;
    public bool IsDelaying = false;

    public float extendingDuration = 0.15f;
    public float delayingDuration =0.15f;
    /*
        CurState--��ǰ״̬
        LastState--��һ��״̬
     */
    private bool CurState = false;
    private bool LastState = false;

    private Timer extTimer = new Timer();
    private Timer delayTimer = new Timer();

    /// <summary>
    /// ��ť״̬�жϺ���
    /// </summary>
    /// <param name="Input">��ǰ��ť״̬</param>
    public void Tick(bool Input)
    {
        extTimer.Tick();
        delayTimer.Tick();

        CurState = Input;
        IsPressing = CurState;      //���õ�ǰ��ť����״̬

        /*
             �´η�������ʱ��
            OnPressed��OnReleased��ʼ��
         */
        OnPressed = false;
        OnReleased = false;
        IsExtending = false;
        IsDelaying = false;

        if (CurState != LastState)
        {
            //�ж��Ƿ�ť�ոհ���
            if (CurState == true)
            {
                OnPressed = true;
                StartTimer(delayTimer, delayingDuration);
            }
            //�ж��Ƿ�ť�ո��ͷ�
            else
            {
                OnReleased = true;
                StartTimer(extTimer, extendingDuration);     //��һ�ΰ�ť�ͷź󣬿�ʼ��ʱ��������IsExtendingΪTrue
            }
        }
        LastState = CurState;   //������һ��״̬
        
        if (extTimer.state == Timer.STATE.RUN)
        {
            IsExtending = true;
        }

        if (delayTimer.state == Timer.STATE.RUN)
        {
            IsDelaying = true;
        }


        /// <summary>
        /// ��ʼ��ʱ����
        /// </summary>
        /// <param name="timer">��ʱ��</param>
        /// <param name="duration">��ʱʱ��</param>
         void StartTimer(Timer timer, float duration)
        {
            timer.duration = duration;
            timer.GoTimer();
        }
    }
}
