using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }
    private List<ItemData> m_items = new();

    public UnityEvent<ItemData> ItemAdded;
    public UnityEvent<ItemData> ItemRemoved;

    private void Awake()
    {
        if (Instance == null || Instance != this)
        {
            Destroy(this);
            
        }
        else
        {
            Instance = this;
        }
    }

    public void AddItem(ItemData item)
    {
        // TODO: Refactor into some form of Item Database to generate instances
        var itemInstance = ItemData.CreateInstance(item);
        m_items.Add(itemInstance);
        
        ItemAdded?.Invoke(itemInstance);
    }

    public void RemoveItem(ItemData itemRef)
    {
        var item = itemRef.IsInstance ? itemRef.OriginalRef : itemRef;
        
        if (m_items.Contains(item))
        {
            m_items.Remove(item);
            ItemRemoved?.Invoke(item);
        }
    }

    public bool ValidCombination(ItemData item1, ItemData item2)
    {
        return true;
    }
}