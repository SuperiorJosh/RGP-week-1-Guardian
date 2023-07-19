using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIInventoryIcon : MonoBehaviour
{
    [SerializeField] private Image m_iconImage;
    private bool m_active = false;
    public bool Active => m_active;
    private ItemData m_itemData;

    public UnityEvent<UIInventoryIcon> m_onClicked;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void Setup(ItemData item)
    {
        m_itemData = item;
        m_active = true;
        gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        m_active = false;
    }
}
