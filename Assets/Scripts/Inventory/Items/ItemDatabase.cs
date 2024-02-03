using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    [SerializeField] ItemObject[] allItems = new ItemObject[0];

    static ItemObject[] AllItems => instance.allItems;

    static ItemDatabase instance;

    void Awake()
    {
        instance = this;
    }

    public static ItemObject GetRandomItem()
    {
        float totalDropChance = 0f;
        foreach (var item in AllItems)
        {
            totalDropChance += item.DropChance;
        }

        float dropRng = Random.Range(0f, totalDropChance);

        foreach (var item in AllItems)
        {
            if (dropRng <= item.DropChance)
            {
                return item;
            }
            dropRng -= item.DropChance;
        }

        return null;
    }
}
