using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    // Unity Events
    public UnityEvent<ItemData> ItemInteraction;

    public void Interact(ItemData _heldItemData)
    {
        ItemInteraction?.Invoke(_heldItemData);
    }
}
