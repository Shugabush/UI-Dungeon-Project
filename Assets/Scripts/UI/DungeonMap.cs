using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonMap : MonoBehaviour
{
    private class RoomLine
    {
        public LineRenderer line;

        public RectTransform parent;

        DungeonRoomButton from;
        DungeonRoomButton to;

        public RectTransform primaryRt;
        public RectTransform secondaryRt;

        public RoomLine(LineRenderer line, RectTransform parent, DungeonRoomButton from, DungeonRoomButton to)
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
            line.SetPosition(0, parent.TransformPoint(point1));
            line.SetPosition(1, parent.TransformPoint(point2));

            line.enabled = from.unlocked && to.unlocked;
        }
    }

    // Assign in the inspector
    [SerializeField] DungeonRoomButton[] rooms = new DungeonRoomButton[0];
    [SerializeField] Scrollbar scrollBar;

    // Lines that will be connecting each of the rooms to each other
    List<RoomLine> lines;
    [SerializeField] Material lineMat;

    const float lineWidth = 0.25f;

    void Awake()
    {
        scrollBar.value = 0;
        lines = new List<RoomLine>();

        foreach (var room in rooms)
        {
            if (room != null)
            {
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
                        RoomLine roomLine = new RoomLine(newLine, transform as RectTransform, room, dependency);

                        lines.Add(roomLine);
                    }
                }
            }
        }
    }

    void Update()
    {
        foreach (var line in lines)
        {
            line.Update();
        }
    }
}
