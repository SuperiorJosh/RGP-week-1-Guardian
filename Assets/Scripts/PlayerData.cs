using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerData : MonoBehaviour
{
    // Player variables
    private bool ghostVisionActive = false;
    private bool m_ghostVisionEnabled = false;

    [SerializeField] private GameStepEvent m_ghostVisionAvailableStepEvent;
    
    public ItemData itemData { get; set; }

    // Unity event
    public UnityEvent<bool> GhostVisionToggle;

    private void Start()
    {
        m_ghostVisionAvailableStepEvent.StepEventChanged.AddListener(OnStepEvent);
    }

    private void OnStepEvent(GameStepEvent stepEvent)
    {
        if (stepEvent.CurrentState != GameStepEventState.Completed) return;
        m_ghostVisionEnabled = true;
    }

    public void ToggleGhostVision()
    {
        if (!m_ghostVisionEnabled) return;
        if (ghostVisionActive)
        {
            ghostVisionActive = false;
        }
        else
        {
            ghostVisionActive = true;
        }

        GhostVisionToggle.Invoke(ghostVisionActive);
    }
}
