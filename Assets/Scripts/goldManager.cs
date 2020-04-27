using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class goldManager : MonoBehaviour
{
    public static goldManager goldMngr;
    public Text goldText, helthText, killedEnemiesText;
    public int gold, playerHelth, killedEnemies;

    void Awake()
    {
         goldMngr = this; 
    }

    void Start()
    {
        gold = 100;
        playerHelth = 10;
        killedEnemies = 0;
    }

    void Update()
    {
        goldText.text = gold.ToString();
        helthText.text = playerHelth.ToString();
        killedEnemiesText.text = "killed enemies: " + killedEnemies.ToString();
        
        checkGameOn();
    }

    void checkGameOn()
    {   
        if(playerHelth <= 0)
        {
            FindObjectOfType<mainController>().isGameOn = false;
        }
    }
}
