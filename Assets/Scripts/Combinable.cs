using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combinable : MonoBehaviour
{
    Interactable interactable;
    ItemData itemData;
    
    void Awake()
    {
        interactable = GetComponent<Interactable>();
        //itemData = GetComponent<ItemData>();
    }

    void Start()
    {
        interactable.ItemInteraction.AddListener(CombineObject);
    }

    void CombineObject(ItemData _heldItemData)
    {
        if(_heldItemData == null)
        {
            return;
        }
        
        // Add functionality here.
        if(Inventory.Instance.ValidateCombination(itemData, _heldItemData))
        {
            // Remove listener
            interactable.ItemInteraction.RemoveListener(CombineObject);
        }
    }
}
