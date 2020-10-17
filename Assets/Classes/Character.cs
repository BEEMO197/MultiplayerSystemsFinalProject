using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    private float health = 100;
    private float damage = 10;
    private float speed = 10;
    private float range = 100;
    private int level = 1;
    private int score = 0;




    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public float getHealth()
    {
        return health;
    }

    public float getDamage()
    {
        return damage;
    }

    public float getSpeed()
    {
        return speed;
    }

    public float getRange()
    {
        return range;
    }

    public int getLevel()
    {
        return level;
    }

    public int getScore()
    {
        return score;
    }

    public void setHealth(float _health)
    {
        health = _health;
    }

    public void setDamage(float _damage)
    {
       damage = _damage;
    }
    public void setSpeed(float _speed)
    {
       speed = _speed;
    }

    public void setRange(float _range)
    {
        range = _range;
    }

    public void setLevel(int _level)
    {
        level = _level;
    }

    public void setScore(int _score)
    {
        score = _score;
    }


}
