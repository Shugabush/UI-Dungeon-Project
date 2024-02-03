using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LootCollection
{
    [SerializeField] ItemObject[] possibleItems = new ItemObject[0];

    public ItemObject GetRandomItem()
    {
        float totalDropChance = 0f;
        foreach (var item in possibleItems)
        {
            totalDropChance += item.DropChance;
        }

        float dropRng = Random.Range(0f, totalDropChance);

        foreach (var item in possibleItems)
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
