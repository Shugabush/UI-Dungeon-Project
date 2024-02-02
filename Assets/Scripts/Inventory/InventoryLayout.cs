using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryLayout : MonoBehaviour
{
    InventorySlot[] slots;
    [SerializeField] HoverPanel hoverPanel;
    const float hoverPanelYOffset = 25f;

    void Awake()
    {
        slots = GetComponentsInChildren<InventorySlot>();

        if (hoverPanel != null)
        {
            foreach (var slot in slots)
            {
                slot.button.onClick.AddListener(() => ToggleHoverPanel(slot));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>Whether or not the current selected game object is one of the inventory slots</returns>
    public bool AnySlotSelected()
    {
        foreach (var slot in slots)
        {
            if (EventSystem.current.currentSelectedGameObject == slot.gameObject)
            {
                return true;
            }
        }
        return false;
    }

    public InventorySlot GetNextEmptySlot()
    {
        foreach (var slot in slots)
        {
            if (slot.Item == null)
            {
                return slot;
            }
        }
        return null;
    }

    public InventorySlot GetSlotWithItem(ItemObject targetItem)
    {
        foreach (var slot in slots)
        {
            if (slot.Item == targetItem)
            {
                return slot;
            }
        }
        // Just return the next empty slot
        return GetNextEmptySlot();
    }

    public void AddItem(ItemObject item)
    {
        InventorySlot targetSlot = GetSlotWithItem(item);
        if (targetSlot != null)
        {
            targetSlot.AddItem(item);
        }
    }

    void ToggleHoverPanel(InventorySlot slot)
    {
        if (slot == null || slot.Item == null)
        {
            hoverPanel.gameObject.SetActive(false);
            hoverPanel.SelectedItem = null;
            return;
        }

        if (hoverPanel.gameObject.activeSelf)
        {
            if (hoverPanel.SelectedItem == slot.Item)
            {
                // Close the hover panel
                hoverPanel.gameObject.SetActive(false);
                hoverPanel.SelectedItem = null;
            }
            else
            {
                // Switch hover panel's selected item
                hoverPanel.SelectedItem = slot.Item;
                hoverPanel.Rt.position = slot.Rt.position + Vector3.up * hoverPanelYOffset;
            }
        }
        else
        {
            // Open hover panel and set its selected item
            hoverPanel.gameObject.SetActive(true);
            hoverPanel.Rt.position = slot.Rt.position + Vector3.up * hoverPanelYOffset;
            hoverPanel.SelectedItem = slot.Item;
        }
    }
}
