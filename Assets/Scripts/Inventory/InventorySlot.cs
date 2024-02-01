using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] Image icon;
    [SerializeField] TMP_Text countText;

    ItemObject occupiedItem;
    int count = 0;

    int Count
    {
        get
        {
            return count;
        }
        set
        {
            count = value;
            if (Empty)
            {
                OccupiedItem = null;
                icon.sprite = null;
                countText.text = string.Empty;
            }
            else
            {
                countText.text = count.ToString();
            }
        }
    }

    public ItemObject OccupiedItem
    {
        get
        {
            return occupiedItem;
        }
        private set
        {
            occupiedItem = value;
            if (value != null)
            {
                icon.sprite = value.Sprite;
            }
            else
            {
                icon.sprite = null;
            }
        }
    }

    public bool Empty => Count == 0;

    public void AddItem(ItemObject newItem)
    {
        if (Empty)
        {
            OccupiedItem = newItem;
            Count++;
        }
        else if (OccupiedItem == newItem)
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
    }
}
