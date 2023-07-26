using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class UIDebug : MonoBehaviour
{
    [Button]
    void AddItems()
    {
        var items = Databases.Instance.Combinations.GetAll().ToList();
        for (int i = 0; i < 4; i++)
        {
            var item1 = items[i].InputOne;
            var item2 = items[i].InputTwo;
            Inventory.Instance.AddItem(item1);
            Inventory.Instance.AddItem(item2);
        }
    }
}
