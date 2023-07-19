using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIInventory : MonoBehaviour
{   
    //
    private List<UIInventoryIcon> m_icons = new();
    private List<UIInventoryIcon> m_activeIcons = new();

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
        var icon = m_icons.FirstOrDefault();
        m_activeIcons.Add(icon);
        if (icon == null) return;
        
        icon.Setup(itemData);
        icon.OnClicked.AddListener(OnIconClicked);
    }

    private void OnIconClicked(UIInventoryIcon icon)
    {
        Debug.Log($"Item Clicked: {icon.ItemData.Name}");
        // show popup window
    }

    private void OnInventoryItemRemoved()
    {
        
    }
}
