using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldUI : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    static List<GoldUI> instances = new List<GoldUI>();

    void Awake()
    {
        if (text == null)
        {
            text = GetComponentInChildren<TMP_Text>();
        }

        instances.Add(this);
    }

    public static void UpdateInstances(int gold)
    {
        foreach (var instance in instances)
        {
            instance.text.text = gold.ToString();
        }
    }

    void OnDestroy()
    {
        instances.Remove(this);
    }
}
