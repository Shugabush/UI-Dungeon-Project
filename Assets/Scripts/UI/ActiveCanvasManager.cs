using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCanvasManager : MonoBehaviour
{
    // Canvases that you want this object to track
    [SerializeField] Canvas[] canvases = new Canvas[0];

    static ActiveCanvasManager instance;

    void Awake()
    {
        instance = this;

        foreach (var canvas in canvases)
        {
            if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
            {
                canvas.worldCamera = Camera.main;
            }
        }
    }

    /// <summary>
    /// Sets the given canvas active and the rest to inactive
    /// </summary>
    public static void SetCanvasActive(Canvas targetCanvas)
    {
        foreach (var canvas in instance.canvases)
        {
            canvas.enabled = canvas == targetCanvas;
        }
    }

    public static void SetCanvasAndChildrenActive(Canvas targetCanvas)
    {
        foreach (var canvas in instance.canvases)
        {
            if (canvas == targetCanvas)
            {
                Canvas[] children = canvas.GetComponentsInChildren<Canvas>();
                foreach (var child in children)
                {
                    child.enabled = true;
                }
            }
            else
            {
                canvas.enabled = false;
            }
        }
    }

    /// <summary>
    /// If you know the index of the canvas that you want active,
    /// you can use this overload
    /// </summary>
    public static void SetCanvasActive(int index)
    {
        for (int i = 0; i < instance.canvases.Length; i++)
        {
            instance.canvases[i].enabled = i == index;
        }
    }

    public static void SetCanvasActiveAdditive(Canvas targetCanvas)
    {
        foreach (var canvas in instance.canvases)
        {
            if (canvas == targetCanvas)
            {
                canvas.enabled = true;
            }
        }
    }

    public static void SetCanvasActiveAdditive(int index)
    {
        instance.canvases[index].enabled = true;
    }

    /// <summary>
    /// Disables all canvases
    /// </summary>
    public static void DisableAllCanvases()
    {
        foreach (var canvas in instance.canvases)
        {
            canvas.enabled = false;
        }
    }
}
