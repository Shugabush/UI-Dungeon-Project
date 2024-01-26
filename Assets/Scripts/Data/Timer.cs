using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Timer
{
    float timeElapsed;
    float timeLimit;

    public float TimeElapsed => timeElapsed;
    public float TimeLimit => timeLimit;

    public float TimeRemaining => TimeLimit - TimeElapsed;

    public bool OutOfTime => TimeElapsed >= TimeLimit;

    public float FractionOfTimeElapsed => TimeElapsed / TimeLimit;
    public float FractionOfTimeRemaining => TimeRemaining / TimeLimit;

    public Timer(float timeLimit)
    {
        timeElapsed = 0f;
        this.timeLimit = timeLimit;
    }

    public void Update(float time)
    {
        timeElapsed += time;
        timeElapsed = Mathf.Clamp(timeElapsed, 0f, timeLimit);
    }

    public void Reset()
    {
        timeElapsed = 0f;
    }
}
