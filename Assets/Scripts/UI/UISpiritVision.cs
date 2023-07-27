using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISpiritVision : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Image m_image;
    [SerializeField] private GameStepEvent m_visionEnabledEvent;

    [SerializeField] private Sprite m_ghostVisionActiveSprite;

    [SerializeField] private Sprite m_ghostVisionInactiveSprite;
    [SerializeField] private Vector2 m_hoverOffset;

    private UnityEvent m_cursorExited;
    private bool m_active = false;
    public bool Active => m_active;
    private PlayerData m_playerData;

    private void Awake()
    {
        m_playerData = FindObjectOfType<PlayerData>();
        m_playerData.GhostVisionToggle.AddListener(OnToggleGhostVision);
    }

    private void OnToggleGhostVision(bool toggled)
    {
        m_image.sprite = toggled ? m_ghostVisionActiveSprite : m_ghostVisionInactiveSprite;
    }

    private void Start()
    {
        m_image.color = new Color(255f, 255f, 255, 0f);
        m_visionEnabledEvent.StepEventChanged.AddListener(OnStepEvent);
    }

    private void OnStepEvent(GameStepEvent stepEvent)
    {
        if (stepEvent.CurrentState != GameStepEventState.Completed) return;
        m_image.DOFade(1f, 1f);
        m_active = true;
        // TODO: Show a pretty highlight effect on it
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!m_active) return;
        DialogueManager.Instance.ShowAltText("Spirit Vision (Shift)", transform, m_hoverOffset, true, m_cursorExited);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!m_active) return;
        m_cursorExited?.Invoke();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!m_active) return;
        m_playerData.ToggleGhostVision();
    }
}
