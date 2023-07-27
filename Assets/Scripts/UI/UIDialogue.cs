using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIDialogue : MonoBehaviour
{
    [SerializeField] private Image m_speakerImage;
    [SerializeField] private TMP_Text m_text;

    private CanvasGroup m_canvasGroup;

    public UnityEvent DialogueFinished;

    private DialogueSpeaker m_currentSpeaker;
    private DialogueData m_data;
    private int dialogueIndex;
    private int linesIndex;

    private ActorDialoguePanel m_currentPanel;
    private ActorDialoguePanel m_otherPanel;
    [SerializeField] private RectTransform m_textPanel;

    [SerializeField] private ActorDialoguePanel m_playerDialogue;
    [SerializeField] private ActorDialoguePanel m_npcDialogue;
    
    [SerializeField] private DialogueSpeaker m_playerSpeakerRef;

    [SerializeField] private Color m_inactivePanelColour;
    [SerializeField] private Color m_inactiveSpeakerColour;

    private int m_npcDialogueCount;
    private int m_playerDialogueCount;
    
    [Serializable]
    public struct ActorDialoguePanel
    {
        public RectTransform DialoguePanel;
        public Image DialoguePanelBg;
        public Image SpeakerImage;
        public TMP_Text DialogueText;
    }

    private bool m_active = false;
    public bool IsActive => m_active;

    private Sequence m_currentSequence;
    
    private void Awake()
    {
        m_canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        m_canvasGroup.alpha = 0;
        m_canvasGroup.blocksRaycasts = false;
        
        m_playerDialogue.DialogueText.text = "";
        m_npcDialogue.DialogueText.text = "";
        m_playerDialogue.DialoguePanel.localScale = new Vector3(0, 0, 0);
        m_playerDialogue.SpeakerImage.transform.localScale = new Vector3(0, 0, 0);
        m_npcDialogue.DialoguePanel.localScale = new Vector3(0, 0, 0);
        m_npcDialogue.SpeakerImage.transform.localScale = new Vector3(0, 0, 0);
    }

    public void ShowDialogue(DialogueData dialogue)
    {
        if (IsActive)
        {
            Debug.LogWarning("Dialogue already active!");
            return;
        }
        dialogueIndex = 0;
        m_canvasGroup.blocksRaycasts = true;
        
        m_data = dialogue;
        var firstDialogue = m_data.dialogueDataList[0];
        m_currentSpeaker = firstDialogue.Speaker;
        
        // assign initial npc sprite
        foreach (var dialog in m_data.dialogueDataList.Where(dialog => dialog.Speaker != m_playerSpeakerRef))
        {
            if (dialog.Speaker == null) continue;
            m_npcDialogue.SpeakerImage.sprite = dialog.Speaker.GetEmotion(SpeakerEmotions.Neutral);
            break;
        }

        m_npcDialogueCount = m_data.dialogueDataList.Count(dialog => dialog.Speaker != m_playerSpeakerRef && dialog.Speaker != null);
        m_playerDialogueCount = m_data.dialogueDataList.Count(dialog => dialog.Speaker == m_playerSpeakerRef);

        m_active = true;
        
        if (m_currentSpeaker == m_playerSpeakerRef)
        {
            ShowPlayerDialogue();
        }
        else
        {
            ShowNpcDialogue();
        }
    }

    void ShowPlayerDialogue()
    {
        var dialogue = m_data.dialogueDataList[dialogueIndex];
        m_playerDialogue.SpeakerImage.sprite = dialogue.Speaker.GetEmotion(dialogue.Emotion);
        m_currentSpeaker = dialogue.Speaker;
        ShowSequence(m_playerDialogue);
    }

    void ShowNpcDialogue()
    {
        var dialogue = m_data.dialogueDataList[dialogueIndex];
        m_npcDialogue.SpeakerImage.sprite = dialogue.Speaker.GetEmotion(dialogue.Emotion);
        m_currentSpeaker = dialogue.Speaker;
        ShowSequence(m_npcDialogue);
    }

    void ShowSequence(ActorDialoguePanel actorDialoguePanel)
    {
        if (m_currentSequence != null)
        {
            if (m_currentSequence.IsPlaying())
            {
                m_currentSequence.Goto(m_currentSequence.Duration(), true);
            }
        }
        
        var showOtherDialogue = true;
        if (actorDialoguePanel.Equals(m_playerDialogue))
        {
            m_currentPanel = m_playerDialogue;
            m_otherPanel = m_npcDialogue;
            if (m_npcDialogueCount <= 0)
            {
                // enable other box
                showOtherDialogue = false;
            }
        }
        else
        {
            m_currentPanel = m_npcDialogue;
            m_otherPanel = m_playerDialogue;
            if (m_playerDialogueCount <= 0)
            {
                showOtherDialogue = false;
            }
        }

        m_currentPanel.DialoguePanel.transform.SetAsLastSibling();
        
        if (showOtherDialogue)
        {
            m_otherPanel.DialoguePanelBg.color = m_inactivePanelColour;
            m_otherPanel.SpeakerImage.color = m_inactiveSpeakerColour;
        }
        else
        {
            var zeroAlpha = new Color(255f, 255f, 255f, 0f);
            m_otherPanel.DialoguePanelBg.color = zeroAlpha;
            m_otherPanel.SpeakerImage.color = zeroAlpha;
        }

        var white = new Color(255f,255f,255f,255f);
        m_currentPanel.DialoguePanelBg.color = white;
        m_currentPanel.SpeakerImage.color = white;

        var sequence = DOTween.Sequence();
        sequence
            .Insert(0f, m_canvasGroup.DOFade(1f, 0.3f))
            .Insert(0.1f, m_currentPanel.SpeakerImage.transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f))
            .Insert(0.1f, m_currentPanel.DialoguePanel.DOScale(new Vector3(1f, 1f, 1f), 0.5f).OnComplete(() =>
            {
                if (m_currentPanel.Equals(default(ActorDialoguePanel)) || m_data == null)
                {
                    Debug.Log("Something is null");
                }
                m_currentPanel.DialogueText.text = m_data.dialogueDataList[0].dialogueLine;
            }))
            .Insert(0.2f, m_currentPanel.DialogueText.DOFade(1f, 0.2f));
        
        if (showOtherDialogue)
        {
            sequence.Insert(0.3f, m_otherPanel.SpeakerImage.transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f))
                .Insert(0.3f, m_otherPanel.DialoguePanel.DOScale(new Vector3(1f, 1f, 1f), 0.5f))
                .Insert(0.4f, m_otherPanel.DialogueText.DOFade(1f, 0.2f));
        }

        sequence.Play();
    }

    void HideSequence(Transform _speakerImage, RectTransform _panel)
    {
        
    }

    void Hide()
    {
        // hide the dialogue
        m_active = false;
        m_currentSequence = DOTween.Sequence();
        m_currentSequence
            .Insert(0f, m_canvasGroup.DOFade(0f, 0.3f))
            .Insert(0.1f, m_currentPanel.SpeakerImage.transform.DOScale(0f, 0.2f))
            .Insert(0.1f, m_currentPanel.DialoguePanel.DOScale(0f,0.2f))
            .Insert(0.2f, m_currentPanel.DialogueText.DOFade(0f, 0.2f))
            .Insert(0.3f, m_otherPanel.SpeakerImage.transform.DOScale(0F, 0.2f))
            .Insert(0.3f, m_otherPanel.DialoguePanel.DOScale(0f, 0.5f))
            .Insert(0.4f, m_otherPanel.DialogueText.DOFade(0f, 0.2f));
        m_currentSequence.OnComplete(() =>
        {
            m_canvasGroup.alpha = 0;
            m_canvasGroup.blocksRaycasts = false;
            //m_data = null;
            dialogueIndex = 0;
            DialogueFinished?.Invoke();
            m_npcDialogue.DialogueText.text = "";
            m_playerDialogue.DialogueText.text = "";
            m_currentSequence = null;
        });
        m_currentSequence.Play();
    }
    
    private void ShowNext()
    {
        if (!m_active) return;
        dialogueIndex++;
        if (dialogueIndex > m_data.dialogueDataList.Count-1)
        {
            Debug.Log("Hiding dialogue");
            Hide();
        }
        else
        {
            var dialogue = m_data.dialogueDataList[dialogueIndex];
            if (dialogue.Speaker != m_currentSpeaker)
            {
                if (dialogue.Speaker == m_playerSpeakerRef)
                {
                    // is player
                    m_currentPanel =  m_playerDialogue;
                    m_otherPanel = m_npcDialogue;
                }
                else
                {
                    // change to other panel
                    m_currentPanel = m_npcDialogue;
                    m_otherPanel = m_playerDialogue; 
                }
                // hide other panel
                var hideShowSequence = DOTween.Sequence();

                if (dialogue.Speaker)
                {
                    m_currentPanel.SpeakerImage.sprite = dialogue.Speaker.GetEmotion(dialogue.Emotion);
                }

                hideShowSequence
                    // change active panel to inactive
                    .Insert(0f, m_otherPanel.DialoguePanelBg.DOColor(m_inactivePanelColour, 0.2f))
                    .Insert(0f, m_otherPanel.SpeakerImage.DOColor(m_inactiveSpeakerColour, 0.2f))
                    .Insert(0f, m_otherPanel.DialogueText.DOFade(0f, 0.1f).OnComplete(() =>
                    {
                        m_otherPanel.DialogueText.text = "";
                    }))
                    
                    .Insert(0.1f, m_otherPanel.DialoguePanel.DOScale(0.9f, 0.1f).SetEase(Ease.InOutQuad).OnComplete(() =>
                    {
                        m_currentPanel.DialogueText.text = dialogue.dialogueLine;
                        m_currentPanel.DialoguePanel.transform.SetAsLastSibling();
                    }))
                    .Insert(0f, m_otherPanel.SpeakerImage.transform.DOScale(0.9f, 0.2f))
                    // change inactive to active
                    .Insert(0.2f, m_currentPanel.DialoguePanelBg.DOColor(new Color(255f, 255f, 255f, 255f), 0.2f))
                    .Insert(0.2f, m_currentPanel.SpeakerImage.DOColor(new Color(255f, 255f, 255f, 255f), 0.2f))
                    .Insert(0.3f, m_currentPanel.DialoguePanel.DOScale(1f, 0.1f).SetEase(Ease.InOutQuad))
                    .Insert(0.2f, m_currentPanel.SpeakerImage.transform.DOScale(1f, 0.2f).SetEase(Ease.InOutQuad))
                    .Append(m_currentPanel.DialogueText.DOFade(1f, 0.1f));
                
                hideShowSequence.Play();
                m_currentSpeaker = dialogue.Speaker;
            }
            else
            {
                m_currentPanel.DialogueText.text = dialogue.dialogueLine;
            }
        }
    }
    
    public void NextLine()
    {
        ShowNext();
    }
}
