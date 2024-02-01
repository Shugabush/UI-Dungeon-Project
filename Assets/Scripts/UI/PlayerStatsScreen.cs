using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsScreen : MonoBehaviour
{
    // Assign these in the inspector
    [SerializeField] InventorySlot weaponSlot;
    [SerializeField] InventorySlot armorSlot;
    [SerializeField] InventorySlot[] otherSlots = new InventorySlot[0];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Get the next empty slot (besides the weapon and armor slot)
    /// </summary>
    public InventorySlot GetNextEmptySlot()
    {
        foreach (var slot in otherSlots)
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
        foreach (var slot in otherSlots)
        {
            if (slot.OccupiedItem == targetItem)
            {
                return slot;
            }
        }
        return null;
    }
}
