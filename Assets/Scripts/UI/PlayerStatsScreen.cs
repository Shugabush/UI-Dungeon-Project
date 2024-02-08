using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsScreen : MonoBehaviour
{
    // Assign these in the inspector
    [SerializeField] Image armorIcon;
    [SerializeField] Image weaponIcon;
    [SerializeField] BaseSlot armorSlot;
    [SerializeField] BaseSlot weaponSlot;
    [SerializeField] InventorySlot[] slots = new InventorySlot[0];
    [SerializeField] InventoryLayout inventory;

    public static Image ArmorIcon => instance.armorIcon;
    public static Image WeaponIcon => instance.weaponIcon;
    public static BaseSlot ArmorSlot => instance.armorSlot;
    public static BaseSlot WeaponSlot => instance.weaponSlot;
    public static InventoryLayout Inventory => instance.inventory;

    static PlayerStatsScreen instance;

    void Awake()
    {
        instance = this;
    }

    public static void SelectWeapon(WeaponItem weapon)
    {
        if (WeaponSlot.Item != null)
        {
            // Add the existing weapon item back to the storage
            StorageScreen.Unequip(WeaponSlot as InventorySlot);
        }

        WeaponIcon.sprite = weapon.SpriteForPlayer;
        WeaponIcon.enabled = true;
        WeaponSlot.Item = weapon;
        GameManager.ExtraFightSuccess = ((WeaponItem)WeaponSlot.Item).Strength;
    }

    public static void SelectArmor(ArmorItem armor)
    {
        if (ArmorSlot.Item != null)
        {
            // Add the existing armor item back to the storage
            StorageScreen.Unequip(ArmorSlot as InventorySlot);
        }

        ArmorIcon.sprite = armor.SpriteForPlayer;
        ArmorIcon.enabled = true;
        ArmorSlot.Item = armor;
        GameManager.Armor = ((ArmorItem)ArmorSlot.Item).ArmorValue;
    }

    public static void RemoveWeapon()
    {
        WeaponIcon.sprite = null;
        WeaponIcon.enabled = false;
        GameManager.ExtraFightSuccess = 0;
    }

    public static void RemoveArmor()
    {
        ArmorIcon.sprite = null;
        ArmorIcon.enabled = false;
        GameManager.Armor = 0;
    }

    public static void Equip(InventorySlot slot)
    {
        if (slot == null || slot.Item == null) return;

        if (slot.Item.GetType() == typeof(WeaponItem))
        {
            SelectWeapon((WeaponItem)slot.Item);
        }
        else if (slot.Item.GetType() == typeof(ArmorItem))
        {
            SelectArmor((ArmorItem)slot.Item);
        }
        else
        {
            InventorySlot targetSlot = GetSlotWithItem(slot.Item);
            if (targetSlot != null)
            {
                targetSlot.AddItem(slot.Item);
            }
        }

        slot.RemoveItem();
    }

    /// <summary>
    /// Adds the given item to the proper slot
    /// </summary>
    public static void AddItem(ItemObject item)
    {
        if (item == null) return;

        if (item.GetType() == typeof(WeaponItem))
        {
            SelectWeapon((WeaponItem)item);
        }
        else if (item.GetType() == typeof(ArmorItem))
        {
            SelectArmor((ArmorItem)item);
        }
        else
        {
            InventorySlot targetSlot = GetSlotWithItem(item);
            if (targetSlot != null)
            {
                targetSlot.AddItem(item);
            }
        }
    }

    /// <summary>
    /// Get the next empty slot (besides the weapon and armor slot)
    /// </summary>
    public static InventorySlot GetNextEmptySlot()
    {
        foreach (var slot in instance.slots)
        {
            if (slot.Empty)
            {
                return slot;
            }
        }
        return null;
    }

    public static InventorySlot GetSlotWithItem(ItemObject targetItem)
    {
        foreach (var slot in instance.slots)
        {
            if (slot.Item == targetItem)
            {
                return slot;
            }
        }
        return GetNextEmptySlot();
    }

    public static int GetAdditionalFightSuccess()
    {
        if (WeaponSlot.Item == null)
        {
            return 0;
        }

        return (int)((WeaponSlot.Item as WeaponItem).Strength * 100);
    }
}
