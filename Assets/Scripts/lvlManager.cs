using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class lvlManager : MonoBehaviour
{
    public int fieldW, fieldH, lvlNumber = 1;
    public GameObject cellPref, towerPref ;
    public Transform cellPrnt, towerPrnt;
    public Sprite[] tillSprite = new Sprite[2];
    public List<GameObject> wayPoints = new List<GameObject>();

    float cellSizeX; 
    float cellSizeY; 
    mainController controller;


    string[] pathArr;
    int currWayX, currWayY;
    GameObject startCell;
    GameObject[,] allCells = new   GameObject[10,18];

    string filename = "lvl.txt";

    void Start()
    {
        controller = FindObjectOfType<mainController>();
        cellSizeX = cellPref.GetComponent<SpriteRenderer>().bounds.size.x;
        cellSizeY = cellPref.GetComponent<SpriteRenderer>().bounds.size.y;
        
        CreateLvl();
        LoadWayPoints();
    }

    string[] loadLvl(int lvlNumber)
    {
        // TextAsset tempTxtAsset = Resources.Load<TextAsset>("lvl" + lvlNumber);
        // string tempStr = tempTxtAsset.text.Replace(Environment.NewLine, string.Empty);
        // // Debug.Log(tempStr);
        // return tempStr.Split(',');

        string tempStr = File.ReadAllText(System.IO.Directory.GetCurrentDirectory() + "/" + filename);
        return tempStr.Split('\n');
    }

    void CreateLvl()
    {
        pathArr = loadLvl(lvlNumber);
        Vector3 worldVec = Camera.main.ScreenToWorldPoint(new Vector3(25, Screen.height - 25, 0));
        for(int i = 0; i < fieldH; i++)
        {
            for(int j = 0; j < fieldW; j++)
            {   
                char ch = pathArr[i].ToCharArray()[j];
                if(ch == '1')
                {
                    CreateCell(j, i, worldVec, tillSprite[1], true);
                }
                else
                {
                    if(ch == '1' || ch == '2' || ch =='3' || ch == '4' || ch == '5' || ch == '6' 
                    || ch == '7' || ch == '8' || ch == '9' || ch == '0')
                    {
                        CreateCell(j, i, worldVec, tillSprite[0], false);
                    }

                    if(ch == '2')
                    {
                        bildTower(j, i, worldVec);
                    }
                }
            }
        }
    }

        void CreateCell(int X, int Y, Vector3 wV, Sprite spr, bool isWay)
    {
        GameObject tempCell = Instantiate(cellPref);
        tempCell.transform.SetParent(cellPrnt);
        tempCell.GetComponent<SpriteRenderer>().sprite = spr;
        tempCell.transform.position = new Vector3(wV.x + (cellSizeX * X), wV.y - (cellSizeY * Y));

        if(isWay) 
        {
            tempCell.GetComponent<cell>().isWay = true;
            if(startCell == null)
            {
                startCell = tempCell;
                currWayX = X;
                currWayY = Y;
            }
        }

        allCells[Y,X] = tempCell;
    }

    void bildTower(int X, int Y, Vector3 wV)
    {
        GameObject tempTower = Instantiate(towerPref);
        tempTower.transform.SetParent(towerPrnt, false);
        tempTower.transform.position = new Vector3(wV.x + (cellSizeX * X), wV.y - (cellSizeY * Y));
        int tempTowerRank = 0;
        tempTower.GetComponent<tower>().selfTower = new Tower(controller.towers[tempTowerRank]);
    }

    void LoadWayPoints()
    {
        GameObject currWayGo;
        wayPoints.Add(startCell);

        while (true)
        {
            currWayGo = null ;
            if(currWayX >0 
            && allCells[currWayY, currWayX - 1].GetComponent<cell>().isWay
            && !wayPoints.Exists(x => x == allCells[currWayY, currWayX-1])
            )/*nxt cell left*/
            {
                currWayGo = allCells[currWayY, currWayX - 1];
                currWayX--;
            }
            else if(currWayX < (fieldW - 1) 
            && allCells[currWayY, currWayX + 1].GetComponent<cell>().isWay
            && !wayPoints.Exists(x => x == allCells[currWayY, currWayX + 1])
            )/*nxt cell right*/
            {
                currWayGo = allCells[currWayY, currWayX + 1];
                currWayX++;
            }
            else if(currWayY > 0 
            && allCells[currWayY - 1, currWayX].GetComponent<cell>().isWay
            && !wayPoints.Exists(x => x == allCells[currWayY - 1, currWayX])
            )/*nxt cell up*/
            {
                currWayGo = allCells[currWayY - 1, currWayX];
                currWayY--;
            }
            else if(currWayY < (fieldH - 1) 
            && allCells[currWayY + 1, currWayX].GetComponent<cell>().isWay
            && !wayPoints.Exists(x => x == allCells[currWayY + 1, currWayX])
            )/*nxt cell down*/
            {
                currWayGo = allCells[currWayY + 1, currWayX];
                currWayY++;
            }
            else
            {
                break;
            }
            wayPoints.Add(currWayGo);
        }
    }
}
