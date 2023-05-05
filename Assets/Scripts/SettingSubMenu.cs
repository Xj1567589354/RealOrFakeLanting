using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingSubMenu : MonoBehaviour
{
    public AudioMixer TatalAudio;       //总音量混合器
    public AudioMixer BackAudio;        //背景音乐混合器
    public CameraController camcon;
    public KeyBoardInput KBI;

    /// <summary>
    /// 设置总音量
    /// </summary>
    /// <param name="valume">音量大小</param>
    public void SetSoundTatalValue(float valume)
    {
        TatalAudio.SetFloat("Valume", valume);
    }

    /// <summary>
    /// 设置背景音乐音量
    /// </summary>
    /// <param name="valume">音量大小</param>
    public void SetSoundBackValue(float valume)
    {
        TatalAudio.SetFloat("Valume", valume);
    }
    /// <summary>
    /// 继续游戏
    /// </summary>
    public void ContinueGame()
    {
        camcon.isOpenMenu = !camcon.isOpenMenu;
        //camcon.SubMenu.SetActive(camcon.isOpenMenu);
        KBI.mouseEnable = true;        //禁用摄像头移动
        KBI.KeyEnable = true;          //禁用键位输入
        camcon.BlurImage_SubMenu.SetActive(false);
        if (camcon.isOpenMenu)  //显示鼠标
        {
            camcon.isOpen = true;
        }
        else if (!camcon.isOpenMenu)
        {
            camcon.isOpen = false;
            camcon.animator.SetBool("isOpen", camcon.isOpenMenu);
        }
    }
    /// <summary>
    /// 返回主菜单
    /// </summary>
    public void ReturnMainMenu()
    {
        SceneManager.LoadScene(0);      //重新加载场景
    }
}
