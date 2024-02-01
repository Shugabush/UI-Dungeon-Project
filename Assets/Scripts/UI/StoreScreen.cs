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

    // Start is called before the first frame update
    void Start()
    {
        foreach (var slot in merchandiseSlots)
        {
            slot.button.onClick.AddListener(() => ToggleHoverPanel(slot));
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetMouseButtonDown(0))
        {
            // Check through each slot and make sure none of them are what we selected
            foreach (var slot in merchandiseSlots)
            {
                if (EventSystem.current.currentSelectedGameObject == slot.gameObject)
                {
                    return;
                }
            }

            // If we didn't select a slot in the slot list,
            // Then make sure the hover panel is closed
            ToggleHoverPanel(null);
        }*/
    }

    void ToggleHoverPanel(MerchandiseSlot slot)
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
