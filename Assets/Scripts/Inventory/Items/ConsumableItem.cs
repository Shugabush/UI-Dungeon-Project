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

    public override string GetDescription()
    {
        switch (ItemType)
        {
            case Type.Health:
                return "+" + StatsValue.ToString() + " health";
            case Type.Strength:
                return "+" + StatsValue.ToPercentString() + " fight success";
            case Type.Armor:
                return "-" + StatsValue.ToString() + " damage taken";
            case Type.Invisibility:
                return "Makes you invisible for one room, but 0% fight success for the next room";
            default:
                return base.GetDescription();
        }
    }

    public override bool Useable()
    {
        switch (ItemType)
        {
            case Type.Health:
                return !GameManager.HealthMeter.IsMaxedOut;
            case Type.Strength:
                return !GameManager.WeaponStats.IsMaxedOut;
            case Type.Armor:
                return !GameManager.ArmorStats.IsMaxedOut;
            case Type.Invisibility:
                return !GameManager.IsInvisible;
            default:
                return base.Useable();
        }
    }

    public override void OnUsed()
    {
        switch (ItemType)
        {
            case Type.Health:
                GameManager.Health += (int)StatsValue;
                break;
            case Type.Strength:
                GameManager.ExtraFightSuccess += (int)StatsValue;
                break;
            case Type.Armor:
                GameManager.Armor += (int)StatsValue;
                break;
            case Type.Invisibility:
                GameManager.IsInvisible = true;
                break;
            default:
                base.OnUsed();
                break;
        }
    }
}
