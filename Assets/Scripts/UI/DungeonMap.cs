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
            line.SetPosition(0, parent.TransformPoint(point1));
            line.SetPosition(1, parent.TransformPoint(point2));

            if (!from.unlocked || !to.unlocked)
            {
                // Gray out the line
                line.material.color = new Color(0.75f, 0.75f, 0.75f, 0.25f);
            }
        }
    }

    // Assign in the inspector
    [SerializeField] DungeonRoom[] rooms = new DungeonRoom[0];

    // Lines that will be connecting each of the rooms to each other
    List<RoomLine> lines;
    [SerializeField] Material lineMat;
    [SerializeField] Canvas mapCanvas;

    const float lineWidth = 0.25f;

    static DungeonMap instance;

    public static Canvas MapCanvas => instance.mapCanvas;

    void Awake()
    {
        instance = this;
        if (mapCanvas == null)
        {
            mapCanvas = GetComponent<Canvas>();
        }

        lines = new List<RoomLine>();

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
            if (mapCanvas.enabled)
            {
                line.line.enabled = true;
                line.Update();
            }
            else
            {
                line.line.enabled = false;
            }
        }
    }
}
