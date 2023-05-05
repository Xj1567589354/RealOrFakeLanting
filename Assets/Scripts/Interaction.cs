using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    private GameObject PlayerHandle;        //玩家
    string filePath;    //XML文档路径
    XmlDocument Doc;
    string SceneState = null;   //当前触碰对象
    public float ElapseTime = 0;    //提示文字开始出现计时参考
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
        Doc.LoadXml(filePath);  //读取XML文档
    }

    private void Update()
    {
        XmlNodeList nodeList = Doc.SelectSingleNode("item").ChildNodes;
        //遍历文档
        foreach (XmlElement xmlElement in nodeList)
        {
            if (xmlElement.Name == SceneState)
            {
                //开始计时
                ElapseTime += Time.deltaTime;
                ElapseTimer.TimerJudge(ElapseTime, 5.0f);


                foreach (XmlElement ChildElement in xmlElement.ChildNodes)
                {
                    SceneText.text = ChildElement.InnerText;
                    SceneText.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);       //场景提示出现
                    if (ElapseTimer.EndState == false)
                    {
                        SceneText.gameObject.SendMessage("UIHide", SendMessageOptions.DontRequireReceiver);      //场景提示消失
                        SceneState = null;  //显示一次
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
            ElapseTime = 0;     //计时归零
            //print(SceneState);
        }
    }
}
