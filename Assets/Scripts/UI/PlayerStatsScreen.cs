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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectWeapon(WeaponItem weapon)
    {
        weaponIcon.sprite = weapon.SpriteForPlayer;
    }

    public void SelectArmor(ArmorItem armor)
    {
        armorIcon.sprite = armor.SpriteForPlayer;
    }

    /// <summary>
    /// Get the next empty slot (besides the weapon and armor slot)
    /// </summary>
    public InventorySlot GetNextEmptySlot()
    {
        foreach (var slot in slots)
        {
            if (slot.Empty)
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
        return null;
    }
}
