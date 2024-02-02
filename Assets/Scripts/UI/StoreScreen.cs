using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreScreen : MonoBehaviour
{
    [SerializeField] BaseSlot[] merchandiseSlots = new BaseSlot[0];

    public static StoreScreen Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }
}
