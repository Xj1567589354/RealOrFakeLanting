using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    private GameObject PlayerHandle;        //���
    string filePath;    //XML�ĵ�·��
    XmlDocument Doc;
    string SceneState = null;   //��ǰ��������
    public float ElapseTime = 0;    //��ʾ���ֿ�ʼ���ּ�ʱ�ο�
    Timer ElapseTimer;
    public Text SceneText;
    private UIAlpha UIAlpha;


    private void Awake()
    {
        UIAlpha = new UIAlpha();
        ElapseTimer = new Timer();
        PlayerHandle = GameObject.Find("PlayerHandle");
        Doc = new XmlDocument();
        filePath = Resources.Load("SceneText").ToString();
        Doc.LoadXml(filePath);  //��ȡXML�ĵ�
    }

    private void Update()
    {
        XmlNodeList nodeList = Doc.SelectSingleNode("item").ChildNodes;
        //�����ĵ�
        foreach (XmlElement xmlElement in nodeList)
        {
            if (xmlElement.Name == SceneState)
            {
                //��ʼ��ʱ
                ElapseTime += Time.deltaTime;
                ElapseTimer.TimerJudge(ElapseTime, 5.0f);


                foreach (XmlElement ChildElement in xmlElement.ChildNodes)
                {
                    SceneText.text = ChildElement.InnerText;
                    SceneText.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);       //������ʾ����
                    if (ElapseTimer.EndState == false)
                    {
                        SceneText.gameObject.SendMessage("UIHide", SendMessageOptions.DontRequireReceiver);      //������ʾ��ʧ
                        SceneState = null;  //��ʾһ��
                        ElapseTimer.StartState = true;
                    }
                }
            }
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == PlayerHandle)
        {
            SceneState = gameObject.name;
            ElapseTime = 0;     //��ʱ����
            //print(SceneState);
        }
    }
}
