using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }
    
    [SerializeField] private UIDialogue m_dialogueBox;

    DialogueData dialogueReceived;    

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            
        }
        else
        {
            Instance = this;
        }
    }

    public void ProcessDialogue(DialogueData _dialogueReceived)
    {
        if (_dialogueReceived.dialogueDataList.Count == 0)
        {
            // No lines provided. Do nothing.
            return;
        }
        
        m_dialogueBox.ShowDialogue(_dialogueReceived);
    }

    public void NextLine()
    {
        m_dialogueBox.NextLine();
    }


    public bool IsUIShowing()
    {
        return m_dialogueBox.IsActive;
    }
}
