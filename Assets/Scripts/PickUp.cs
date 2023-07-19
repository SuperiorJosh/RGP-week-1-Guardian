using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    Interactable interactable;
    ItemData itemData;
    
    void Awake()
    {
        interactable = GetComponent<Interactable>();
        itemData = GetComponent<ItemData>();
    }

    void Start()
    {
        interactable.ItemInteraction.AddListener(PickUpObject);
    }

    void PickUpObject()
    {
        Inventory.Instance.AddItem(itemData);
        Destroy(gameObject);
    }
}
