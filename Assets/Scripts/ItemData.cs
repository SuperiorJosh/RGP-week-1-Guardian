using System;
using Sirenix.OdinInspector;using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Item Data", fileName = "Item")]
public class ItemData : ScriptableObject
{
    [SerializeField] private string m_name = "";
    [SerializeField] private string m_description = "";
    [SerializeField] private GameObject m_prefab;
    
    [SerializeField] private Sprite m_inventoryIcon;

    public string Name => m_name;
    public string Description => m_description;
    public Sprite InventoryIcon => m_inventoryIcon;
    public GameObject ItemPrefab => m_prefab;
    
    // for instances - want to ensure we're not messing with the original
    private ItemData m_originalDataRef;
    public ItemData OriginalRef => m_originalDataRef;

    public bool IsInstance => m_originalDataRef != null;

    public static ItemData CreateInstance(ItemData original)
    {
        ItemData instance = null;
        var originalRef = Databases.Instance.Items.GetOriginal(original);
        if (originalRef != null)
        {
            instance = Instantiate(originalRef);
            instance.m_originalDataRef = original;
        }

        return instance;
    }
    
    public bool IsInstanceOf(ItemData itemData)
    {
        return itemData == m_originalDataRef;
    }

    [Button]
    public void CreateCombination(ItemData otherItem)
    {
        if (otherItem == null)
        {
            return;
        }
        CombinationData.CreateNew(this, otherItem);
    }
}