using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{   
    //
    private List<UIInventoryIcon> m_icons = new();
    private List<UIInventoryIcon> m_inactiveIcons = new();
    private List<UIInventoryIcon> m_activeIcons = new();

    [SerializeField] private UIInventoryPopup m_popupWindow;

    private UIInventoryIcon m_activeIcon;
    private UIInventoryIcon m_combineIcon;

    private void Awake()
    {
        foreach (var icon in GetComponentsInChildren<UIInventoryIcon>())
        {
            m_icons.Add(icon);
            m_inactiveIcons.Add(icon);
        }
    }

    private void Start()
    {
        Inventory.Instance.ItemAdded.AddListener(OnInventoryItemAdded);
        Inventory.Instance.ItemRemoved.AddListener(OnInventoryItemRemoved);
    }

    private void OnInventoryItemAdded(ItemData itemData)
    {
        var icon = m_inactiveIcons.Find(x => x.Active == false);
        if (icon == null) return;
        m_activeIcons.Add(icon);
        m_inactiveIcons.Remove(icon);
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
            m_popupWindow.Show(icon);
        }
        else
        {
            m_popupWindow.SetNewTarget(icon);
        }
    }

    private void OnInventoryItemRemoved(ItemData data)
    {
        var itemData = data.IsInstance ? data.OriginalRef : data;
        var icon = m_activeIcons.Find(x => x.ItemData == itemData);
        if (icon != null)
        {
            icon.OnClicked.RemoveListener(OnIconClicked);
            icon.Reset();
            m_activeIcons.Remove(icon);
            m_inactiveIcons.Add(icon);
        }
    }
}
