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

    public static Image ArmorIcon => instance.armorIcon;
    public static Image WeaponIcon => instance.weaponIcon;
    public static BaseSlot ArmorSlot => instance.armorSlot;
    public static BaseSlot WeaponSlot => instance.weaponSlot;

    static PlayerStatsScreen instance;

    void Awake()
    {
        instance = this;
    }

    public static void SelectWeapon(WeaponItem weapon)
    {
        WeaponIcon.sprite = weapon.SpriteForPlayer;
        WeaponIcon.enabled = true;
        WeaponSlot.Item = weapon;
    }

    public static void SelectArmor(ArmorItem armor)
    {
        ArmorIcon.sprite = armor.SpriteForPlayer;
        ArmorIcon.enabled = true;
        ArmorSlot.Item = armor;
    }

    public static void RemoveWeapon()
    {
        WeaponIcon.sprite = null;
        WeaponIcon.enabled = false;
    }

    public static void RemoveArmor()
    {
        ArmorIcon.sprite = null;
        ArmorIcon.enabled = false;
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
                GetSlotWithItem(slot.Item).AddItem(slot.Item);
            }
        }

        slot.RemoveItem();
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
