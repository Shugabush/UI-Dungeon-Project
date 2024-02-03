using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int gold = 500;
    [SerializeField] int health = 10;
    [SerializeField] int maxHealth = 10;
    [SerializeField] int armor = 0;
    [SerializeField] float extraFightSuccess = 0;

    [SerializeField] BarMeter healthMeter;
    [SerializeField] BarMeter armorStats;
    [SerializeField] BarMeter weaponStats;

    static GameManager instance;
    public static int Gold
    {
        get
        {
            return instance.gold;
        }
        set
        {
            instance.gold = value;
            GoldUI.UpdateInstances(value);
        }
    }
    public static int Health
    {
        get
        {
            return instance.health;
        }
        set
        {
            instance.health = value;
            instance.healthMeter.UpdateUI(value);

            if (value <= 0)
            {
                // TODO: Implement Game Over system
            }
        }
    }
    public static int Armor
    {
        get
        {
            return instance.armor;
        }
        set
        {
            instance.armor = value;
            instance.armorStats.UpdateUI(value);
        }
    }
    public static float ExtraFightSuccess
    {
        get
        {
            return instance.extraFightSuccess;
        }
        set
        {
            instance.extraFightSuccess = value;
            instance.weaponStats.UpdateUI((int)(value * 10));
        }
    }

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        healthMeter.MaxValue = maxHealth;
        GoldUI.UpdateInstances(gold);
        Health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
