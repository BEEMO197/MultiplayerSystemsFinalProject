using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    NetworkObjects.NetworkPlayer connectedPlayer;
    NetworkClient connectedClient;
    public float health = 100;
    public HealthBar healthBar;
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
        }
    }

    public void TakeDamage(float damage)
    {
        healthBar.SetHealth(damage);
    }
}
