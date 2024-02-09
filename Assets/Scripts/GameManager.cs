using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int gold = 500;
    [SerializeField] int health = 10;
    [SerializeField] int maxHealth = 10;
    [SerializeField] int armor = 0;
    [SerializeField] int extraFightSuccess = 0;

    [SerializeField] BarMeter healthMeter;
    [SerializeField] BarMeter armorStats;
    [SerializeField] BarMeter weaponStats;

    const int lowHealthIndicatorThreshold = 4;

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
            GoldUI.UpdateText(value);
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

            if (value < lowHealthIndicatorThreshold)
            {
                // Display low health indicator
                LowHealthIndicator.Enable();
            }
            else
            {
                // Hide low health indicator
                LowHealthIndicator.Disable();
            }

            if (value <= 0)
            {
                // TODO: Implement Game Over system
                GameOverScreen.Trigger();
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
            ArmorStats.UpdateUI(value);
        }
    }
    public static int ExtraFightSuccess
    {
        get
        {
            return instance.extraFightSuccess;
        }
        set
        {
            instance.extraFightSuccess = value;
            WeaponStats.UpdateUI((int)(value / 10f));
        }
    }

    bool isInvisible;
    public static bool IsInvisible
    {
        get
        {
            return instance.isInvisible;
        }
        set
        {
            instance.isInvisible = value;
            if (value)
            {
                // Trigger some UI displaying the invisibility
            }
        }
    }

    public static BarMeter HealthMeter => instance.healthMeter;
    public static BarMeter ArmorStats => instance.armorStats;
    public static BarMeter WeaponStats => instance.weaponStats;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        healthMeter.MaxValue = maxHealth;
        GoldUI.UpdateText(gold);
        Health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
