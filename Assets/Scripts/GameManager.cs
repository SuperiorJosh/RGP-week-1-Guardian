using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : SerializedMonoBehaviour
{
    public static GameManager Instance { get; set; }

    [ShowInInspector] private Dictionary<GameStepEvent, GameStepEventState> m_gameSteps = new();

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

    private void Start()
    {
        var events = Databases.Instance.GameStepEvents.GetAll();
        foreach (var stepEvent in events)
        {
            stepEvent.StepEventChanged.AddListener(OnGameStepChanged);
        }
    }

    private void OnGameStepChanged(GameStepEvent stepEvent)
    {
        if (m_gameSteps.ContainsKey(stepEvent))
        {
            
        }
    }
}