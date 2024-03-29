using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BarMeter : MonoBehaviour
{
    // Assign these in the inspector
    [SerializeField] TMP_Text text;
    [SerializeField] int maxValue = 3;
    int value;

    public int MaxValue
    {
        get
        {
            return maxValue;
        }
        set
        {
            maxValue = value;

            int currentIconCount = icons.Count;

            // Resize icon list if necessary
            if (value > currentIconCount)
            {
                // Destroy extra icons
                for (int i = currentIconCount - 1; i >= value; i--)
                {
                    if (icons[i] != null)
                    {
                        Destroy(icons[i].gameObject);
                        icons.RemoveAt(i);
                    }
                }
            }
            else if (value < currentIconCount)
            {
                // Add new icons
                for (int i = currentIconCount - 1; i < value; i++)
                {
                    Image newIcon = new GameObject("Icon").AddComponent<Image>();
                    newIcon.transform.parent = transform;
                    newIcon.sprite = iconSprite;
                    icons.Add(newIcon);
                }
            }
        }
    }
    public bool IsMaxedOut => value >= maxValue;

    List<Image> icons;

    [SerializeField] Sprite iconSprite;

    void Awake()
    {
        icons = new List<Image>();
        for (int i = 0; i < maxValue; i++)
        {
            Image newIcon = new GameObject("Icon").AddComponent<Image>();
            newIcon.transform.SetParent(transform);

            // If we have a grid layout, it should adjust, but the z coordinate might be wrong,
            // so we want to set that to 0
            newIcon.transform.localPosition = Vector3.zero;
            newIcon.transform.localScale = Vector3.one;

            newIcon.sprite = iconSprite;
            newIcon.enabled = false;
            icons.Add(newIcon);
        }
    }

    public void UpdateUI(int newValue)
    {
        value = newValue;
        for (int i = 0; i < maxValue; i++)
        {
            icons[i].enabled = newValue > i;
        }
        if (text != null)
        {
            text.text = newValue.ToString() + "/" + maxValue.ToString();
        }
    }
}
