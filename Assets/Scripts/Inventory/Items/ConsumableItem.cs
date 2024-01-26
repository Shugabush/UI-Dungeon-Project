using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ConsumableItem : ItemObject
{
    [System.Serializable]
    public enum Type
    {
        Health,
        Strength,
        Armor,
        Invisibility,
    }

    [field: SerializeField]
    public float StatsValue { get; private set; } = 2f;

    [field: SerializeField]
    public Type ItemType { get; private set; } = Type.Health;
}
