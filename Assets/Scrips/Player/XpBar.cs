using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class XpBar : MonoBehaviour
{
    public Slider slider;

    public float xp = 0;
    public float maxXP = 100;

    void Start()
    {
        //slider.maxValue = maxHealth;
        slider.maxValue = maxXP;
    }

    void Update()
    {
        slider.value = xp;
    }

    //public void SetMaxXp(float newMaxXP)
    //{
    //    maxXP = newMaxXP;
    //    health = maxXP;
    //    slider.maxValue = maxXP;
    //    slider.value = maxXP;
    //}

    public void SetXp(float xpNum)
    {
        xp = xpNum;
        //slider.value = health;
    }
    public void LevelUp()
    {
        if(xp >= maxXP)
        {
            xp = 0;
            slider.value = xp;
            maxXP *= 1.1f;
            slider.maxValue = maxXP;
        }
    }


}
