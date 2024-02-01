using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HoverPanel : MonoBehaviour
{
    public bool IsOpen { get; private set; }

    ItemObject selectedItem = null;

    // Assign these in the inspector
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text descriptionText;
    [SerializeField] TMP_Text priceText;
    [SerializeField] Button purchaseButton;

    public RectTransform Rt { get; private set; }

    public ItemObject SelectedItem
    {
        get
        {
            return selectedItem;
        }
        set
        {
            selectedItem = value;
            if (selectedItem != null)
            {
                nameText.text = selectedItem.name;
                descriptionText.text = selectedItem.GetDescription();
                priceText.text = selectedItem.GoldValue.ToString();
            }
        }
    }

    void Awake()
    {
        Rt = transform as RectTransform;
        if (purchaseButton != null)
        {
            purchaseButton.onClick.AddListener(Purchase);
        }
    }

    public void Toggle(ItemObject item)
    {
        if (item == null) return;
        StopAllCoroutines();
        if (IsOpen)
        {
            if (SelectedItem != item)
            {
                // Just switch the item out, don't close
                SelectedItem = item;
                // If the panel was in a state of closing
                // before we stopped all coroutines,
                // we need to make sure it's open
                StartCoroutine(Open(item));
                return;
            }
            StartCoroutine(Close());
        }
        else
        {
            StartCoroutine(Open(item));
        }
    }

    void Purchase()
    {
        StorageScreen.Inventory.AddItem(selectedItem);
        GameManager.Gold -= selectedItem.GoldValue;

        if (purchaseButton != null)
        {
            purchaseButton.interactable = GameManager.Gold >= selectedItem.GoldValue;
        }
    }

    IEnumerator Open(ItemObject item)
    {
        StopCoroutine(Close());
        SelectedItem = item;

        if (purchaseButton != null)
        {
            purchaseButton.interactable = GameManager.Gold >= item.GoldValue;
        }

        descriptionText.text = item.GetDescription();
        priceText.text = item.GoldValue.ToString() + " Gold";
        yield return null;
        IsOpen = true;
    }

    IEnumerator Close()
    {
        StopCoroutine(Open(selectedItem));
        yield return null;
        SelectedItem = null;
        IsOpen = false;
    }
}
