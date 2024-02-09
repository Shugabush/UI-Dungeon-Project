using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovableAnimation : MonoBehaviour
{
    public Vector3 positionOffset;
    public float duration = 5f;

    bool moving;
    int direction;

    [SerializeField] float gizmoSphereRadius = 5f;

    Vector3 startPos;

    Vector3 Position
    {
        get
        {
            if (rt != null)
            {
                return rt.anchoredPosition;
            }
            return transform.localPosition;
        }
        set
        {
            if (rt != null)
            {
                rt.anchoredPosition = value;
            }
            else
            {
                transform.localPosition = value;
            }
        }
    }

    Timer moveTimer;

    RectTransform rt;

    void Awake()
    {
        moveTimer = new Timer(duration);

        rt = GetComponent<RectTransform>();
        startPos = Position;
    }

    public void StartMoveOrUnmove()
    {
        if (direction == 1)
        {
            StartCoroutine(Unmove());
        }
        else
        {
            StartCoroutine(Move());
        }
    }

    public void InstantUnmove()
    {
        float originalDuration = duration;
        duration = 0;

        if (direction == 1)
        {
            StartCoroutine(Unmove());
        }

        duration = originalDuration;
    }

    public IEnumerator Move()
    {
        StopCoroutine(Unmove());
        direction = 1;
        moving = true;
        while (!moveTimer.OutOfTime && moving)
        {
            moveTimer.Update(Time.deltaTime);
            Position = startPos + (positionOffset * moveTimer.FractionOfTimeElapsed);
            yield return null;
        }
        if (moving)
        {
            Position = startPos + positionOffset;
        }
    }

    public IEnumerator Unmove()
    {
        StopCoroutine(Move());
        direction = -1;
        moving = false;
        while (moveTimer.TimeElapsed > 0 && !moving)
        {
            // Update timer in opposite direction
            moveTimer.Update(-Time.deltaTime);
            Position = startPos + (positionOffset * moveTimer.FractionOfTimeElapsed);
            yield return null;
        }
        if (!moving)
        {
            Position = startPos;
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a line representing where this move animation will take the object to
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, gizmoSphereRadius);
        Gizmos.DrawSphere(transform.position + positionOffset, gizmoSphereRadius);
        Gizmos.DrawLine(transform.position, transform.position + positionOffset);
    }
}
