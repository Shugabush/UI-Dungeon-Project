using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageScreen : MonoBehaviour
{
    [SerializeField] InventoryLayout inventory;

    static StorageScreen instance;

    public static InventoryLayout Inventory => instance.inventory;

    void Awake()
    {
        instance = this;
    }

    public static void Unequip(InventorySlot slot)
    {
        InventorySlot targetSlot = Inventory.GetSlotWithItem(slot.Item);
        if (targetSlot != null)
        {
            targetSlot.AddItem(slot.Item);
        }

        slot.RemoveItem();
    }
}
