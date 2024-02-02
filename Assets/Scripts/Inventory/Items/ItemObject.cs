using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ItemObject : ScriptableObject
{
    [field: SerializeField]
    public Sprite Sprite { get; private set; } = null;

    [field: SerializeField]
    public int GoldValue { get; private set; } = 10;

    public string description = string.Empty;

    [field: SerializeField, Range(0, 1)]
    public float DropChance { get; private set; } = 0.7f;

    public virtual string GetDescription()
    {
        return description;
    }
}
