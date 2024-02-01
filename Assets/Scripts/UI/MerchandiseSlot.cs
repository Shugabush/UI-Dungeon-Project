using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MerchandiseSlot : MonoBehaviour
{
    // Assign these in the inspector
    [field: SerializeField] 
    public Button button { get; private set; }
    [SerializeField] Image icon;

    [SerializeField] ItemObject item;

    public RectTransform Rt { get; private set; }

    public ItemObject Item => item;

    void Awake()
    {
        if (item != null)
        {
            icon.sprite = item.Sprite;
        }
        Rt = transform as RectTransform;
    }
}
