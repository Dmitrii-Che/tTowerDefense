using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerBullet : MonoBehaviour
{
    public Transform target;
    public TowerBullet selfBullet;
    
    public void SetTarget(Transform targetObject)
    {
        target = targetObject;
    }

    void Move()
    {
        if(target != null)
        {
            if(Vector2.Distance(transform.position, target.position) < .1f)
            {
                target.GetComponent<enemy>().TakeDemage(selfBullet.demage);
                Destroy(gameObject);
            }
            else
            {
             Vector2 dir = target.position - transform.position;
                transform.Translate(dir.normalized * Time.deltaTime * selfBullet.speed);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = selfBullet.spriteBullet;
    }

    void Update()
    {
        Move();
    }
}
