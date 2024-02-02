using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    // Assign these in the inspector
    [SerializeField] BarMeter barMeter;

    static HealthBar instance;

    void Awake()
    {
        instance = this;
    }

    public static void UpdateHealth(int newHealthValue, int maxHealth)
    {
        instance.barMeter.UpdateUI(newHealthValue);
    }
}
