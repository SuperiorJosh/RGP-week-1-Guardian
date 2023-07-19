using Sirenix.OdinInspector;using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Item Data", fileName = "Item")]
public class ItemData : ScriptableObject
{
    [SerializeField] private string m_name = "";
    [SerializeField] private string m_description = "";
    [SerializeField] private Sprite m_sprite;
    [SerializeField] private GameObject m_prefab;
    
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