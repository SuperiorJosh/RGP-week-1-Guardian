using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UIAltTextBox : MonoBehaviour
{
    private Transform m_prevTarget;
    private Transform m_currentTarget;
    private UnityEvent m_hideEvent;
    [SerializeField] private CanvasGroup m_canvasGroup;
    [SerializeField] private TMP_Text m_text;

    private void Awake()
    {
        m_canvasGroup.alpha = 0f;
        m_canvasGroup.blocksRaycasts = false;
    }

    public void Show(string text, Transform targetObject, Vector2 offset, bool uiTarget, UnityEvent hideEvent = null)
    {
        var targetTrans = uiTarget ? targetObject as RectTransform : targetObject;
        if (targetTrans == null) return;
        Vector3 pos;
        if (targetTrans is RectTransform rectTransform)
        {
            var canvasRoot = GetComponentInParent<Canvas>();
            pos = RectTransformUtility.CalculateRelativeRectTransformBounds(canvasRoot.transform,
                rectTransform.transform).center;
        }
        else
        {
            pos = RectTransformUtility.WorldToScreenPoint(Camera.main, targetObject.transform.position);
        }

        var rectTrans = transform as RectTransform;
        var endPos = new Vector3(pos.x + offset.x, pos.y + offset.y, 0f);
        var startPos = new Vector3(endPos.x, endPos.y - 50f, 0f);
        rectTrans.anchoredPosition = startPos; 
        m_text.text = text;
        m_canvasGroup.DOFade(1f, .5f);
        rectTrans.DOAnchorPosY(endPos.y, .5f);

        if (hideEvent != null)
        {
            m_hideEvent = hideEvent;
            hideEvent.AddListener(OnHideEvent);
        }
    }
    
    private void OnHideEvent()
    {
        m_hideEvent.RemoveListener(OnHideEvent);
        m_text.text = "";
        m_canvasGroup.DOFade(0f, .5f);
    }

    private void Update()
    {
        if (!m_currentTarget) return;
    }
}
