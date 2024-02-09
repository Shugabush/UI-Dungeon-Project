using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreScreen : MonoBehaviour
{
    [SerializeField] BaseSlot[] merchandiseSlots = new BaseSlot[0];
    static BaseSlot[] MerchandiseSlots => Instance.merchandiseSlots;

    public static StoreScreen Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    public static void EnableSlot(ItemObject targetItem)
    {
        foreach (var slot in MerchandiseSlots)
        {
            if (slot.Item == targetItem)
            {
                slot.button.interactable = true;
                break;
            }
        }
    }
}
