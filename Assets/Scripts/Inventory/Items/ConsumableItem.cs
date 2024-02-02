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
}
