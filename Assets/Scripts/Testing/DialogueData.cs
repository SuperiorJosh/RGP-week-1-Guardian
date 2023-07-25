using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Dialogue Data", fileName = "Dialogue Data")]
public class DialogueData : ScriptableObject
{
    [System.Serializable]
    public struct SpriteLinesPair
    {
        public Sprite speakerSprite;
        public string dialogueLine;
    }
    public List<SpriteLinesPair> dialogueDataList;
    
    //[SerializeField] Sprite speakersSprite;
    //[SerializeField] string[] dialogueLines;

    //public Sprite _speakersSprite => speakersSprite;
    //public string[] _lines => dialogueLines;
}
