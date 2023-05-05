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
            sound.Source = gameObject.AddComponent<AudioSource>();     //添加音频源
            sound.Source.clip = sound.Clip;     //获取音频
            sound.Source.volume = sound.Valume;     //获取音量
            sound.Source.pitch = sound.Pitch;       //获取音高
            sound.Source.loop = sound.Loop;       //循环
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown("left shift") && isGround)
        {
            FindObjectOfType<AudioManager>().Play("跑", true);
        }
        if (Input.GetKeyUp("left shift") || !isGround)
        {
            FindObjectOfType<AudioManager>().Play("跑", false);
        }
    }
    /// <summary>
    /// 播放音频
    /// </summary>
    /// <param name="_name">音频名称</param>
    public void Play(string _name, bool isStart)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == _name)
            {
                if (isStart) s.Source.Play();        //播放声音
                else s.Source.Stop();       //停止声音
            }
        }
    }
}
