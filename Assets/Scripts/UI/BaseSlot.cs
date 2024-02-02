using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BaseSlot : MonoBehaviour
{
    // Assign these in the inspector
    [field: SerializeField] 
    public Button button { get; private set; }
    [SerializeField] protected Image icon;

    [SerializeField] protected ItemObject item;

    public RectTransform Rt { get; private set; }

    public ItemObject Item
    {
        get
        {
            return item;
        }
        set
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

    protected virtual void Awake()
    {
        if (item != null)
        {
            icon.sprite = item.Sprite;
        }
        Rt = transform as RectTransform;
    }
}
