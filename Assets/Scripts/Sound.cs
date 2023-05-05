using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;     //“Ù∆µ√˚≥∆
    public AudioClip Clip;      //“Ù∆µ

    [Range(0, 1)]
    public float Valume;        //“Ù¡ø
    [Range(.1f, 3.0f)]
    public float Pitch;     //“Ù∏ﬂ
    public bool Loop;       //—≠ª∑

    [HideInInspector]
    public AudioSource Source;      //“Ù∆µ‘¥
}
