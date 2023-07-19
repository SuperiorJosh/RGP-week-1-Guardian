using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    ItemData itemData;
    
    void Awake()
    {
        itemData = GetComponent<ItemData>();
    }

    public void PickUpObject()
    {
        Inventory.Instance.AddItem(itemData);
        Destroy(gameObject);
    }
}
