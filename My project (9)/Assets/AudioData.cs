using UnityEngine;

[System.Serializable]
public class AudioData
{
    public AudioClip audioClip;
    [Range(0, 1)] public float volume = 1.0f;
}
