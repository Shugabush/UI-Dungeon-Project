using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : BaseSlot
{
    public void AddItem(ItemObject newItem)
    {
        Item = newItem;
    }

    public void RemoveItem(bool sell = false)
    {
        if (this == PlayerStatsScreen.WeaponSlot)
        {
            PlayerStatsScreen.RemoveWeapon();
        }
        else if (this == PlayerStatsScreen.ArmorSlot)
        {
            PlayerStatsScreen.RemoveArmor();
        }

        if (sell)
        {
            GameManager.Gold += item.GoldValue;
        }
        Item = null;
    }
}
