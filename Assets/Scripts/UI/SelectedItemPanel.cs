using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectedItemPanel : MonoBehaviour
{
    public bool IsOpen => selectedItem != null;

    ItemObject selectedItem = null;

    // Assign these in the inspector
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text descriptionText;
    [SerializeField] TMP_Text priceText;
    [SerializeField] Button purchaseButton;

    public ItemObject SelectedItem
    {
        get
        {
            return selectedItem;
        }
        private set
        {
            selectedItem = value;
            if (selectedItem != null)
            {
                nameText.text = selectedItem.name;
                descriptionText.text = selectedItem.Description;
                priceText.text = selectedItem.GoldValue.ToString();
            }
        }
    }

    [SerializeField] MovableAnimation movableAnimation;

    void Awake()
    {
        purchaseButton.onClick.AddListener(Purchase);
    }

    public void Toggle(ItemObject item)
    {
        if (item == null) return;
        StopAllCoroutines();
        if (IsOpen)
        {
            StartCoroutine(Close());
        }
        else
        {
            StartCoroutine(Open(item));
        }
    }

    void Purchase()
    {

    }

    IEnumerator Open(ItemObject item)
    {
        StopCoroutine(Close());
        SelectedItem = item;

        purchaseButton.interactable = GameManager.Gold >= item.GoldValue;

        descriptionText.text = item.Description;
        priceText.text = item.GoldValue.ToString() + " Gold";
        yield return movableAnimation.Move();
    }

    IEnumerator Close()
    {
        StopCoroutine(Open(selectedItem));
        yield return movableAnimation.Unmove();
        SelectedItem = null;
    }
}
