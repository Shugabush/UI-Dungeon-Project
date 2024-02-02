using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventorySlot : BaseSlot
{
    [SerializeField] TMP_Text countText;

    int count = 0;

    int Count
    {
        get
        {
            return count;
        }
        set
        {
            count = value;
            if (Empty)
            {
                Item = null;
                icon.sprite = null;
                countText.text = string.Empty;
            }
            else
            {
                countText.text = count.ToString();
            }
        }
    }

    public bool Empty => Count == 0;

    public void MoveToOtherSlot(InventorySlot otherSlot)
    {
        if (otherSlot == null) return;

        otherSlot.AddItem(Item);
        RemoveItem();
    }

    public void AddItem(ItemObject newItem)
    {
        if (Empty)
        {
            Item = newItem;
            Count++;
        }
        else if (Item == newItem)
        {
            Count++;
        }
    }

    public void RemoveItem()
    {
        if (!Empty)
        {
            Count--;
        }
    }
}
