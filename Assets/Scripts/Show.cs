using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// ��ʼ��Ϸ��������
/// </summary>
public class Show : MonoBehaviour
{
    KeyBoardInput KBI;       //��λ����
    public bool FadeState;
    [SerializeField] private float fadeWaitTime = 10.0f;       //�����ȴ�ʱ��
    public CameraController camcon;
    float time = 0;
    public Image Back_Show;
    public bool StartGame;      //��ʼ��Ϸ
    public Image Ray_Image;     //׼��


    /*
     * ��������
     FadeWaitTime--�ⲿ������
    ��֤ĳЩ����İ�ȫ��
     */
    public float FadeWaitTime
    {
        get { return fadeWaitTime; }        //ֻ��
        set     //ֻд
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
         0.5s����Ծ����ָ�
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
    /// �����¼�
    /// </summary>
    /// <param name="_RawImage">ͼ��</param>
    public void Fade(RawImage _RawImage)
    {
        FadeState = true;
        if (FadeState == true)      //4s���԰���ʧ
        {
            _RawImage.gameObject.SendMessage("UIHide", SendMessageOptions.DontRequireReceiver);     //ͼ����ʧ
            camcon.ShowText.gameObject.SendMessage("UIHide", SendMessageOptions.DontRequireReceiver);     //��ʾ��ʧ
            camcon.ShowText2.gameObject.SendMessage("UIHide", SendMessageOptions.DontRequireReceiver);     //��ʾ��ʧ

            KBI.mouseEnable = true;     //��������ͷ�ƶ�
            KBI.KeyEnable = true;       //���ü�λ����
            KBI.KeyJumpEnable = false;      //������Ծ����
            FadeState = false;
            camcon.ShowNumber = 0;      //��Ʒ��Ź���
            camcon.Interact_State = false;      //���⽻���ź��ص�
            KBI.Jump = false;       //������Ծ
        }
    }
}
