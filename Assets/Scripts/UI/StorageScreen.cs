using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageScreen : MonoBehaviour
{
    [SerializeField] InventoryLayout inventory;

    public static StorageScreen Instance { get; private set; }

    public static InventoryLayout Inventory => Instance.inventory;

    void Awake()
    {
        Instance = this;
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
