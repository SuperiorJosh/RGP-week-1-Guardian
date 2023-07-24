using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSender : MonoBehaviour
{
    // [System.Serializable]
    // public struct DialogueData
    // {
    //     public Sprite speakersSprite;
    //     public string[] dialogueLines;
    // }
    //public List<DialogueData> dialogueDataList;

    //public void DeliverDialogue(DialogueData _dialogueData)
    public void DeliverDialogue(DialogueData _dialogueData)
    {
        // TODO: Send data to dialogue canva manager.
        DialogueManager.Instance.ProcessDialogue(_dialogueData);
    }
}
