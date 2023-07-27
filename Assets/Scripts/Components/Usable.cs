using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Usable : MonoBehaviour
{
    //Interactable interactable;    

    [SerializeField] bool requiresItem;
    //[SerializeField] ItemData requiredItemData;
    //public UnityEvent<ItemData> usableEvent; // Unity event for when usable is successful.

    [System.Serializable]
    public struct ItemEventPair
    {
        public ItemData requiredItem;
        public GameStepEvent gameStepEventToCheck;
        public UnityEvent<ItemData> usableEvent;
    }
    public List<ItemEventPair> requiredItemDataList;

    public void UseItem(ItemData _clickedItem)
    {
        if (!InteractionManager.Instance.useButtonClicked)
        {
            // Don't do anything unless use button clicked.
            return;
        }

        if (requiresItem)
        {
            foreach (ItemEventPair itemEventPair in requiredItemDataList)
            {
                if (_clickedItem == itemEventPair.requiredItem && itemEventPair.gameStepEventToCheck.CurrentState != GameStepEventState.Completed)
                {
                    Debug.Log("Correct item used");
                    (itemEventPair.usableEvent)?.Invoke(_clickedItem);
                    return;
                }
            }
            Debug.Log("Item not usable on target");
        }
    }

        // if(requiresItem && requiredItemData != null)
        // {
            
            
            
        //     if(_clickedItem == requiredItemData  && InteractionManager.Instance.useButtonClicked)
        //     {
        //         Debug.Log("Correct item used");
        //         // Invoke event attached to TV?
        //         usableEvent?.Invoke(_clickedItem);
        //     }
        //     else{
        //         Debug.Log("Wrong item used");
        //     }
        // }
        // else{
        //     // TODO: Add any necessary logic for cases where an item is not required.
        // }        
}
