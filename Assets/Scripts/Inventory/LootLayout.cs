using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootLayout : InventoryLayout
{
    public void GenerateLoot()
    {
        // Reset loot layout
        RemoveAllItems();

        int itemsToDrop = Random.Range(DungeonMap.CurrentDifficulty.minLootDrops, DungeonMap.CurrentDifficulty.maxLootDrops);

        for (int i = 0; i < itemsToDrop; i++)
        {
            ItemObject item = ItemDatabase.GetRandomItem();
            AddItem(item);
            PlayerStatsScreen.AddItem(item);
        }
    }
}
