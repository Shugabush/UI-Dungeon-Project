using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ArmorItem : ItemObject
{
    [field: SerializeField]
    public Sprite SpriteForPlayer { get; private set; }

    [field: SerializeField]
    public int ArmorValue { get; private set; } = 1;
}
