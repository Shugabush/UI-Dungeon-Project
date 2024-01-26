using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedItemPanel : MonoBehaviour
{
    [SerializeField] bool isOpen = false;

    [SerializeField] MovableAnimation movableAnimation;

    public IEnumerator Open()
    {
        yield return movableAnimation.Move();
        isOpen = true;
    }

    public IEnumerator Close()
    {
        yield return movableAnimation.Unmove();
        isOpen = false;
    }
}
