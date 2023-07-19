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
    }

    private void OnInventoryItemRemoved()
    {
        
    }
}
