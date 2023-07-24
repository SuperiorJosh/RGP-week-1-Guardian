using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }
    
    [SerializeField]GameObject dialogueUI;
    [SerializeField] Image speakersImage;
    [SerializeField] TMP_Text tmpro;
    private int linesIndex;
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
        if(_dialogueReceived._lines.Length == 0)
        {
            // No lines provided. Do nothing.
            return;
        }
        
        // Assign sprite and first line.
        dialogueUI.SetActive(true);
        linesIndex = 0;
        dialogueReceived = _dialogueReceived;
        speakersImage.sprite = dialogueReceived._speakersSprite;
        tmpro.text = dialogueReceived._lines[linesIndex];

        // TODO: Activate dialogue UI.
    }

    public void NextLine()
    {
        linesIndex++;
        if(linesIndex < dialogueReceived._lines.Length)
        {
            tmpro.text = dialogueReceived._lines[linesIndex];
        }
        else{
            // No more lines.
            // TODO: Deactivate dialogue UI.
            dialogueUI.SetActive(false);
        }
    }

    public bool IsUIShowing()
    {
        return dialogueUI.activeSelf;
    }
}
