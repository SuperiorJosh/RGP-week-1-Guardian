using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Dialogue Speaker", fileName = "Speaker")]
public class DialogueSpeaker : SerializedScriptableObject
{
    [SerializeField] private string m_name;
    [OdinSerialize] private Dictionary<SpeakerEmotions, Sprite> m_emotions;

    public string Name => m_name;
    
    public Sprite GetEmotion(SpeakerEmotions emotion)
    {
        if (m_emotions.TryGetValue(emotion, out var sprEmotion))
        {
            return sprEmotion;
        }

        return null;
    }
}

public enum SpeakerEmotions
{
    Neutral,
    Sad,
    Shocked,
    Speaking,
}