using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    static GameOverScreen instance;

    void Awake()
    {
        instance = this;
    }
}
