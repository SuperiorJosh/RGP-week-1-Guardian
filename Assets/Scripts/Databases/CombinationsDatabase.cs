using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class CombinationsDatabase : IDatabase<CombinationData>
{
    [ShowInInspector] private Dictionary<string, CombinationData> m_db;
    public void Init()
    {
        m_db = new();
        var assets = DatabaseHelpers<CombinationData>.GetAssets("Combinations");
        foreach (var asset in assets)
        {
            m_db.Add(asset.name, asset);
        }
        
        Debug.Log($"Loaded <b>{assets.Count}</b> Item Combinations");
    }

    public CombinationData FindFromItems(ItemData item1, ItemData item2)
    {
        foreach (var data in m_db)
        {
            var item1Original = item1.IsInstance ? item1.OriginalRef : item1;
            var item2Original = item2.IsInstance ? item2.OriginalRef : item2;
            if (item1Original == data.Value.InputOne && item2Original == data.Value.InputTwo ||
                item2Original == data.Value.InputOne && item1Original == data.Value.InputTwo)
            {
                return data.Value;
            }
        }

        return null;
    }

    public void GetInstance(CombinationData data)
    {
        
    }

    public IEnumerable<CombinationData> GetAll()
    {
        return null;
    }

    public CombinationData GetSingle()
    {
        return null;
    }
}