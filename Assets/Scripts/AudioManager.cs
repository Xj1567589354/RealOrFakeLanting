using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public KeyBoardInput KBI;
    public bool isGround = true;

    private void Awake()
    {
        foreach (Sound sound in sounds)
        {
            sound.Source = gameObject.AddComponent<AudioSource>();     //�����ƵԴ
            sound.Source.clip = sound.Clip;     //��ȡ��Ƶ
            sound.Source.volume = sound.Valume;     //��ȡ����
            sound.Source.pitch = sound.Pitch;       //��ȡ����
            sound.Source.loop = sound.Loop;       //ѭ��
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown("left shift") && isGround)
        {
            FindObjectOfType<AudioManager>().Play("��", true);
        }
        if (Input.GetKeyUp("left shift") || !isGround)
        {
            FindObjectOfType<AudioManager>().Play("��", false);
        }
    }
    /// <summary>
    /// ������Ƶ
    /// </summary>
    /// <param name="_name">��Ƶ����</param>
    public void Play(string _name, bool isStart)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == _name)
            {
                if (isStart) s.Source.Play();        //��������
                else s.Source.Stop();       //ֹͣ����
            }
        }
    }
}
