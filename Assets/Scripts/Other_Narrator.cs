using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Other_Narrator : MonoBehaviour
{
    public Dialogue _Narrator;
    public Queue<string> Sentences;
    public GameObject PlayerHandle;
    public Text NarratorText;
    bool FadeState = false;
    public float time = 0;
    public TaskList TaskList;
    KeyBoardInput KBI;       //键位控制
    public CameraController camcon;

    private void Start()
    {
        Sentences = new Queue<string>();
        KBI = FindObjectOfType<KeyBoardInput>();
    }

    public void Update()
    {
        if (FadeState == true)      //4s后旁白消失
        {
            time += Time.deltaTime;
            if (time > 4.0f)
            {
                NarratorText.gameObject.SendMessage("UIHide", SendMessageOptions.DontRequireReceiver);
                KBI.mouseEnable = true;
                KBI.KeyEnable = true;

                FadeState = false;
                time = 0;

                TaskList.ElapseTime = 0;                //任务计时清零
                TaskList.Count++;       //显示下一步任务提示
                camcon.BlurImage_ThingShow.gameObject.SendMessage("UIHide", SendMessageOptions.DontRequireReceiver);    //线索旁白消失
            }
        }
    }

    /// <summary>
    /// 开始旁白事件
    /// </summary>
    /// <param name="_narrator">旁白内容</param>
    public void StartNarrator(Dialogue _narrator)
    {
        NarratorText.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);       //旁白显示
        KBI.mouseEnable = false;        //禁用摄像头移动
        KBI.KeyEnable = false;          //禁用键位输入

        Sentences.Clear();  //清空队列
        foreach (string item_sentence in _narrator.Sentences)
        {
            Sentences.Enqueue(item_sentence);       //入队
        }
        DisplayNarrator();      //播放下一段旁白
    }

    /// <summary>
    /// 播放下一段旁白事件
    /// </summary>
    public void DisplayNarrator()
    {
        StopAllCoroutines();
        string sentence = Sentences.Dequeue();
        StartCoroutine(TypeSentence(sentence));
    }

    /// <summary>
    /// 旁白逐字显示事件
    /// </summary>
    /// <param name="Sentence">旁白内容</param>
    /// <returns></returns>
    IEnumerator TypeSentence(string Sentence)
    {
        NarratorText.text = "";
        foreach (char letter in Sentence.ToCharArray())
        {
            NarratorText.text += letter;
            yield return new WaitForSeconds(0.1f);      //0.1s后继续执行
        }
        EndNarrator();
    }

    /// <summary>
    /// 结束旁白
    /// </summary>
    public void EndNarrator()
    {
        FadeState = true;
    }
}
