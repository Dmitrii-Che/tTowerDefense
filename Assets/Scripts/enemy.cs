using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    List<GameObject> wayPoints = new List<GameObject>();
    public Enemy selfEnemy = null;
    int wayIndex = 0, prize, demage;
    public int speed, helth;

    void Start()
    {
        GetWayPoints();
        if(selfEnemy != null)
        {
            helth = selfEnemy.helth;
            demage = selfEnemy.demage;
            speed = selfEnemy.speed;
            prize = selfEnemy.prize;
        }
    }

    public void TakeDemage(int dmgValue)
    {
        helth -= dmgValue;
    }

    public void MakeDemage(int dmgValue)
    {
        helth -= dmgValue;
    }



    bool isAlive()
    {
        return helth > 0;
    }
   
    void Update()
    {
        if(FindObjectOfType<mainController>().isGameOn)
        {
            if(isAlive())
            {
            Move();
            }
            else
            {
            Destroy(gameObject);
            FindObjectOfType<goldManager>().gold += prize;
            FindObjectOfType<goldManager>().killedEnemies ++;
            }
        }
    }

    void GetWayPoints()
    {
        wayPoints = GameObject.Find("lvlGroup").GetComponent<lvlManager>().wayPoints;
    }

    private void Move()
    {

        Transform curWayPoint = wayPoints[wayIndex].transform;
        Vector3 dir = curWayPoint.position - transform.position;

        if(dir[0] > 0 )
        {
            transform.localScale = new Vector2(1, 1);
        }
        else
        {
            transform.localScale = new Vector2(-1, 1);
        }

        transform.Translate(dir.normalized * Time.deltaTime * speed);

        if(Vector3.Distance(transform.position, wayPoints[wayIndex].transform.position) < 0.3f)
        {
            if(wayIndex < wayPoints.Count - 1)
                {
                    wayIndex++;
                }
            else
                {
                    Destroy(gameObject); /*дошли ло конца*/
                    FindObjectOfType<goldManager>().playerHelth -= demage;
                }
        }

    }
}
