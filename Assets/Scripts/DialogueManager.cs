using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Queue<string> Sentences;  //字符队列
    public Text NameText;
    public Text DialogueText;

    public Animator animator;

    void Start()
    {
        Sentences = new Queue<string>();
    }

    /// <summary>
    /// 开始对话
    /// </summary>
    /// <param name="_dialogue">对话内容</param>
    public void StartDialogue(Dialogue _dialogue)
    {
        animator.SetBool("IsOpen", true);
        NameText.text = _dialogue.Name;

        Sentences.Clear();  //清除上一次对话内容

        foreach (string sentence in _dialogue.Sentences)
        {
            Sentences.Enqueue(sentence);    //入队
        }
        DisplayNextSentences();     //播放下一句对话内容
    }

    /// <summary>
    /// 播放下一段对话内容
    /// </summary>
    public void DisplayNextSentences()
    {
        if (Sentences.Count == 0)   //到达队尾
        {
            EndDialogue();
            return;
        }

        StopAllCoroutines();    //结束之前所有协程操作
        string sentence = Sentences.Dequeue();      //遍历队列
        StartCoroutine(TypeSentence(sentence));     //调用协程
    }

    /// <summary>
    /// 协程实现对话系统逐字显示
    /// </summary>
    /// <param name="Sentence">对话内容</param>
    /// <returns></returns>
    IEnumerator TypeSentence(string Sentence)
    {
        DialogueText.text = "";
        foreach (char letter in Sentence.ToCharArray())
        {
            DialogueText.text += letter;
            yield return new WaitForSeconds(0.1f);      //0.1s后继续执行
        }
    }

    /// <summary>
    /// 结束对话
    /// </summary>
    public void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        //GetComponentInChildren<CameraController>().Quit_Dlo = true;         //退出对话系统
    }
}
