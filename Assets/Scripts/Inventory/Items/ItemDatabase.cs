using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    [SerializeField] ItemObject[] possibleItems = new ItemObject[0];

    static ItemObject[] PossibleItems => instance.possibleItems;

    static ItemDatabase instance;

    void Awake()
    {
        instance = this;
    }

    public static ItemObject GetRandomItem()
    {
        float totalDropChance = 0f;
        foreach (var item in PossibleItems)
        {
            totalDropChance += item.DropChance;
        }

        float dropRng = Random.Range(0f, totalDropChance);

        foreach (var item in PossibleItems)
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
