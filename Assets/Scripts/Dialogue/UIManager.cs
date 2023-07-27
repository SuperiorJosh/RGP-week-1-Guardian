using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    
    [SerializeField] private UIDialogue m_dialogueBox;
    [SerializeField] private UIAltTextBox m_altTextBox;
    [SerializeField] private UIInventory m_inventory;

    private bool m_showHud = true;

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

    public void ShowAltText(string text, Transform target, Vector3 offset, bool uiTarget, UnityEvent hideEvent)
    {
        m_altTextBox.Show(text, target, offset, false, hideEvent);
    }
    
    public void ProcessDialogue(DialogueData _dialogueReceived)
    {
        if (_dialogueReceived.dialogueDataList.Count == 0)
        {
            // No lines provided. Do nothing.
            return;
        }

        if (m_showHud)
        {
            m_inventory.Hide();
        }
        m_dialogueBox.DialogueFinished.AddListener(OnDialogueFinished);
        m_dialogueBox.ShowDialogue(_dialogueReceived);
    }

    private void OnDialogueFinished()
    {
        m_dialogueBox.DialogueFinished.RemoveListener(OnDialogueFinished);
        if (m_showHud)
        {
            m_inventory.Show();
        }
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
