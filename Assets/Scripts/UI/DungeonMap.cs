using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DungeonMap : MonoBehaviour
{
    [System.Serializable]
    private class DungeonRoomCollection
    {
        [SerializeReference] public List<DungeonRoom> rooms = new List<DungeonRoom>();

        public DungeonRoomCollection()
        {
            rooms = new List<DungeonRoom>();
        }
    }

    private class RoomLine
    {
        public LineRenderer line;

        public RectTransform parent;

        DungeonRoom from;
        DungeonRoom to;

        public RectTransform primaryRt;
        public RectTransform secondaryRt;

        public RoomLine(LineRenderer line, RectTransform parent, DungeonRoom from, DungeonRoom to)
        {
            this.line = line;
            this.parent = parent;
            this.from = from;
            this.to = to;
            primaryRt = from.transform as RectTransform;
            secondaryRt = to.transform as RectTransform;
        }

        public void Update()
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(parent, primaryRt.position, Camera.main, out Vector2 point1);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(parent, secondaryRt.position, Camera.main, out Vector2 point2);

            Vector3 targetPoint1 = parent.TransformPoint(point1);
            Vector3 targetPoint2 = parent.TransformPoint(point2);

            targetPoint1.z = -1;
            targetPoint2.z = -1;

            Vector3 direction = (to.transform.position - from.transform.position).normalized;

            line.SetPosition(0, from.transform.position + (direction * LineOffset));
            line.SetPosition(1, to.transform.position - (direction * LineOffset));

            if (!from.Completed || !to.Unlocked)
            {
                // Gray out the line
                line.material.color = new Color(0.75f, 0.75f, 0.75f, 0.25f);
            }
            else
            {
                // Make the color white
                line.material.color = Color.white;
            }
        }
    }

    // Assign in the inspector
    [SerializeField] DungeonRoom[] rooms = new DungeonRoom[0];
    [SerializeField] DungeonDifficulty[] difficulties = new DungeonDifficulty[0];

    // Lines that will be connecting each of the rooms to each other
    List<RoomLine> roomLines;
    [SerializeField] Material lineMat;

    [SerializeField] Canvas screen;
    [SerializeField] Canvas[] childrenScreens = new Canvas[0]; 
    [SerializeField] Button retreatButton;

    [SerializeField] float lineWidth = 5f;
    [SerializeField] float lineOffset = 1f;

    static float LineOffset => instance.lineOffset;

    static DungeonMap instance;

    public static Canvas Screen => instance.screen;
    static DungeonDifficulty[] Difficulties => instance.difficulties;
    DungeonDifficulty currentDifficulty;
    public static DungeonDifficulty CurrentDifficulty
    {
        get
        {
            return instance.currentDifficulty;
        }
        private set
        {
            instance.currentDifficulty = value;
        }
    }

    // Each collection contains dungeons that are of a certain difficulty level (determined by the index)
    [SerializeField] DungeonRoomCollection[] dungeonRoomCollections = new DungeonRoomCollection[0];

    void Awake()
    {
        instance = this;
        if (screen == null)
        {
            screen = GetComponent<Canvas>();
        }
        retreatButton.onClick.AddListener(RetreatToSurface);

        roomLines = new List<RoomLine>();

        foreach (var room in rooms)
        {
            if (room != null)
            {
                // Set up line renderers
                foreach (var dependency in room.dungeonRoomDependencies)
                {
                    if (dependency != null)
                    {
                        LineRenderer newLine = new GameObject("Line").AddComponent<LineRenderer>();
                        newLine.startWidth = lineWidth;
                        newLine.endWidth = lineWidth;
                        newLine.material = new Material(lineMat);
                        newLine.transform.parent = transform;
                        newLine.transform.localPosition = Vector3.zero;
                        newLine.positionCount = 2;

                        RoomLine roomLine = new RoomLine(newLine, transform as RectTransform, dependency, room);

                        roomLines.Add(roomLine);
                    }
                }
            }
        }
    }

    void Update()
    {
        foreach (var child in childrenScreens)
        {
            child.enabled = screen.enabled;
        }

        foreach (var roomLine in roomLines)
        {
            if (screen.enabled)
            {
                roomLine.line.enabled = true;
                roomLine.Update();
            }
            else
            {
                roomLine.line.enabled = false;
            }
        }
    }

    static void RetreatToSurface()
    {
        Screen.enabled = false;
    }

    public static DungeonDifficulty GetDifficulty(DungeonRoom room)
    {
        return Difficulties[room.DifficultyIndex];
    }
    public static void SetCurrentDifficulty(DungeonRoom room)
    {
        CurrentDifficulty = GetDifficulty(room);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;

        foreach (var room in rooms)
        {
            if (room == null) continue;

            foreach (var nextRoom in room.nextDungeonRooms)
            {
                if (nextRoom == null) continue;

                Vector3 direction = (nextRoom.transform.position - room.transform.position).normalized;
                Gizmos.DrawLine(room.transform.position + (direction * lineOffset), nextRoom.transform.position - (direction * lineOffset));
            }
        }
    }

#if UNITY_EDITOR
    [ContextMenu("Get Room Children")]
    void GetRoomChildren()
    {
        rooms = GetComponentsInChildren<DungeonRoom>();
        UnityEditor.EditorUtility.SetDirty(this);
    }

    [ContextMenu("Get Dungeon Collections")]
    void GetDungeonCollections()
    {
        dungeonRoomCollections = new DungeonRoomCollection[difficulties.Length];

        // Get each room's difficulty and add it to the corresponding index in dungeon room collections
        foreach (var room in rooms)
        {
            DungeonRoomCollection targetCollection = dungeonRoomCollections[room.DifficultyIndex];

            if (targetCollection == null)
            {
                targetCollection = new DungeonRoomCollection();
                dungeonRoomCollections[room.DifficultyIndex] = targetCollection;
            }

            targetCollection.rooms.Add(room);
        }

        UnityEditor.EditorUtility.SetDirty(this);
    }
#endif
}
