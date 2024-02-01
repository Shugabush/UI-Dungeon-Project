using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageScreen : MonoBehaviour
{
    [SerializeField] InventoryLayout inventory;

    static StorageScreen instance;

    public static InventoryLayout Inventory => instance.inventory;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
