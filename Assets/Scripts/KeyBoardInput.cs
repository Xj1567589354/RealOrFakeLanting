using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoardInput : IUserInput
{
    [Space(5)]
    [Header("=====  Key settings  =====")]
    public string keyup;
    public string keydown;
    public string keyleft;
    public string keyright;

    public string keyRun;
    public string keyJump;
    public string keyAttack;
    public string keyDefense;
    public string keyLockon;
    public string keyGet;
    public string keyTab;

    public string keyJUp;
    public string keyJDown;
    public string keyJLeft;
    public string keyJRight;

    public MyButtons KR = new MyButtons();
    public MyButtons KJ = new MyButtons();
    public MyButtons KA = new MyButtons();
    public MyButtons KD = new MyButtons();
    public MyButtons KL = new MyButtons();
    public MyButtons KG = new MyButtons();
    public MyButtons KT = new MyButtons();

    [Header("=====  Mouse settings=====")]
    public bool mouseEnable = false;
    public float mouseSensitivityX = 1.0f;
    public float mouseSensitivityY = 1.0f;

    public bool KeyEnable = false;
    public bool KeyJumpEnable = true;       //跳跃信号激活状态
    void Update()
    {
        print(targetdright);
        KR.Tick(Input.GetKey(keyRun));
        KJ.Tick(Input.GetKey(keyJump));
        KA.Tick(Input.GetKey(keyAttack));
        KD.Tick(Input.GetKey(keyDefense));
        KL.Tick(Input.GetKey(keyLockon));
        KG.Tick(Input.GetKey(keyGet));
        KT.Tick(Input.GetKey(keyTab));


        ///<summary>
        /// WSAD移动
        /// </summary>
        if (KeyEnable == true)
        {
            targetdup = (Input.GetKey(keyup) ? 1.0f : 0) - (Input.GetKey(keydown) ? 1.0f : 0);
            targetdright = (Input.GetKey(keyright) ? 1.0f : 0) - (Input.GetKey(keyleft) ? 1.0f : 0);
            Run = (KR.IsPressing && !KR.IsDelaying) || KR.IsExtending;  //  Run接收到了延迟时间已过和等待扩展时间按钮是否再响应的信号
            Defense = KD.IsPressing;
            if (KeyJumpEnable)
            {
                Jump = KJ.OnPressed;
            }
            Attack = KA.OnPressed;
            Roll = KR.OnReleased && KR.IsDelaying;
            Lockon = KL.OnPressed;
            Interact = KG.OnPressed;
            Quit_Dialogue = KT.OnPressed;
        }
        else
        {
            targetdup = 0;      //避免向前移动键位叠加
        }
        ///<summary>
        /// 摄像头上下左右转动
        /// false--鼠标控制
        /// true--键盘控制
        /// </summary>
        if (mouseEnable == true)
        {
            Jup = Input.GetAxis("Mouse Y") * 3.0f * mouseSensitivityY;
            Jright = Input.GetAxis("Mouse X") * 2.5f * mouseSensitivityX;
        }
        else
        {
            Jup = (Input.GetKey(keyJUp) ? 1.0f : 0) - (Input.GetKey(keyJDown) ? 1.0f : 0);
            Jright = (Input.GetKey(keyJRight) ? 1.0f : 0) - (Input.GetKey(keyJLeft) ? 1.0f : 0);
        }
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
    }
}
