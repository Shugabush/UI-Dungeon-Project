using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class WeaponItem : ItemObject
{
    [field: SerializeField]
    public Sprite SpriteForPlayer { get; private set; }

    [field: SerializeField]
    public float Strength { get; private set; } = 0.1f;

    public override string GetDescription()
    {
        return "+" + Strength.ToPercentString() + " Fight Success";
    }

    public override bool Useable()
    {
        return false;
    }
}
