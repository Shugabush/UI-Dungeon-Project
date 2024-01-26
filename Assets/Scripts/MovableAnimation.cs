using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableAnimation : MonoBehaviour
{
    public Vector3 positionOffset;
    public float duration = 5f;

    bool moving;

    [SerializeField] float gizmoSphereRadius = 5f;

    Vector3 startPos;

    Timer moveTimer;

    void Awake()
    {
        moveTimer = new Timer(duration);
        startPos = transform.position;
    }

    public IEnumerator Move()
    {
        StopCoroutine(Unmove());
        moving = true;
        while (!moveTimer.OutOfTime && moving)
        {
            moveTimer.Update(Time.deltaTime);
            transform.position = startPos + (positionOffset * moveTimer.FractionOfTimeElapsed);
            yield return null;
        }
        if (moving)
        {
            transform.position = startPos + positionOffset;
        }
    }

    public IEnumerator Unmove()
    {
        StopCoroutine(Move());
        moving = false;
        while (moveTimer.TimeElapsed > 0 && !moving)
        {
            // Update timer in opposite direction
            moveTimer.Update(-Time.deltaTime);
            transform.position = startPos + (positionOffset * moveTimer.FractionOfTimeElapsed);
            yield return null;
        }
        if (!moving)
        {
            transform.position = startPos;
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
