using System;
using System.Collections;
using System.Collections.Generic;
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
    private int linesIndex;

    private RectTransform m_rectTransform;
    [SerializeField] private RectTransform m_textPanel;

    public bool IsActive => m_data != null;
    
    private void Awake()
    {
        m_canvasGroup = GetComponent<CanvasGroup>();
        m_rectTransform = transform as RectTransform;
    }

    private void Start()
    {
        m_canvasGroup.alpha = 0;
        m_canvasGroup.blocksRaycasts = false;
        m_text.text = "";
        m_textPanel.localScale = new Vector3(0, 0, 0);
        m_speakerImage.transform.localScale = new Vector3(0, 0, 0);
    }

    public void ShowDialogue(DialogueData dialogue)
    {
        linesIndex = 0;
        m_canvasGroup.blocksRaycasts = true;
        
        m_data = dialogue;
        var firstDialogue = m_data.dialogueDataList[0];
        m_currentSpeaker = firstDialogue.Speaker;
        m_speakerImage.sprite = firstDialogue.Speaker.GetEmotion(firstDialogue.Emotion);
        var sequence = DOTween.Sequence()
            .Insert(0f, m_speakerImage.transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f))
            .Insert(0.1f, m_canvasGroup.DOFade(1f, 0.2f))
            .Insert(0.1f, m_textPanel.DOScale(new Vector3(1f, 1f, 1f), 0.5f));

        sequence.OnComplete(() =>
        {
            m_text.text = firstDialogue.dialogueLine;
            linesIndex++;
        });
        
        sequence.Play();
    }

    private void ShowNext()
    {
        linesIndex++;
        if (linesIndex < m_data.dialogueDataList.Count)
        {
            var next = m_data.dialogueDataList[linesIndex];
            if (next.Speaker != null && next.Speaker != m_currentSpeaker)
            {
                m_currentSpeaker = next.Speaker;
            }
            m_speakerImage.sprite = m_currentSpeaker.GetEmotion(next.Emotion);
            m_text.text = next.dialogueLine;
        }
        else
        {
            var sequence = DOTween.Sequence();
            sequence.Insert(0, m_rectTransform.DOScale(new Vector3(0f, 0f, 0f), 0.5f));
            sequence.Insert(0.3f, m_canvasGroup.DOFade(0f, 0.2f));
            sequence.OnComplete(() =>
            {
                m_canvasGroup.alpha = 0;
                m_canvasGroup.blocksRaycasts = false;
                m_data = null;
                DialogueFinished?.Invoke();
            });
        }
    }
    
    public void NextLine()
    {
        ShowNext();
    }
}
