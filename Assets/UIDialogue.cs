using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIDialogue : MonoBehaviour
{
    [SerializeField] private Image m_speakerImage;
    [SerializeField] private TMP_Text m_text;

    public UnityEvent DialogueFinished;

    public void ShowDialogue(DialogueData dialogue)
    {
        
    }
}
