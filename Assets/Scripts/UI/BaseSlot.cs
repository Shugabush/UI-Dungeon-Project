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
                icon.enabled = true;
            }
            else
            {
                icon.sprite = null;
                icon.enabled = false;
            }
        }
    }

    protected virtual void Awake()
    {
        if (item != null)
        {
            icon.sprite = item.Sprite;
            icon.enabled = true;
        }
        Rt = transform as RectTransform;
    }

    protected virtual void OnValidate()
    {
        if (item != null)
        {
            icon.sprite = item.Sprite;
            icon.enabled = true;
        }
        else
        {
            icon.sprite = null;
            icon.enabled = false;
        }
        if (Rt == null)
        {
            Rt = transform as RectTransform;
        }
    }
}
