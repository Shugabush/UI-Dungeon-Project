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
    static BaseSlot ArmorSlot => instance.armorSlot;
    static BaseSlot WeaponSlot => instance.weaponSlot;

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

    public static void Equip(BaseSlot item)
    {
        if (item.GetType() == typeof(WeaponItem))
        {
            SelectWeapon((WeaponItem)item.Item);
            item.button.interactable = false;
            return;
        }
        if (item.GetType() == typeof(ArmorItem))
        {
            SelectArmor((ArmorItem)item.Item);
            return;
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
        return null;
    }
}
