using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DungeonRoom : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] int difficultyIndex = 0;
    [SerializeField] Button button;
    [SerializeField] TMP_Text difficultyText;

    // Sprite to use when this dungeon room isn't unlocked
    [SerializeField] Sprite blankButtonSprite;

    // Sprite to use when this dungeon room is unlocked
    [SerializeField] Sprite mainButtonSprite;

    [field: SerializeField]
    public RectTransform Rt { get; private set; }

    static int currentDifficultyIndex = 0;

    public bool DifficultyIndexIsValid => DifficultyIndex >= 0 && DifficultyIndex < DungeonMap.DifficultyCount;
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
        currentDifficultyIndex = 1;

        // Dungeon complete requirement cannot be more than the number of dungeon dependencies
        minDungeonCompleteRequirement = Mathf.Min(minDungeonCompleteRequirement, dungeonRoomDependencies.Count);

        CheckForUnlocked();

        button.onClick.AddListener(() => DungeonRoomScreen.EnableCombatReport(this));

        if (difficultyIndex == 0)
        {
            difficultyText.text = string.Empty;
        }
        else
        {
            difficultyText.text = difficultyIndex.ToString();
        }
    }

    void Update()
    {
        if (currentDifficultyIndex != difficultyIndex)
        {
            // Skip this dungeon if it wasn't completed (aka the player took a different path)
            unlocked = completed;
        }
        button.interactable = !completed;

        difficultyText.enabled = button.enabled;

        if (unlocked)
        {
            button.enabled = true;
            button.image.sprite = mainButtonSprite;
        }
        else
        {
            button.enabled = false;
            button.image.sprite = blankButtonSprite;
        }
    }

#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        foreach (var room in nextDungeonRooms)
        {
            if (room != null)
            {
                Gizmos.DrawLine(transform.position, room.transform.position);
            }
        }

        Gizmos.color = Color.blue;
        foreach (var room in dungeonRoomDependencies)
        {
            if (room != null)
            {
                Gizmos.DrawLine(transform.position, room.transform.position);
            }
        }
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

        if (button != null && button.image.sprite != mainButtonSprite)
        {
            button.image.sprite = mainButtonSprite;
            anythingModified = true;
        }

        if (difficultyText != null)
        {
            if (difficultyIndex == 0)
            {
                if (difficultyText.text != string.Empty)
                {
                    difficultyText.text = string.Empty;
                    anythingModified = true;
                }
            }
            else if (difficultyText.text != difficultyIndex.ToString())
            {
                difficultyText.text = difficultyIndex.ToString();
                anythingModified = true;
            }
        }

        if (anythingModified)
        {
            UnityEditor.EditorUtility.SetDirty(this);
        }
    }
#endif

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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!button.enabled || !button.interactable) return;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!button.enabled || !button.interactable) return;
    }
}
