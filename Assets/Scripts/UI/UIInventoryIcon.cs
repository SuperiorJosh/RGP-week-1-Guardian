using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIInventoryIcon : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image m_iconImage;
    private bool m_active = false;
    public bool Active => m_active;
    private ItemData m_itemData;
    public ItemData ItemData => m_itemData;

    [FormerlySerializedAs("m_onClicked")] public UnityEvent<UIInventoryIcon> OnClicked;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void Setup(ItemData item)
    {
        m_itemData = item;
        m_active = true;
        m_iconImage.sprite = item.InventoryIcon;
        gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        m_active = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClicked?.Invoke(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }
}
