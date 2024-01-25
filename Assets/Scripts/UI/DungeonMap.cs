using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonMap : MonoBehaviour
{
    // Assign in the inspector
    [SerializeField] Button[] rooms = new Button[0];
    [SerializeField] Scrollbar scrollBar;

    void Awake()
    {
        scrollBar.value = 0;
    }

    void Update()
    {
        
    }
}
