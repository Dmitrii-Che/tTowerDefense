using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tower : MonoBehaviour
{
    public GameObject shot; 
    GameObject UpdateTowerPanel;
    mainController controller;
    public Tower selfTower = null;
    int rank, maxRank;
    public Color baseColor, actColor;
    public GameObject updatePref;
    bool canUpdate = false, isGameOn;

    void Update()
    {
        isGameOn = FindObjectOfType<mainController>().isGameOn;
        if(isGameOn)
        {
            canUpdate = FindObjectsOfType<updatePanel>().Length == 0 && selfTower != null && rank < controller.towers.Count - 1 && isGameOn;

            if (selfTower.currCooldDown >= 0)
            {
                selfTower.currCooldDown -= Time.deltaTime;
            } 
        }
    }

    void OnMouseEnter()
    {
        GetComponent<SpriteRenderer>().color = actColor;
        makeUpdateTowerPanel();
    }

    void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = baseColor;
        Destroy(UpdateTowerPanel);
    }

    void OnMouseDown()
    {
        if(FindObjectOfType<goldManager>().gold >= selfTower.updCost && rank < controller.towers.Count - 1 && isGameOn)
        {
        FindObjectOfType<goldManager>().gold -= selfTower.updCost;
        TowerUpdate();
        Destroy(UpdateTowerPanel);
        makeUpdateTowerPanel();
        }      
    }

    void makeUpdateTowerPanel()
    {
        if(rank < controller.towers.Count - 1 && selfTower != null)
        {
            UpdateTowerPanel = Instantiate(updatePref);
            UpdateTowerPanel.transform.SetParent(GameObject.Find("Canvas").transform, false);
            UpdateTowerPanel.transform.position = transform.position;
            UpdateTowerPanel.GetComponent<updatePanel>().costText.text = selfTower.updCost.ToString(); 
        }
    }

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = selfTower.spriteTower;

        controller = FindObjectOfType<mainController>();
        rank = selfTower.rank;
        maxRank = controller.towers.Count - 1;
        InvokeRepeating("SearchTarget", 0, 1f); /*zapusk F s periodich. povtorom*/
    }

    bool CanShoot()
    {
        return selfTower.currCooldDown <= 0 ? true : false;
    }

    public void TowerUpdate() 
    {
        if(selfTower.rank < maxRank)
        {
           rank++;
           selfTower = new Tower(controller.towers[rank]);
           GetComponent<SpriteRenderer>().sprite = selfTower.spriteTower;          
        }
    }

    void SearchTarget()
    {
        if(CanShoot() && isGameOn){
            Transform nearestTarget = null;
            float nearestTargetDist = Mathf.Infinity;
            foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                float currDist = Vector2.Distance(transform.position, enemy.transform.position); /*ishem blijaiishego vraga*/

                if(currDist < nearestTargetDist && currDist < selfTower.range) /*provrka distancii dlya strel'by*/
                {
                 nearestTarget = enemy.transform;
                    nearestTargetDist = currDist;
                }
            }   

            if(nearestTarget != null)
            {
                Shoot(nearestTarget);
            }
        }
    }

    void Shoot(Transform enemy)
    {
        selfTower.currCooldDown = selfTower.coolDown; /*sbros timera*/
        GameObject tempShot = Instantiate(shot); /*sozdanie ob\ekta vystrela*/
        tempShot.GetComponent<towerBullet>().selfBullet = controller.bullets[rank];
        tempShot.transform.position = transform.position;
        tempShot.GetComponent<towerBullet>().target = enemy;
    }
}
