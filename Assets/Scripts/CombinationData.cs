using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "Game/Combine Data")]
public class CombinationData : ScriptableObject
{
    [SerializeField] private ItemData m_inputItem1;
    [SerializeField] private ItemData m_inputItem2;
    [SerializeField] private ItemData m_outputItem;
}