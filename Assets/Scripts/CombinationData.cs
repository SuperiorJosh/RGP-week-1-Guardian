using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "Game/Combine Data")]
public class CombinationData : ScriptableObject
{
    [SerializeField] private ItemData m_inputItem1;
    [SerializeField] private ItemData m_inputItem2;
    [SerializeField] private ItemData m_outputItem;

    public ItemData InputOne => m_inputItem1;
    public ItemData InputTwo => m_inputItem2;
    public ItemData Output => m_outputItem;

    [Button]
    void CreateOutput()
    {
        
    }
    
    public static void CreateNew(ItemData inputOne, ItemData inputTwo)
    {
        var combination = CreateInstance(typeof(CombinationData)) as CombinationData;
        combination.m_inputItem1 = inputOne;
        combination.m_inputItem2 = inputTwo;
        AssetDatabase.CreateAsset(combination, $"Assets/Resources/Combinations/{inputOne.Name}{inputTwo.Name}Combination.asset");
        AssetDatabase.SaveAssets();
        Selection.activeObject = AssetDatabase.LoadAssetAtPath<Object>(AssetDatabase.GetAssetPath(combination));
    }
}