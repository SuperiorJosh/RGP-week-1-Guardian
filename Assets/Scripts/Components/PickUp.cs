using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    Interactable interactable;
    [SerializeField] ItemData itemData;
    
    void Awake()
    {
        interactable = GetComponent<Interactable>();
    }

    void Start()
    {
        interactable.ItemInteraction.AddListener(PickUpObject);
    }

    void PickUpObject(ItemData _heldItemData)
    {
        if(_heldItemData != null)
        {
            return;
        }
        
        // Remove listener
        interactable.ItemInteraction.RemoveListener(PickUpObject);
        
        Inventory.Instance.AddItem(itemData);
        Destroy(gameObject);
    }
}
