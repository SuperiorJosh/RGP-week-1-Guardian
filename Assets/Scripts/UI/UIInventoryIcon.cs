using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIInventoryIcon : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image m_iconImage;
    [SerializeField] private Image m_iconBgImage;
    private bool m_active = false;
    public bool Active => m_active;
    private ItemData m_itemData;
    public ItemData ItemData => m_itemData;

    private CanvasGroup m_canvasGroup;

    [FormerlySerializedAs("m_onClicked")] public UnityEvent<UIInventoryIcon> OnClicked;
    [SerializeField] 

    private void Awake()
    {
        m_canvasGroup = GetComponent<CanvasGroup>();
        m_canvasGroup.alpha = 0;
        m_canvasGroup.blocksRaycasts = false;
    }

    private void Start()
    {
    }

    public void Setup(ItemData item)
    {
        OnClicked.RemoveAllListeners();
        m_itemData = item;
        m_iconImage.sprite = item.InventoryIcon;
        m_canvasGroup.alpha = 1f;
        m_active = false;
        m_iconBgImage.color = new Color(m_iconBgImage.color.r, m_iconBgImage.color.g, m_iconBgImage.color.b, 0f);
        m_iconBgImage.transform.localScale = Vector3.zero;
        m_canvasGroup.blocksRaycasts = true;
    }

    private void OnDisable()
    {
        
    }

    public void Reset()
    {
        m_active = false;
        m_canvasGroup.alpha = 0f;
        m_canvasGroup.blocksRaycasts = false;
        m_iconBgImage.color = new Color(m_iconBgImage.color.r, m_iconBgImage.color.g, m_iconBgImage.color.b, 0f);
        m_iconBgImage.transform.localScale = Vector3.zero;
        m_itemData = null;
    }

    public void ChangeActiveState(bool active)
    {
        m_active = active;
        ShowHideActiveBg();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        m_active = !m_active;
        ShowHideActiveBg();
        OnClicked?.Invoke(this);
    }

    private void ShowHideActiveBg()
    {
        var endVal = m_active ? 1f : 0f;
        var endSize = m_active ? 1f : 0f;
        m_iconBgImage.DOFade(endVal, 0.2f);
        m_iconBgImage.transform.DOScale(endSize, 1.5f).SetEase(Ease.OutElastic);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }
}
