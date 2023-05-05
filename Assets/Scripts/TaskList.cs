using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskList : MonoBehaviour
{
    public Text Task_Context;
    public Text Task_Title;
    public Image Sign;
    public float Count;     //�������
    public float ElapseTime;        //ʵʱʱ��
    public float FadeTime = 0;
    public CameraController camcon;

    private void Start()
    {
        Count = 0;
        ElapseTime = 0;
    }

    private void Update()
    {
        print(Count);
        switch (Count)
        {
            case 1:
                Task_Title.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);       //���������ʾ
                Sign.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);     //���������ʾͼƬ
                Task_Context.text = "ǰ���Ḯ�鷿��¥���常��ȶԻ�";
                Task_Context.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);       //������ʾ����
                break;
            case 2:
                Task_Update("ǰ���丮�˽�ʵ��");
                break;
            case 3:
                Task_Update("ǰ������ͤѰ��Ŀ����");
                break;
            case 4:
                Task_Update("������Ԫ��ʬ��");
                break;
            case 5:
                Task_Update("ǰ���ƹ�");
                break;
            case 6:
                Task_Update("���̷��Ի�");
                break;
            case 7:
                Task_Update("ǰ�������ֲ�ɱҰ�޻�ȡ���������̷���Ƭ");
                break;
            case 8:
                Task_Update("ǰ�������Ժ");
                break;
            case 9:
                Task_Update("����Ա�Ի�");
                break;
            case 10:
                Task_Update("������ӶԻ�");
                break;
            case 11:
                Task_Update("ǰ�������¸���ͷ֮Լ");
                break;
            case 12:
                Task_Update("ǰ������");
                break;
            case 13:
                Task_Update("ǰ��������¥��ɮ�˱�ŶԻ�");
                break;
            case 14:
                Task_Update("�밢���Ի�");
                break;
            case 15:
                Task_Update("�ڴ�������Ѱ�����");
                break;
            case 16:
                Task_Update("�������ʬ��");
                break;
            case 17:
                Task_Update("������Ժ�뺫������Ա�Ի�");
                break;
            case 20:
                Task_Update("�뺫���ӶԻ�");
                break;
            case 21:
                Task_Update("���������ֲ����뺫���ӻ�ȡ��Ƭ");
                break;
            case 22:
                Task_Update("ǰ���޸�");
                break;
            case 23:
                Task_Update("����Ի�");
                break;
            case 24:
                Task_Update("ǰ�������ֱ�����ɱҰ���������ȡ��Ƭ");
                break;
            case 25:
                Task_Update("ǰ���ָ�");
                break;
            case 26:
                Task_Update("��ֲ��Ի�");
                break;
            case 27:
                Task_Update("Ѱ�Ҽֲ�����Ƭ");
                break;
            case 28:
                Task_Update("������ͤ����Ƭ�ϳ�һ�������ġ���ͤ��");            //�ϳɹ���
                break;
            case 29:
                Task_Update("�鿴����ͤ����ġ��");
                break;
            case 30:
                Task_Update("ǰ������������Ի�");
                break;
            case 31:
                Task_Update("ǰ����������¶���˽�ʵ��");
                break;
            case 32:
                Task_Update("ǰ����¶���¥����̫�ڶԻ�");
                break;
            case 33:
                Task_Update("�ڴ�������Ѱ���漣����ͤ��");
                break;
            case 34:
                Task_Update("���ش������˽�����Ը");
                break;
            case 35:
                Task_Title.text = "����ɣ� Ѱ�ҡ���ͤ��";
                FadeTime += Time.deltaTime;
                if (FadeTime > 3.0f)        //3s��������ʾ��ʧ
                {
                    Task_Title.gameObject.SendMessage("UIHide", SendMessageOptions.DontRequireReceiver);
                    Sign.gameObject.SendMessage("UIHide", SendMessageOptions.DontRequireReceiver);
                    Task_Context.gameObject.SendMessage("UIHide", SendMessageOptions.DontRequireReceiver);
                    FadeTime = 0;
                }
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// ��������¼�
    /// </summary>
    /// <param name="_Word">��������</param>
    public void Task_Update(string _Word)
    {
        Task_Context.gameObject.SendMessage("UIHide", SendMessageOptions.DontRequireReceiver);      //��һ��������ʾ��ʧ
        ElapseTime += Time.deltaTime;
        if (ElapseTime >= 2.0f)         //2s��������ʾ����
        {
            Task_Context.text = _Word;
            Task_Context.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);
        }
    }
}
