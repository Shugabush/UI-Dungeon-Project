using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryLayout : MonoBehaviour
{
    InventorySlot[] slots;

    void Awake()
    {
        slots = GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public InventorySlot GetNextEmptySlot()
    {
        foreach (var slot in slots)
        {
            if (slot.OccupiedItem == null)
            {
                return slot;
            }
        }
        return null;
    }

    public InventorySlot GetSlotWithItem(ItemObject targetItem)
    {
        foreach (var slot in slots)
        {
            if (slot.OccupiedItem == targetItem)
            {
                return slot;
            }
        }
        // Just return the next empty slot
        return GetNextEmptySlot();
    }

    public void AddItem(ItemObject item)
    {
        InventorySlot targetSlot = GetSlotWithItem(item);
        if (targetSlot != null)
        {
            targetSlot.AddItem(item);
        }
    }
}
