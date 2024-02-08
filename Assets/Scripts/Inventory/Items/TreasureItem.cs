using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class TreasureItem : ItemObject
{
    public override bool Useable()
    {
        return false;
    }
}
