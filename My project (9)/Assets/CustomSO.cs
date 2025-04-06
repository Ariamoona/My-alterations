using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewCustomSO", menuName = "Custom SO")]
public class CustomSO : ScriptableObject
{
    public string uniqueId; 
    public AudioContentType contentType; 
    [TextArea(5, 20)] public string textContent; 

    [HideInInspector] public List<AudioData> dangerousAudioList = new List<AudioData>();
    [HideInInspector] public List<AudioData> friendlyAudioList = new List<AudioData>();
    [HideInInspector] public List<AudioData> neutralAudioList = new List<AudioData>();
}