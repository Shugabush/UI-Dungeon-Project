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

    /// <summary>
    /// 
    /// </summary>
    /// <returns>Whether or not this item is useable</returns>
    public virtual bool Useable()
    {
        return true;
    }

    /// <summary>
    /// Called when this item gets used/consumed
    /// </summary>
    public virtual void OnUsed()
    {

    }
}
