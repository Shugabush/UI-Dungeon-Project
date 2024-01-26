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

    public void Toggle(ItemObject item)
    {
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

    IEnumerator Open(ItemObject item)
    {
        StopCoroutine(Close());
        SelectedItem = item;
        yield return movableAnimation.Move();
    }

    IEnumerator Close()
    {
        StopCoroutine(Open(selectedItem));
        yield return movableAnimation.Unmove();
        SelectedItem = null;
    }
}
