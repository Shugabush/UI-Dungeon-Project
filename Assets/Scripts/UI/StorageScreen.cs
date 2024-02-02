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

    public static void Unequip(InventorySlot slotToRemoveFrom)
    {
        InventorySlot targetSlot = Inventory.GetSlotWithItem(slotToRemoveFrom.Item);
        if (targetSlot != null)
        {
            targetSlot.AddItem(slotToRemoveFrom.Item);
        }

        slotToRemoveFrom.RemoveItem();
    }
}
