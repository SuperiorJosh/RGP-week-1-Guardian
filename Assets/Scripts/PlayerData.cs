using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerData : MonoBehaviour
{
    // Player variables
    private bool ghostVisionActive = false;

    public ItemData itemData { get; set; }

    // Unity event
    public UnityEvent<bool> GhostVisionToggle;

    public void ToggleGhostVision()
    {
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
