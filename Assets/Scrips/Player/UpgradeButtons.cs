using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButtons : MonoBehaviour
{
    public Character player;
    public void OnHealthButtonPress()
    {
        float health = player.healthBar.maxHealth + 10;
        player.setHealth(health);
        player.healthBar.SetMaxHealth(health);
        player.upgradeVariables -= 1;
    }
    public void OnSpeedButtonPress()
    {
        player.setPlayerSpeed(player.getPlayerSpeed() + 1);
        player.upgradeVariables -= 1;
    }
    public void OnRangeButtonPress()
    {
        player.setRange(player.getRange() + 5);
        player.upgradeVariables -= 1;
    }
    public void OnBulletSpeedButtonPress()
    {
        player.setBulletSpeed(player.getBulletSpeed() + 0.1f);
        player.upgradeVariables -= 1;
    }
    public void OnDamageButtonPress()
    {
        player.setDamage(player.getDamage() + 1);
        player.upgradeVariables -= 1;
    }

}
