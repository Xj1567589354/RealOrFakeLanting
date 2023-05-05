using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Show show;
    public GameObject Menu;
    public CameraController camcon;
    public MinMap minMap;
    public KeyBoardInput KBI;
    /// <summary>
    /// 开始游戏
    /// </summary>
    /// 
    public void Update()
    {
        KBI.mouseEnable = false;        //禁用摄像头移动
        KBI.KeyEnable = false;          //禁用键位输入
    }
    public void PlayGame()
    {
        show.StartGame = true;      //开始游戏
        Menu.SetActive(false);      //隐藏主菜单

        FindObjectOfType<AudioManager>().Play("背景音乐", true);
        minMap.MinMapImage.SetActive(true);

        KBI.mouseEnable = true;        //启用摄像头移动
        KBI.KeyEnable = true;          //启用键位输入
    }
    /// <summary>
    /// 退出游戏
    /// </summary>
    public void ExitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
    /// <summary>
    /// 菜单点击音效
    /// </summary>
    public void ClickSound()
    {
        FindObjectOfType<AudioManager>().Play("点击", true);      //菜单点击
    }
}
