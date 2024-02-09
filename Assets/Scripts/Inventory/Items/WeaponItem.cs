using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class WeaponItem : ItemObject
{
    [field: SerializeField]
    public Sprite SpriteForPlayer { get; private set; }

    [field: SerializeField]
    public int ExtraFightSuccess { get; private set; } = 1;

    public override string GetDescription()
    {
        return "+" + ExtraFightSuccess.ToPercentString() + " Fight Success";
    }

    public override bool Useable()
    {
        return StorageScreen.Instance.gameObject.activeInHierarchy && PlayerStatsScreen.WeaponSlot.Item != this;
    }

    public override void OnUsed()
    {
        PlayerStatsScreen.SelectWeapon(this);
    }
}
