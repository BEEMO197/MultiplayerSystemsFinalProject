using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public float health;
    public float maxHealth;

    void Start()
    {
        //slider.maxValue = maxHealth;
    }

    void Update()
    {
        slider.value = health;
    }

    public void SetMaxHealth(float newMaxHealth)
    {
        maxHealth = newMaxHealth;
        health = maxHealth;
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }

    public void SetHealth(float healthNum)
    {
        health = healthNum;
        //slider.value = health;
    }

    

}
