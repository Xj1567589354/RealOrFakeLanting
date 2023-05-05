using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 初始游戏背景淡入
/// </summary>
public class Show : MonoBehaviour
{
    KeyBoardInput KBI;       //键位控制
    public bool FadeState;
    [SerializeField] private float fadeWaitTime = 10.0f;       //淡出等待时间
    public CameraController camcon;
    float time = 0;
    public Image Back_Show;
    public bool StartGame;      //开始游戏
    public Image Ray_Image;     //准心


    /*
     * 场景淡入
     FadeWaitTime--外部访问器
    保证某些方面的安全性
     */
    public float FadeWaitTime
    {
        get { return fadeWaitTime; }        //只读
        set     //只写
        {
            if (value < 0)
            {
                Back_Show.gameObject.SendMessage("UIHide", SendMessageOptions.DontRequireReceiver);
                KBI.KeyEnable = true;
                Ray_Image.enabled = true;
            }
            fadeWaitTime = value;
        }
    }


    private void Start()
    {
        Ray_Image.enabled = false;
        FadeState = false;
        KBI = FindObjectOfType<KeyBoardInput>();
    }


    private void Update()
    {
        print(fadeWaitTime);
        if (StartGame && FadeWaitTime > 0)
        {
            FadeWaitTime -= Time.deltaTime;
        }
        /*
         0.5s后跳跃输入恢复
         */
        if (KBI.KeyJumpEnable == false)
        {
            time += Time.deltaTime;
            print(time);
            if (time >= 0.5f)
            {
                KBI.KeyJumpEnable = true;
            }
        }

    }

    /// <summary>
    /// 淡出事件
    /// </summary>
    /// <param name="_RawImage">图像</param>
    public void Fade(RawImage _RawImage)
    {
        FadeState = true;
        if (FadeState == true)      //4s后旁白消失
        {
            _RawImage.gameObject.SendMessage("UIHide", SendMessageOptions.DontRequireReceiver);     //图像消失
            camcon.ShowText.gameObject.SendMessage("UIHide", SendMessageOptions.DontRequireReceiver);     //提示消失
            camcon.ShowText2.gameObject.SendMessage("UIHide", SendMessageOptions.DontRequireReceiver);     //提示消失

            KBI.mouseEnable = true;     //禁用摄像头移动
            KBI.KeyEnable = true;       //禁用键位输入
            KBI.KeyJumpEnable = false;      //禁用跳跃输入
            FadeState = false;
            camcon.ShowNumber = 0;      //物品序号归零
            camcon.Interact_State = false;      //避免交互信号重叠
            KBI.Jump = false;       //禁用跳跃
        }
    }
}
