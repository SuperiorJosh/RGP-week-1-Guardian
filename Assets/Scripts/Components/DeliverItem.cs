using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverItem : MonoBehaviour
{
    public void SendItem(ItemData _itemData)
    {        
        Inventory.Instance.AddItem(_itemData);
    }
}
