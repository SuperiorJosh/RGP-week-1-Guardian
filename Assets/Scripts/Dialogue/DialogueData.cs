using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Dialogue Data", fileName = "Dialogue Data")]
public class DialogueData : ScriptableObject
{
    [SerializeField] private bool m_repeats;
    public bool DeliveredDialogue;

    private void OnEnable()
    {
        DeliveredDialogue = false;
    }

    [System.Serializable]
    public struct SpriteLinesPair
    {
        [AssetSelector(Paths = "Assets/Resources/Dialogue Data")]
        public DialogueSpeaker Speaker;
        public SpeakerEmotions Emotion;
        public string dialogueLine;
    }
    public List<SpriteLinesPair> dialogueDataList;
    
    //[SerializeField] Sprite speakersSprite;
    //[SerializeField] string[] dialogueLines;

    //public Sprite _speakersSprite => speakersSprite;
    //public string[] _lines => dialogueLines;
}