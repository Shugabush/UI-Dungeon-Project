using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    // Game Objects that we want to be enabling or disabling
    [SerializeField] GameObject[] objectsToAffect = new GameObject[0];

    // Graphics that we want to be enabling or disabling
    [SerializeField] MaskableGraphic[] maskableGraphicsToAffect = new MaskableGraphic[0];

    [SerializeField] Canvas screen;

    static GameObject[] ObjectsToAffect => instance.objectsToAffect;
    static MaskableGraphic[] MaskableGraphicsToAffect => instance.maskableGraphicsToAffect;
    static Canvas Screen => instance.screen;

    static StartScreen instance;

    void Awake()
    {
        instance = this;
    }

    public static void Enable()
    {
        foreach (var obj in ObjectsToAffect)
        {
            obj.SetActive(true);
        }
        foreach (var graphic in MaskableGraphicsToAffect)
        {
            graphic.enabled = true;
        }
    }

    public static void Disable()
    {
        foreach (var obj in ObjectsToAffect)
        {
            obj.SetActive(false);
        }
        foreach (var graphic in MaskableGraphicsToAffect)
        {
            graphic.enabled = false;
        }
    }
}
