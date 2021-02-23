using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class enemySpawner : MonoBehaviour
{
    public float timeToSpawn = 5, enemyDelay = 0.4f;
    public GameObject enemyPref;
    public int extraNumberOfEnemies = 5;
    mainController controller;

    int waveCount = 1;
    float timeToSpawtSyst;
    bool isGameOn = false;
    string filename = "config.txt";

    public static System.Random rnd = new System.Random(DateTime.Now.Minute*60000+DateTime.Now.Second*1000+DateTime.Now.Millisecond);

    void Awake()
    {
        timeToSpawn = loadTimeToSpawn();
        timeToSpawtSyst = timeToSpawn;
    }

    void Start()
    {
        controller = FindObjectOfType<mainController>();
    }

    int loadTimeToSpawn()
    {
        string tempStr = File.ReadAllText(System.IO.Directory.GetCurrentDirectory() + "/" + filename);
        // Debug.Log("tempStr: " + tempStr);
        return Int32.Parse(tempStr);
    }

    void Update()
    {
        isGameOn = controller.isGameOn;
        if(isGameOn)
        {
            if(timeToSpawn <= 0)
            {
                StartCoroutine(startNewWave(rnd.Next(waveCount, extraNumberOfEnemies + waveCount)));
                timeToSpawn = timeToSpawtSyst;
            }
            timeToSpawn -= Time.deltaTime;
        }
    }

    /*K*/IEnumerator startNewWave(int enemyCount) 
    {
        waveCount++;

        for(int i = 0; i < enemyCount; i++)
        {
            GameObject  tempEnemy = Instantiate(enemyPref);
            tempEnemy.transform.SetParent(gameObject.transform, false);
            tempEnemy.transform.position = GameObject.Find("lvlGroup").GetComponent<lvlManager>().wayPoints[0].transform.position;
            tempEnemy.GetComponent<enemy>().selfEnemy = new Enemy(controller.enemies[0]);
            tempEnemy.GetComponent<enemy>().selfEnemy.helth += (waveCount * 10); /*uvelichivaem zjizn'*/
            /*mozjno sdelat' otdel'nuyu F dlya upgreda vragov*/
            yield return new WaitForSeconds(enemyDelay);
        }

        
    }
}
