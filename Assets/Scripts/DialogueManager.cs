using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Queue<string> Sentences;  //�ַ�����
    public Text NameText;
    public Text DialogueText;

    public Animator animator;

    void Start()
    {
        Sentences = new Queue<string>();
    }

    /// <summary>
    /// ��ʼ�Ի�
    /// </summary>
    /// <param name="_dialogue">�Ի�����</param>
    public void StartDialogue(Dialogue _dialogue)
    {
        animator.SetBool("IsOpen", true);
        NameText.text = _dialogue.Name;

        Sentences.Clear();  //�����һ�ζԻ�����

        foreach (string sentence in _dialogue.Sentences)
        {
            Sentences.Enqueue(sentence);    //���
        }
        DisplayNextSentences();     //������һ��Ի�����
    }

    /// <summary>
    /// ������һ�ζԻ�����
    /// </summary>
    public void DisplayNextSentences()
    {
        if (Sentences.Count == 0)   //�����β
        {
            EndDialogue();
            return;
        }

        StopAllCoroutines();    //����֮ǰ����Э�̲���
        string sentence = Sentences.Dequeue();      //��������
        StartCoroutine(TypeSentence(sentence));     //����Э��
    }

    /// <summary>
    /// Э��ʵ�ֶԻ�ϵͳ������ʾ
    /// </summary>
    /// <param name="Sentence">�Ի�����</param>
    /// <returns></returns>
    IEnumerator TypeSentence(string Sentence)
    {
        DialogueText.text = "";
        foreach (char letter in Sentence.ToCharArray())
        {
            DialogueText.text += letter;
            yield return new WaitForSeconds(0.1f);      //0.1s�����ִ��
        }
    }

    /// <summary>
    /// �����Ի�
    /// </summary>
    public void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        //GetComponentInChildren<CameraController>().Quit_Dlo = true;         //�˳��Ի�ϵͳ
    }
}
