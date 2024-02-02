using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsScreen : MonoBehaviour
{
    // Assign these in the inspector
    [SerializeField] Image armorIcon;
    [SerializeField] Image weaponIcon;
    [SerializeField] InventorySlot[] slots = new InventorySlot[0];

    public static Image ArmorIcon => instance.armorIcon;
    public static Image WeaponIcon => instance.weaponIcon;

    static PlayerStatsScreen instance;

    void Awake()
    {
        instance = this;
    }

    public static void SelectWeapon(WeaponItem weapon)
    {
        WeaponIcon.sprite = weapon.SpriteForPlayer;
    }

    public static void SelectArmor(ArmorItem armor)
    {
        ArmorIcon.sprite = armor.SpriteForPlayer;
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
