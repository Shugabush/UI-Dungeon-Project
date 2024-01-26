using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class WeaponItem : ItemObject
{
    [field: SerializeField]
    public float Strength { get; private set; } = 0.1f;
}
