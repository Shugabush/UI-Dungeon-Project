using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int gold = 500;
    [SerializeField] int health = 10;
    [SerializeField] int maxHealth = 10;

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
            HealthBar.UpdateHealth(value, instance.maxHealth);
        }
    }

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GoldUI.UpdateInstances(gold);
        Health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
