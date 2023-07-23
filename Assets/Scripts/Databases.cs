using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class Databases : SerializedMonoBehaviour
{
    public static Databases Instance { get; set; }

    [OdinSerialize] private ItemDatabase m_items;
    [OdinSerialize] private CombinationsDatabase m_combinations;
    [OdinSerialize] private GameStepDatabase m_stepEvents;

    public ItemDatabase Items => m_items;
    public CombinationsDatabase Combinations => m_combinations;
    public GameStepDatabase GameStepEvents => m_stepEvents;
    
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
        m_items.Init();
        m_combinations.Init();
        m_stepEvents.Init();
    }
}