using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldUI : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    static GoldUI instance;

    void Awake()
    {
        if (text == null)
        {
            text = GetComponentInChildren<TMP_Text>();
        }
        instance = this;
    }

    public static void UpdateText(int gold)
    {
        instance.text.text = gold.ToString();
    }
}
