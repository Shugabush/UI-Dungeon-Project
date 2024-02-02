using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

    /// <summary>
    /// 
    /// </summary>
    /// <returns>Whether or not the current selected game object is one of the inventory slots</returns>
    public bool AnySlotSelected()
    {
        foreach (var slot in slots)
        {
            if (EventSystem.current.currentSelectedGameObject == slot.gameObject)
            {
                return true;
            }
        }
        return false;
    }

    public InventorySlot GetNextEmptySlot()
    {
        foreach (var slot in slots)
        {
            if (slot.Item == null)
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
            if (slot.Item == targetItem)
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
