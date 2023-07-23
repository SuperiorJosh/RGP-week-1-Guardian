using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "GameStepEvent", menuName = "Game/Game Step Event")]
public class GameStepEvent : ScriptableObject
{
    private GameStepEventState m_startState = GameStepEventState.NotStarted;
    private GameStepEventState m_currentState = GameStepEventState.NotStarted;
    private GameStepEventState m_prevState;
    public UnityEvent<GameStepEvent> StepEventChanged;

    public GameStepEventState CurrentState => m_currentState;
    public GameStepEventState PreviousState => m_prevState;

    private void OnEnable()
    {
        m_currentState = m_startState;
    }

    public void ChangeContext(GameStepEventState state)
    {
        m_prevState = m_currentState;
        m_currentState = state;
        StepEventChanged?.Invoke(this);
    }
}

public enum GameStepEventState
{
    NotStarted,
    Started,
    InProgress,
    Completed
}