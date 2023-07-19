using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    // Unity Events
    public UnityEvent ItemInteraction;

    public void Interact()
    {
        ItemInteraction?.Invoke();
    }
}
