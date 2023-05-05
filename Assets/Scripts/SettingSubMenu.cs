using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingSubMenu : MonoBehaviour
{
    public AudioMixer TatalAudio;       //�����������
    public AudioMixer BackAudio;        //�������ֻ����
    public CameraController camcon;
    public KeyBoardInput KBI;

    /// <summary>
    /// ����������
    /// </summary>
    /// <param name="valume">������С</param>
    public void SetSoundTatalValue(float valume)
    {
        TatalAudio.SetFloat("Valume", valume);
    }

    /// <summary>
    /// ���ñ�����������
    /// </summary>
    /// <param name="valume">������С</param>
    public void SetSoundBackValue(float valume)
    {
        TatalAudio.SetFloat("Valume", valume);
    }
    /// <summary>
    /// ������Ϸ
    /// </summary>
    public void ContinueGame()
    {
        camcon.isOpenMenu = !camcon.isOpenMenu;
        //camcon.SubMenu.SetActive(camcon.isOpenMenu);
        KBI.mouseEnable = true;        //��������ͷ�ƶ�
        KBI.KeyEnable = true;          //���ü�λ����
        camcon.BlurImage_SubMenu.SetActive(false);
        if (camcon.isOpenMenu)  //��ʾ���
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
    /// �������˵�
    /// </summary>
    public void ReturnMainMenu()
    {
        SceneManager.LoadScene(0);      //���¼��س���
    }
}
