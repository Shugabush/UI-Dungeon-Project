using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static string ToPercentString(this float _decimal)
    {
        return (_decimal * 100).ToString() + "%";
    }
}
