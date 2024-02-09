using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LowHealthIndicator : MonoBehaviour
{
    [SerializeField] Volume volume;
    Vignette vg;

    static LowHealthIndicator instance;

    [SerializeField] float animationSpeed = 5f;
    [SerializeField] float animationAmount = 0.25f;

    // Time elapsed (while the volume is active)
    float timeElapsed;

    void Awake()
    {
        instance = this;

        timeElapsed = 0f;

        volume.profile.TryGet(out vg);
    }

    void Update()
    {
        if (volume.enabled)
        {
            // Animate vignette intensity (back and forth with sine operator)
            timeElapsed += Time.deltaTime;
            Debug.Log(Mathf.Cos(Mathf.PI / 2f));
            vg.intensity.value = Mathf.Cos(timeElapsed * animationSpeed) * 0.5f * animationAmount + 0.5f;
        }
    }

    public static void Enable()
    {
        instance.volume.enabled = true;
    }

    public static void Disable()
    {
        instance.volume.enabled = false;
    }
}
