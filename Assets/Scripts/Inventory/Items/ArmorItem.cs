using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ArmorItem : ItemObject
{
    [field: SerializeField]
    public Sprite SpriteForPlayer { get; private set; }

    [field: SerializeField]
    public int DamageReduction { get; private set; } = 1;

    public override string GetDescription()
    {
        return "-" + DamageReduction.ToString() + " damage taken";
    }

    public override bool Useable()
    {
        return false;
    }
}
