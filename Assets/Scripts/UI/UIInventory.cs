using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{   
    //
    private Dictionary<UIInventoryIcon, int> m_icons = new();
    private List<UIInventoryIcon> m_inactiveIcons = new();
    private List<UIInventoryIcon> m_activeIcons = new();

    [SerializeField] private UIInventoryPopup m_popupWindow;

    private UIInventoryIcon m_activeIcon;
    private UIInventoryIcon m_combineIcon;
    private Vector2 m_startPosition;

    private void Awake()
    {
        var count = 0;
        foreach (var icon in GetComponentsInChildren<UIInventoryIcon>())
        {
            m_icons.Add(icon, count);
            m_inactiveIcons.Add(icon);
            count++;
        }
    }

    private void Start()
    {
        Inventory.Instance.ItemAdded.AddListener(OnInventoryItemAdded);
        Inventory.Instance.ItemRemoved.AddListener(OnInventoryItemRemoved);
        m_startPosition = (transform as RectTransform).anchoredPosition;
    }

    private void OnInventoryItemAdded(ItemData itemData)
    {
        UIInventoryIcon icon = null;
        foreach (var ic in m_icons.Where(ic => ic.Key.ItemData == null))
        {
            icon = ic.Key;
            break;
        }
        if (icon == null) return;
        var item = itemData.IsInstance ? itemData.OriginalRef : itemData;
        icon.Setup(item);
        icon.OnClicked.AddListener(OnIconClicked);
    }

    private void OnIconClicked(UIInventoryIcon icon)
    {
        //Debug.Log($"Item Clicked: {icon.ItemData.Name}");
        // show popup window
        if (!m_popupWindow.Active)
        {
            m_popupWindow.Show(icon, OnIconClicked);
        }
        else
        {
            m_popupWindow.SetNewTarget(icon);            
        }
        //InteractionManager.Instance.itemWasClicked = true;
        InteractionManager.Instance.clickedItemData = icon.ItemData;
    }

    private void OnInventoryItemRemoved(ItemData data)
    {
        var itemData = data.IsInstance ? data.OriginalRef : data;
        UIInventoryIcon icon = null;
        foreach (var ic in m_icons.Where(ic => ic.Key.ItemData == itemData))
        {
            icon = ic.Key;
        }
        
        if (icon != null)
        {
            icon.OnClicked.RemoveListener(OnIconClicked);
            icon.Reset();
        }
    }

    public void Hide()
    {
        var rectTrans = GetComponent<RectTransform>();
        rectTrans.DOAnchorPosY(-700f, .6f).SetEase(Ease.InOutQuad);
    }

    public void Show()
    {
        var rectTrans = GetComponent<RectTransform>();
        rectTrans.DOAnchorPosY(m_startPosition.y, .4f).SetEase(Ease.InOutQuad);
    }
}
