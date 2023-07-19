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
    private List<UIInventoryIcon> m_activeIcons = new();

    [SerializeField] private UIInventoryPopup m_popupWindow;

    private UIInventoryIcon m_activeIcon;
    private UIInventoryIcon m_combineIcon;

    private void Awake()
    {
        foreach (var icon in GetComponentsInChildren<UIInventoryIcon>())
        {
            m_icons.Add(icon);
        }
    }

    private void Start()
    {
        Inventory.Instance.ItemAdded.AddListener(OnInventoryItemAdded);
    }

    private void OnInventoryItemAdded(ItemData itemData)
    {
        var icon = m_icons.Find(x => x.Active == false);
        m_activeIcons.Add(icon);
        if (icon == null) return;
        
        icon.Setup(itemData);
        icon.OnClicked.AddListener(OnIconClicked);
    }

    private void OnIconClicked(UIInventoryIcon icon)
    {
        Debug.Log($"Item Clicked: {icon.ItemData.Name}");
        // show popup window
        if (!m_popupWindow.Active)
        {
            m_activeIcon = icon;
            m_popupWindow.Show(icon);
        }
        else
        {
            if (icon == m_activeIcon)
            {
                if (m_combineIcon)
                {
                    m_activeIcon = m_combineIcon;
                    m_popupWindow.Show(m_activeIcon);
                    m_combineIcon = null;
                    m_popupWindow.SetCombineTarget(m_combineIcon);
                    return;
                }

                m_activeIcon = null;
                m_popupWindow.Hide();
            }

            m_combineIcon = icon == m_combineIcon ? null : icon;
            m_popupWindow.SetCombineTarget(m_combineIcon);
        }
    }

    private void OnInventoryItemRemoved()
    {
        
    }
}
