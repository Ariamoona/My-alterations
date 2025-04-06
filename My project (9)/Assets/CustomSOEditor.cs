using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(CustomSO))]
public class CustomSOEditor : Editor
{
    private bool showList = false;
    private bool showText = false;

    public override void OnInspectorGUI()
    {
        CustomSO so = (CustomSO)target;

        
        so.uniqueId = EditorGUILayout.TextField("Unique ID", so.uniqueId);
        so.contentType = (AudioContentType)EditorGUILayout.EnumPopup("Content Type", so.contentType);
        so.textContent = EditorGUILayout.TextArea(so.textContent, GUILayout.Height(50));

        
        GUILayout.BeginHorizontal();
        if (GUILayout.Button(showList ? "Hide List" : "Show List"))
        {
            showList = !showList;
            showText = false;
        }
        if (GUILayout.Button(showText ? "Hide Text" : "Show Text"))
        {
            showText = !showText;
            showList = false;
        }
        if (GUILayout.Button("Hide All"))
        {
            showList = false;
            showText = false;
        }
        GUILayout.EndHorizontal();

        
        if (showList)
        {
            List<AudioData> currentList = GetCurrentList(so);
            DrawAudioList(currentList);
        }

       
        if (showText)
        {
            EditorGUILayout.LabelField("Text Content", EditorStyles.boldLabel);
            EditorGUILayout.TextArea(so.textContent, GUILayout.Height(100));
        }
    }

    private List<AudioData> GetCurrentList(CustomSO so)
    {
        switch (so.contentType)
        {
            case AudioContentType.Dangerous: return so.dangerousAudioList;
            case AudioContentType.Friendly: return so.friendlyAudioList;
            case AudioContentType.Neutral: return so.neutralAudioList;
            default: return new List<AudioData>();
        }
    }

    private void DrawAudioList(List<AudioData> list)
    {
        EditorGUILayout.LabelField($"{((CustomSO)target).contentType} Audio List", EditorStyles.boldLabel);
        for (int i = 0; i < list.Count; i++)
        {
            EditorGUILayout.BeginVertical("box");
            list[i].audioClip = (AudioClip)EditorGUILayout.ObjectField("Audio Clip", list[i].audioClip, typeof(AudioClip), false);
            list[i].volume = EditorGUILayout.Slider("Volume", list[i].volume, 0, 1);
            EditorGUILayout.EndVertical();
        }
    }
}