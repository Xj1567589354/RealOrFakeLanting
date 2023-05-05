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
    /// ��ʼ��Ϸ
    /// </summary>
    /// 
    public void Update()
    {
        KBI.mouseEnable = false;        //��������ͷ�ƶ�
        KBI.KeyEnable = false;          //���ü�λ����
    }
    public void PlayGame()
    {
        show.StartGame = true;      //��ʼ��Ϸ
        Menu.SetActive(false);      //�������˵�

        FindObjectOfType<AudioManager>().Play("��������", true);
        minMap.MinMapImage.SetActive(true);

        KBI.mouseEnable = true;        //��������ͷ�ƶ�
        KBI.KeyEnable = true;          //���ü�λ����
    }
    /// <summary>
    /// �˳���Ϸ
    /// </summary>
    public void ExitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
    /// <summary>
    /// �˵������Ч
    /// </summary>
    public void ClickSound()
    {
        FindObjectOfType<AudioManager>().Play("���", true);      //�˵����
    }
}
