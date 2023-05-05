using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;     //��Ƶ����
    public AudioClip Clip;      //��Ƶ

    [Range(0, 1)]
    public float Valume;        //����
    [Range(.1f, 3.0f)]
    public float Pitch;     //����
    public bool Loop;       //ѭ��

    [HideInInspector]
    public AudioSource Source;      //��ƵԴ
}
