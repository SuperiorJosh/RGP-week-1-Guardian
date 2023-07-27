using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverItem : MonoBehaviour
{
    public void SendItem(ItemData _itemData)
    {
        if (Inventory.Instance.HasItem(_itemData))
        {
            Debug.Log($"Item {_itemData.Name} already in inventory.");
            return;
        }
        Inventory.Instance.AddItem(_itemData);
    }
}
