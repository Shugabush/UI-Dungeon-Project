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
