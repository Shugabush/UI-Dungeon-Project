using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI/DungeonRoomButton", 30)]
public class DungeonRoomButton : Button
{
    // Dungeon rooms that will be unlocked after this one is completed
    public List<DungeonRoomButton> nextDungeonRooms = new List<DungeonRoomButton>();
    public List<DungeonRoomButton> dungeonRoomDependencies = new List<DungeonRoomButton>();

    // How many dungeons from dungeon dependencies do we need to complete before this one is unlocked?
    public int minDungeonCompleteRequirement = 1;

    public bool unlocked = false;
    public bool completed = false;

    protected override void Awake()
    {
        base.Awake();

        // Dungeon complete requirement cannot be more than the number of dungeon dependencies
        minDungeonCompleteRequirement = Mathf.Min(minDungeonCompleteRequirement, dungeonRoomDependencies.Count);

        unlocked = dungeonRoomDependencies.Count == 0 || minDungeonCompleteRequirement == 0;
        int dungeonRoomsCompleted = 0;
        foreach (var dungeonRoom in dungeonRoomDependencies)
        {
            if (dungeonRoom.completed)
            {
                dungeonRoomsCompleted++;
                if (dungeonRoomsCompleted >= minDungeonCompleteRequirement)
                {
                    unlocked = true;
                    break;
                }
            }
        }

        interactable = unlocked;
    }

    void Update()
    {
        
    }

    protected override void OnValidate()
    {
        base.OnValidate();

        bool anythingModified = false;

        // Make sure any next dungeon rooms have this dungeon room in their dependencies
        foreach (var dungeonRoom in nextDungeonRooms)
        {
            if (dungeonRoom != null && !dungeonRoom.dungeonRoomDependencies.Contains(this))
            {
                dungeonRoom.dungeonRoomDependencies.Add(this);
                anythingModified = true;
            }
        }

        // For each dependency, if any don't have this dungeon in their next dungeons,
        // remove that dungeon from this list
        for (int i = 0; i < dungeonRoomDependencies.Count; i++)
        {
            if (dungeonRoomDependencies[i] != null && !dungeonRoomDependencies[i].nextDungeonRooms.Contains(this))
            {
                dungeonRoomDependencies.RemoveAt(i);
                anythingModified = true;
                i--;
            }
        }

#if UNITY_EDITOR
        if (anythingModified)
        {
            UnityEditor.EditorUtility.SetDirty(this);
        }
#endif
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        foreach (var dungeonRoom in nextDungeonRooms)
        {
            if (dungeonRoom != null)
            {
                Gizmos.DrawLine(transform.position, dungeonRoom.transform.position);
            }
        }

        Gizmos.color = Color.blue;
        foreach (var dungeonRoom in dungeonRoomDependencies)
        {
            if (dungeonRoom != null)
            {
                Gizmos.DrawLine(transform.position, dungeonRoom.transform.position);
            }
        }
    }
}
