using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonRoom : MonoBehaviour
{
    [SerializeField] int difficultyIndex = 0;
    [SerializeField] Button button;

    static int currentDifficultyIndex = 0;

    public int DifficultyIndex => difficultyIndex;

    // Dungeon rooms that will be unlocked after this one is completed
    public List<DungeonRoom> nextDungeonRooms = new List<DungeonRoom>();
    public List<DungeonRoom> dungeonRoomDependencies = new List<DungeonRoom>();

    // How many dungeons from dungeon dependencies do we need to complete before this one is unlocked?
    public int minDungeonCompleteRequirement = 1;

    [SerializeField] bool unlocked = false;
    [SerializeField] bool completed = false;

    public bool Unlocked
    {
        get
        {
            return unlocked;
        }
    }

    public bool Completed
    {
        get
        {
            return completed;
        }
        set
        {
            completed = value;
            if (value)
            {
                currentDifficultyIndex++;
            }
            foreach (var room in nextDungeonRooms)
            {
                room.CheckForUnlocked();
            }
        }
    }

    void Awake()
    {
        currentDifficultyIndex = 0;

        // Dungeon complete requirement cannot be more than the number of dungeon dependencies
        minDungeonCompleteRequirement = Mathf.Min(minDungeonCompleteRequirement, dungeonRoomDependencies.Count);

        CheckForUnlocked();

        button.onClick.AddListener(() => DungeonRoomScreen.EnableCombatReport(this));
    }

    void Update()
    {
        if (currentDifficultyIndex != difficultyIndex)
        {
            // Skip this dungeon if it wasn't completed
            unlocked = completed;
        }
        button.interactable = unlocked;
        button.enabled = !completed;
    }

    void OnValidate()
    {
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

    /*void OnDrawGizmosSelected()
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
    }*/

    void CheckForUnlocked()
    {
        if (dungeonRoomDependencies.Count == 0 || minDungeonCompleteRequirement == 0)
        {
            unlocked = true;
        }
        else
        {
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
        }
    }
}
