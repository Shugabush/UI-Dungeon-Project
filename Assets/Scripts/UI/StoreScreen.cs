using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StoreScreen : MonoBehaviour
{
    [SerializeField] HoverPanel hoverPanel;

    [SerializeField] MerchandiseSlot[] merchandiseSlots = new MerchandiseSlot[0];

    const float hoverPanelYOffset = 25f;

    void Start()
    {
        foreach (var slot in merchandiseSlots)
        {
            slot.button.onClick.AddListener(() => ToggleHoverPanel(slot));
        }
    }

    void ToggleHoverPanel(MerchandiseSlot slot)
    {
        if (slot == null || slot.Item == null)
        {
            hoverPanel.gameObject.SetActive(false);
            hoverPanel.selectedObject = null;
            hoverPanel.SelectedItem = null;
            return;
        }

        if (hoverPanel.gameObject.activeSelf)
        {
            if (hoverPanel.SelectedItem == slot.Item)
            {
                // Close the hover panel
                hoverPanel.gameObject.SetActive(false);
                hoverPanel.selectedObject = null;
                hoverPanel.SelectedItem = null;
            }
            else
            {
                // Switch hover panel's selected item
                hoverPanel.SelectedItem = slot.Item;
                hoverPanel.selectedObject = slot.button.gameObject;
                hoverPanel.Rt.position = slot.Rt.position + Vector3.up * hoverPanelYOffset;
            }
        }
        else
        {
            // Open hover panel and set its selected item
            hoverPanel.gameObject.SetActive(true);
            hoverPanel.selectedObject = slot.button.gameObject;
            hoverPanel.Rt.position = slot.Rt.position + Vector3.up * hoverPanelYOffset;
            hoverPanel.SelectedItem = slot.Item;
        }
    }
}
