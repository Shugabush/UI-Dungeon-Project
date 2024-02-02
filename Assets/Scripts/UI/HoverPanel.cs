using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
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
    [SerializeField] Button equipButton;

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

    // If we select any of these selectables,
    // that will prevent this hover panel from closing
    [SerializeField] GameObject[] selectablesToKeepThisEnabled = new GameObject[0];

    public GameObject selectedObject;

    void Awake()
    {
        Rt = transform as RectTransform;

        if (purchaseButton != null)
        {
            purchaseButton.onClick.AddListener(Purchase);
        }
        if (equipButton != null)
        {
            equipButton.onClick.AddListener(() => Equip(SelectedItem));
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                gameObject.SetActive(false);
            }
            else if (EventSystem.current.currentSelectedGameObject != selectedObject &&
                !AnyOtherSelectableSelected() && !IsSelected())
            {
                gameObject.SetActive(false);
            }
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

    void Equip(ItemObject item)
    {

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

    /// <summary>
    /// 
    /// </summary>
    /// <returns>Whether or not the hover panel is selected or either of its buttons are selected</returns>
    public bool IsSelected()
    {
        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            return true;
        }

        if (purchaseButton != null && EventSystem.current.currentSelectedGameObject == purchaseButton.gameObject)
        {
            return true;
        }

        if (equipButton != null && EventSystem.current.currentSelectedGameObject == equipButton.gameObject)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>Whether or not any other selectable is selected besides the selected object</returns>
    bool AnyOtherSelectableSelected()
    {
        if (EventSystem.current.currentSelectedGameObject == selectedObject)
        {
            return false;
        }

        foreach (var selectable in selectablesToKeepThisEnabled)
        {
            if (EventSystem.current.currentSelectedGameObject == selectable)
            {
                return true;
            }
        }

        return false;
    }
}
