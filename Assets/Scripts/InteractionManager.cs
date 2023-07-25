using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance { get; private set; }
    //public bool itemWasClicked = false;
    public bool useButtonClicked = false;
    public ItemData clickedItemData;

    public UnityEvent<Transform> InteractionTargetChanged;

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

    public void SetInteractionTarget(Transform target)
    {
        InteractionTargetChanged?.Invoke(target);
    }
}
