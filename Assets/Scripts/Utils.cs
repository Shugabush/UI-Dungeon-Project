using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static string ToPercentString(this float _decimal, string format = "")
    {
        return _decimal.ToString(format) + "%";
    }

    public static string ToPercentString(this int _int, string format = "")
    {
        return _int.ToString(format) + "%";
    }
}
