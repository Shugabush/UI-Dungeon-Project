using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int gold = 500;

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

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GoldUI.UpdateInstances(gold);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
