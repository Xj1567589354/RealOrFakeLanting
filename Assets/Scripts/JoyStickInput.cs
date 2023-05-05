using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStickInput : IUserInput
{
    [Header("=====  Joy Signals  =====")]
    public string axisX = "axisX";
    public string axisY = "axisY";
    public string axisJright = "axis4";
    public string axisJup = "axis5";

    public string btnA = "button0";
    public string btnB = "button1";
    public string btnX = "button2";
    public string btnY = "button3";
    public string btnLB = "button4";
    public string btnRB = "button5";

    public MyButtons buttonA = new MyButtons();
    public MyButtons buttonB = new MyButtons();
    public MyButtons buttonX = new MyButtons();
    public MyButtons buttonY = new MyButtons();
    public MyButtons buttonLB = new MyButtons();
    public MyButtons buttonRB = new MyButtons();

    void Update()
    {
        buttonA.Tick(Input.GetButton(btnA));
        buttonB.Tick(Input.GetButton(btnB));
        buttonX.Tick(Input.GetButton(btnX));
        buttonY.Tick(Input.GetButton(btnY));
        buttonLB.Tick(Input.GetButton(btnLB));
        buttonRB.Tick(Input.GetButton(btnRB));


        ///<summary>
        /// WSAD移动
        /// </summary>
        targetdright = (Input.GetAxis(axisX));
        targetdup = (Input.GetAxis(axisY));
        ///<summary>
        /// 摄像头上下左右转动
        /// </summary>
        Jright = (Input.GetAxis(axisJright));
        Jup = -1*(Input.GetAxis(axisJup));
        ///<summary>
        /// 锁定键位命令输入
        /// </summary>
        if (InputEnabled == false)
        {
            targetdright = 0;
            targetdup = 0;
        }
        ///<summary>
        /// 插值实现平滑
        /// </summary>
        dup = Mathf.SmoothDamp(dup, targetdup, ref velocityDup, 0.1f);
        dright = Mathf.SmoothDamp(dright, targetdright, ref velocityDright, 0.1f);
        ///<summary>
        /// 矩形坐标轴转换成球形坐标轴
        /// </summary>
        tempdAxis = SquareToCircle(new Vector2(dright, dup));
        dright2 = tempdAxis.x;
        dup2 = tempdAxis.y;
        ///<summary>
        /// 勾股定理计算斜边
        /// </summary>
        dmap = Mathf.Sqrt(dup2 * dup2 + dright2 * dright2);
        ///<summary>
        /// 使玩家旋转
        /// </summary>
        dvec = dright2 * transform.right + dup2 * transform.forward;

        /*
            Run--跑
            Defense--防御
            Jump--跳
            Attack--攻击
         */
        Run = (buttonA.IsPressing&&!buttonA.IsDelaying) || buttonA.IsExtending ;
        //Jump = buttonB.OnPressed;
        Jump = buttonA.IsExtending && buttonA.OnPressed;       //双击跑键实现跳跃
        Roll = buttonA.IsDelaying && buttonA.OnReleased;

        Defense = buttonLB.IsPressing;
        Attack = buttonX.OnPressed;

    }
}
