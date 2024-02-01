using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryLayout : MonoBehaviour
{
    InventorySlot[] slots;

    void Awake()
    {
        slots = GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
