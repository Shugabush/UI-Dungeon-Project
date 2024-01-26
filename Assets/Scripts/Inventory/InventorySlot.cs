using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : Button
{
    public ItemObject occupiedItem;
    public int count = 0;

    public bool Empty => count == 0;

    public void AddItem(ItemObject newItem)
    {
        if (Empty)
        {
            occupiedItem = newItem;
            count++;
        }
        else if (occupiedItem == newItem)
        {
            count++;
        }
    }

    public void RemoveItem()
    {
        if (!Empty)
        {
            count--;
            if (Empty)
            {
                occupiedItem = null;
            }
        }
    }
}
