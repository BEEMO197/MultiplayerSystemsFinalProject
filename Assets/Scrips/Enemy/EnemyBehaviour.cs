using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    NetworkObjects.NetworkPlayer connectedPlayer;
    NetworkClient connectedClient;
    public float health = 100;
    public HealthBar healthBar;
    public bool isSetToDie = false;
    void Start()
    {
        healthBar.SetMaxHealth(health);
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Bullets"))
        {
            // Damage Enemy
            TakeDamage(col.gameObject.GetComponent<BulletBehaviour>().damage);
            col.gameObject.GetComponent<BulletBehaviour>().client.connectedPlayer.cube.GetComponent<Character>().GainXp(10);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.SetHealth(health);
        if(health <= 0)
        {
            isSetToDie = true;
        }
    }
}
