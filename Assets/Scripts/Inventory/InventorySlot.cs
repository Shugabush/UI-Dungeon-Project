using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventorySlot : BaseSlot
{
    [SerializeField] TMP_Text countText;

    int count = 0;

    public int Count
    {
        get
        {
            return count;
        }
        set
        {
            count = value;
            if (value <= 0)
            {
                Item = null;
                countText.text = string.Empty;
            }
            else
            {
                countText.text = count.ToString();
            }
        }
    }

    public bool Empty => Count <= 0;

    public void AddItem(ItemObject newItem)
    {
        if (Empty)
        {
            Item = newItem;
            Count++;
        }
        else if (Item == newItem)
        {
            Count++;
        }
    }

    public void RemoveItem()
    {
        if (!Empty)
        {
            Count--;
        }
        else
        {
            Count = 0;
        }

        if (this == PlayerStatsScreen.WeaponSlot)
        {
            PlayerStatsScreen.RemoveWeapon();
        }
        else if (this == PlayerStatsScreen.ArmorSlot)
        {
            PlayerStatsScreen.RemoveArmor();
        }
    }
}
