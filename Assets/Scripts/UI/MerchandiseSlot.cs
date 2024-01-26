using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MerchandiseSlot : Button
{
    [SerializeField] ItemObject item;
    public ItemObject Item => item;

    [SerializeField] Image icon;

    protected override void Awake()
    {
        base.Awake();

        if (item != null)
        {
            icon.sprite = item.Sprite;
        }
    }
}
