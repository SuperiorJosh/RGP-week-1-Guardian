using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "", menuName = "Game/Combine Data")]
public class CombinationData : ScriptableObject
{
    [InlineEditor(InlineEditorModes.FullEditor)]
    [SerializeField] private ItemData m_inputItem1;
    [InlineEditor(InlineEditorModes.FullEditor)]
    [SerializeField] private ItemData m_inputItem2;
    [InlineEditor(InlineEditorModes.FullEditor)]
    [SerializeField] private ItemData m_outputItem;
    //[SerializeField] UnityEvent m_usableEvent;

    public ItemData InputOne => m_inputItem1;
    public ItemData InputTwo => m_inputItem2;
    public ItemData Output => m_outputItem;
    //public UnityEvent Event => m_usableEvent;
    public DialogueData dialogueData;

    [Button]
    void CreateOutput()
    {
        
    }
    
    
    public static void CreateNew(ItemData inputOne, ItemData inputTwo)
    {
        #if UNITY_EDITOR
        var combination = CreateInstance(typeof(CombinationData)) as CombinationData;
        combination.m_inputItem1 = inputOne;
        combination.m_inputItem2 = inputTwo;
        AssetDatabase.CreateAsset(combination, $"Assets/Resources/Combinations/{inputOne.Name}{inputTwo.Name}Combination.asset");
        AssetDatabase.SaveAssets();
        Selection.activeObject = AssetDatabase.LoadAssetAtPath<Object>(AssetDatabase.GetAssetPath(combination));
        #endif
    }
}