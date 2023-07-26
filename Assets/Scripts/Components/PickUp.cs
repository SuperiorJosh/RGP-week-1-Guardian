using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickUp : MonoBehaviour
{
    Interactable interactable;
    [SerializeField] ItemData itemData;

    public UnityEvent eventOnPickUp;
    
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

        eventOnPickUp?.Invoke();

        Destroy(gameObject);
    }
}
