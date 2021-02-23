using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower
{
    public float range, currCooldDown, coolDown;
    public Sprite spriteTower;
    public int rank, updCost;

    public Tower(float _range, float _coolDown, string _path, int _rank, int _updCost)
    {
        range = _range;
        coolDown = _coolDown;
        currCooldDown = 0;
        rank = _rank;
        updCost = _updCost;
        spriteTower = Resources.Load<Sprite>(_path);
    }

        public Tower(Tower other)
    {
        range = other.range;
        coolDown = other.coolDown;
        rank = other.rank;
        currCooldDown = 0;
        spriteTower = other.spriteTower; 
        updCost = other.updCost;

    }
}

public class TowerBullet
{
    public float speed;
    public int demage;
    public Sprite spriteBullet;

    public TowerBullet(float _speed, int _demage, Sprite _spriteBullet)
    {
        this.speed = _speed;
        this.demage = _demage;
        this.spriteBullet = _spriteBullet;
    }

    public TowerBullet(float _speed, int _demage, string path)
    {
        this.speed = _speed;
        this.demage = _demage;
        spriteBullet = Resources.Load<Sprite>(path);
    }
}

public class Enemy
{
    public int helth, demage, speed, prize;
    public Enemy(int _helth, int _demage, int _speed, int _prize)
    {
        helth = _helth;
        demage = _demage;
        speed = _speed;
        prize = _prize;
    }
    public Enemy(Enemy other)
    {
        helth = other.helth;
        demage = other.demage;
        speed = other.speed;
        prize = other.prize;
    }
}

public class mainController : MonoBehaviour
{
    public List<Tower> towers = new List<Tower>();
    public List<TowerBullet> bullets = new List<TowerBullet>();
    public List<Enemy> enemies = new List<Enemy>();

    public bool isGameOn;

    GameObject restartPanel, startPanel;

    void Awake()
    {
        isGameOn = true;
        restartPanel = GameObject.Find("PanelRestart");
        restartPanel.SetActive(false);

        towers.Add(new Tower(0, 0f, "Towers/twr00", 0, 5));
        towers.Add(new Tower(3, 1f, "Towers/twr01", 1, 10));
        towers.Add(new Tower(3, 0.5f, "Towers/twr011", 2, 30));
        towers.Add(new Tower(5, 0.5f, "Towers/twr012", 3, 50));
        towers.Add(new Tower(10, 0.01f, "Towers/twr21", 3, 80));

        bullets.Add(new TowerBullet(0,0,"Bullets/ydr0"));
        bullets.Add(new TowerBullet(10,20,"Bullets/ydr0"));
        bullets.Add(new TowerBullet(10,20,"Bullets/ydr0"));
        bullets.Add(new TowerBullet(20,30,"Bullets/ydr1"));
        bullets.Add(new TowerBullet(20,30,"Bullets/ydr1"));

        enemies.Add(new Enemy(30, 1, 4, 5 ));
    }

    void Update()
    {
        if(!isGameOn)
        {
        restartPanel.SetActive(true);
        }
    }
}
