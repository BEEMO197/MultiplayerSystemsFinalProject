using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int health;
    private int damage;
    private float scoreGiven;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public float giveScore()
    {
        return scoreGiven;
    }

    public void setScore(float _score)
    {
        scoreGiven = _score;
    }

}
