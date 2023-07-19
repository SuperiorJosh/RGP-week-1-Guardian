using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Usable : MonoBehaviour
{
    Interactable interactable;
    ItemData itemData;
    
    void Awake()
    {
        interactable = GetComponent<Interactable>();
    }

    void Start()
    {
        interactable.ItemInteraction.AddListener(UseObject);
    }

    void UseObject(ItemData _heldItemData)
    {
        // Remove listener
        interactable.ItemInteraction.RemoveListener(UseObject);
        
        // Add functionality here.

        Destroy(gameObject);
    }
}
