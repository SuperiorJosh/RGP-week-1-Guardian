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

    [FormerlySerializedAs("m_onClicked")] public UnityEvent<UIInventoryIcon> OnClicked;

    private void Start()
    {
        gameObject.SetActive(false);
        m_iconBgImage.DOFade(0f, 0f);
    }

    public void Setup(ItemData item)
    {
        m_itemData = item;
        m_iconImage.sprite = item.InventoryIcon;
        gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        m_active = false;
    }

    public void Reset()
    {
        m_active = false;
        gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClicked?.Invoke(this);
        
        m_active = !m_active;
        var endVal = m_active ? 1f : 0f;
        m_iconBgImage.DOFade(endVal, 0.2f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }
}
