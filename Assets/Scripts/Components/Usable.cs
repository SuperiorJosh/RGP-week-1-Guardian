using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Usable : MonoBehaviour
{
    //Interactable interactable;    

    [SerializeField] bool requiresItem;
    [SerializeField] ItemData requiredItemData;
    
    public UnityEvent<ItemData> usableEvent; // Unity event for when usable is successful.

    public void UseItem(ItemData _clickedItem)
    {
        if(requiresItem && requiredItemData != null)
        {
            if(_clickedItem == requiredItemData  && InteractionManager.Instance.itemWasClicked)
            {
                Debug.Log("Correct item used");
                // Invoke event attached to TV?
                usableEvent?.Invoke(_clickedItem);
            }
            else{
                Debug.Log("Wrong item used");
            }
        }
        else{
            // TODO: Add any necessary logic for cases where an item is not required.
        }        
    }
}
