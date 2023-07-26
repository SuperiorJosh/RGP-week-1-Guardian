using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }
    private List<ItemData> m_items = new();

    public UnityEvent<ItemData> ItemAdded;
    public UnityEvent<ItemData> ItemRemoved;
    public UnityEvent<CombinationData> ItemsCombined; 

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            
        }
        else
        {
            Instance = this;
        }
    }

    public void AddItem(ItemData item, bool fireEvent = true)
    {
        if (item == null)
        {
            Debug.Log("Attempting to add NULL item");
            return;
        }
        // TODO: Refactor into some form of Item Database to generate instances
        var itemInstance = ItemData.CreateInstance(item);
        m_items.Add(itemInstance);
        
        if (fireEvent)
            ItemAdded?.Invoke(itemInstance);
    }

    public void RemoveItem(ItemData itemRef)
    {
        var item = itemRef.IsInstance ? itemRef.OriginalRef : itemRef;

        ItemData foundItem = null; 
        foreach (var itemData in m_items.Where(itemData => itemData.IsInstanceOf(item)))
        {
            foundItem = itemData;
            ItemRemoved?.Invoke(foundItem);
            break;
        }

        if (foundItem != null)
            m_items.Remove(foundItem);
    }

    public bool ValidateCombination(ItemData itemOne, ItemData itemTwo)
    {
        var combination = Databases.Instance.Combinations.FindFromItems(itemOne, itemTwo);
        return combination != null;
    }
    
    public CombinationData CombineItems(ItemData itemOne, ItemData itemTwo)
    {
        var combination = Databases.Instance.Combinations.FindFromItems(itemOne, itemTwo);
        if (combination == null) return null;
        RemoveItem(itemOne);
        RemoveItem(itemTwo);
        AddItem(combination.Output, true);
        ItemsCombined?.Invoke(combination);
        return combination;

    }
}