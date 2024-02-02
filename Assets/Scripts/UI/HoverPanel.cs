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

    CanvasGroup purchaseButtonCanvasGroup;

    // Enable and disable this instead of this object so we can still run behavior
    [SerializeField] GameObject folder;

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

    public BaseSlot selectedSlot;

    MaskableGraphic[] graphics;

    const float yOffset = 25f;

    bool justPurchased = false;

    void Awake()
    {
        Rt = transform as RectTransform;

        if (purchaseButton != null)
        {
            purchaseButton.onClick.AddListener(Purchase);
            purchaseButtonCanvasGroup = purchaseButton.GetComponent<CanvasGroup>();
        }
        if (equipButton != null)
        {
            equipButton.onClick.AddListener(() => PlayerStatsScreen.Equip(selectedSlot));
        }

        graphics = GetComponents<MaskableGraphic>();

        DisableUI();
    }

    void Update()
    {
        if (!justPurchased && Input.GetMouseButtonUp(0))
        {
            if (AnyOtherSelectableSelected() || IsSelected())
            {
                selectedSlot = EventSystem.current.currentSelectedGameObject.GetComponent<BaseSlot>();
                if (selectedSlot != null)
                {
                    SelectedItem = selectedSlot.Item;
                }
                if (selectedItem != null)
                {
                    EnableUI();
                }
                else
                {
                    DisableUI();
                }
            }
            else if (folder.activeInHierarchy)
            {
                DisableUI();
                selectedSlot = null;
                SelectedItem = null;
            }
        }
    }

    void EnableUI()
    {
        if (selectedSlot != null)
        {
            Rt.position = selectedSlot.Rt.position + Vector3.up * yOffset;
        }
        folder.SetActive(true);
        foreach (var graphic in graphics)
        {
            graphic.enabled = true;
        }
    }

    void DisableUI()
    {
        folder.SetActive(false);
        foreach (var graphic in graphics)
        {
            graphic.enabled = false;
        }
    }

    void Purchase()
    {
        StorageScreen.Inventory.AddItem(selectedItem);
        GameManager.Gold -= selectedItem.GoldValue;

        if (purchaseButton != null)
        {
            EnableOrDisablePurchaseButton();
        }

        if (selectedItem.GetType() == typeof(ArmorItem) || selectedItem.GetType() == typeof(WeaponItem))
        {
            // We can only purchase one of each armor item and weapon item
            DisableUI();
            selectedSlot.button.interactable = false;
        }

        StartCoroutine(PurchaseCooldown());
    }
    
    // Give a one frame cooldown,
    // if we don't do this, when we purchase an item
    // and then can't afford the next one, the logic in update
    // will think the hover panel needs to be disabled
    IEnumerator PurchaseCooldown()
    {
        justPurchased = true;
        yield return null;
        justPurchased = false;
    }

    void EnableOrDisablePurchaseButton()
    {
        bool interactable = GameManager.Gold >= selectedItem.GoldValue;
        purchaseButton.interactable = interactable;
        purchaseButtonCanvasGroup.blocksRaycasts = interactable;
    }

    IEnumerator Open(ItemObject item)
    {
        StopCoroutine(Close());
        SelectedItem = item;

        if (purchaseButton != null)
        {
            EnableOrDisablePurchaseButton();
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
        if (selectedSlot != null &&
            EventSystem.current.currentSelectedGameObject == selectedSlot.gameObject)
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
