using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ItemDatabase : IDatabase<ItemData>
{
    [ShowInInspector] private Dictionary<string, ItemData> m_db = new();
    public void Init()
    {
        m_db = new();
        var assets = DatabaseHelpers<ItemData>.GetAssets("Items");
        foreach (var asset in assets)
        {
            m_db.Add(asset.Name,asset);
        }

        Debug.Log($"Loaded <b>{assets.Count}</b> Items");
    }

    public ItemData GetOriginal(ItemData data)
    {
        foreach (var itemData in m_db)
        {
            if (itemData.Value == data)
            {
                // is original
                return itemData.Value;
            }

            if (data.IsInstanceOf(itemData.Value))
            {
                // found original from clone
                return itemData.Value;
            }
        }

        return null;
    }
    
    public void GetInstance(ItemData data)
    {
        if (data.IsInstance)
        {
            
        }
    }
}