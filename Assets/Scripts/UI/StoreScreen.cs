using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreScreen : MonoBehaviour
{
    [SerializeField] SelectedItemPanel selectedItemPanel;

    [SerializeField] MerchandiseSlot[] merchandiseSlots = new MerchandiseSlot[0];

    // Start is called before the first frame update
    void Start()
    {
        foreach (var slot in merchandiseSlots)
        {
            slot.onClick.AddListener(() => selectedItemPanel.Toggle(slot.Item));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
