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
    [SerializeField] Button unequipButton;
    [SerializeField] Button sellButton;
    [SerializeField] TMP_Text sellButtonText;
    [SerializeField] Button useButton;

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
                SetTexts();
            }
        }
    }

    void SetTexts()
    {
        nameText.text = selectedItem.name;
        descriptionText.text = selectedItem.GetDescription();
        priceText.text = selectedItem.GoldValue.ToString() + " Gold";
    }

    // If we select any of these selectables,
    // that will prevent this hover panel from closing
    [SerializeField] GameObject[] selectablesToKeepThisEnabled = new GameObject[0];

    public BaseSlot selectedSlot;
    BaseSlot lastNonNullSelectedSlot;

    MaskableGraphic[] graphics;

    [SerializeField] Vector3 offset = Vector3.zero;

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
            equipButton.onClick.AddListener(() => Equip(lastNonNullSelectedSlot as InventorySlot));
        }
        if (unequipButton != null)
        {
            unequipButton.onClick.AddListener(() => Unequip(lastNonNullSelectedSlot as InventorySlot));
        }
        if (sellButton != null)
        {
            sellButton.onClick.AddListener(() => Sell(lastNonNullSelectedSlot as InventorySlot));
        }
        if (useButton != null)
        {
            useButton.onClick.AddListener(() => Use(lastNonNullSelectedSlot as InventorySlot));
        }

        graphics = GetComponents<MaskableGraphic>();

        DisableUI();
    }

    void Update()
    {
        if (selectedSlot != null)
        {
            lastNonNullSelectedSlot = selectedSlot;
        }
        if (!justPurchased && Input.GetMouseButtonUp(0))
        {
            GameObject selectedGameObject = EventSystem.current.currentSelectedGameObject;

            if (selectedGameObject != null && (AnyOtherSelectableSelected() || IsSelected()))
            {
                selectedSlot = selectedGameObject.GetComponent<BaseSlot>();
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
            }
        }

        if (folder.activeInHierarchy && selectedItem != null)
        {
            EnableOrDisablePurchaseButton();
        }
    }

    void EnableUI()
    {
        if (selectedSlot != null)
        {
            Rt.position = selectedSlot.Rt.position + offset;
            if (selectedSlot.Item != null)
            {
                if (sellButtonText != null)
                {
                    sellButtonText.text = $"Sell ({selectedSlot.Item.GoldValue} Gold)";
                }
                EnableOrDisableUseButton();
            }
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

        selectedSlot = null;
        SelectedItem = null;
    }

    void Purchase()
    {
        StorageScreen.Inventory.AddItem(selectedItem);
        GameManager.Gold -= selectedItem.GoldValue;

        EnableOrDisablePurchaseButton();

        if (selectedItem.GetType() == typeof(ArmorItem) || selectedItem.GetType() == typeof(WeaponItem))
        {
            lastNonNullSelectedSlot.button.interactable = false;
            DisableUI();
        }

        StartCoroutine(PurchaseCooldown());
    }

    void Equip(InventorySlot slot)
    {
        PlayerStatsScreen.Equip(slot);
        if (slot.Item == null)
        {
            DisableUI();
        }
    }

    void Unequip(InventorySlot slot)
    {
        StorageScreen.Unequip(slot);
        if (slot.Item == null)
        {
            DisableUI();
        }
    }

    void Sell(InventorySlot slot)
    {
        StoreScreen.EnableSlot(slot.Item);
        slot.RemoveItem(true);
        if (slot.Empty)
        {
            DisableUI();
        }
    }

    void Use(InventorySlot slot)
    {
        if (slot.Item != null)
        {
            slot.Item.OnUsed();
        }
        slot.RemoveItem();
        EnableOrDisableUseButton();

        if (slot.Item == null)
        {
            DisableUI();
        }
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
        if (purchaseButton != null)
        {
            bool interactable = GameManager.Gold >= selectedItem.GoldValue;
            purchaseButton.interactable = interactable;
            purchaseButtonCanvasGroup.blocksRaycasts = interactable;
        }
    }

    void EnableOrDisableUseButton()
    {
        if (useButton != null)
        {
            useButton.gameObject.SetActive(selectedSlot != null && selectedSlot.Item != null && selectedSlot.Item.Useable());
        }
    }

    IEnumerator Open(ItemObject item)
    {
        StopCoroutine(Close());
        SelectedItem = item;

        EnableOrDisablePurchaseButton();

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
        GameObject selectedGameObject = EventSystem.current.currentSelectedGameObject;

        if (selectedGameObject  == gameObject)
        {
            return true;
        }

        if (purchaseButton != null && selectedGameObject == purchaseButton.gameObject)
        {
            return true;
        }

        if (equipButton != null && selectedGameObject == equipButton.gameObject)
        {
            return true;
        }

        if (unequipButton != null && selectedGameObject == unequipButton.gameObject)
        {
            return true;
        }

        if (sellButton != null && selectedGameObject == sellButton.gameObject)
        {
            return true;
        }

        if (useButton != null && selectedGameObject == useButton.gameObject)
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
