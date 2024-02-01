using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [field: SerializeField]
    public Button button { get; private set; }
    public RectTransform Rt { get; private set; }
    [SerializeField] Image icon;
    [SerializeField] TMP_Text countText;

    ItemObject item;
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
                Item = null;
                icon.sprite = null;
                countText.text = string.Empty;
            }
            else
            {
                countText.text = count.ToString();
            }
        }
    }

    public ItemObject Item
    {
        get
        {
            return item;
        }
        private set
        {
            item = value;
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

    void Awake()
    {
        Rt = transform as RectTransform;
    }

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
    }
}
