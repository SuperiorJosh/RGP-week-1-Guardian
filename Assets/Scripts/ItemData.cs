using Sirenix.OdinInspector;
using UnityEngine;

public class ItemData : ScriptableObject
{
    private string m_name = "";
    private string m_description = "";
    private Sprite m_sprite;
    private GameObject m_prefab;
    
    public string Name => m_name;
    
    public GameObject ItemPrefab => m_prefab;
    
    // for instances - want to ensure we're not messing with the original
    private ItemData m_originalDataRef;
    public ItemData OriginalRef => m_originalDataRef;

    public bool IsInstance => m_originalDataRef != null; 
    
    public static ItemData CreateInstance(ItemData original)
    {
        ItemData instance = Instantiate(original);
        instance.m_originalDataRef = original;
        return instance;
    }
    
    public bool IsInstanceOf(ItemData itemData)
    {
        return itemData == m_originalDataRef;
    }
}