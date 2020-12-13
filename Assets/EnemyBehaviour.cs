using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullets")
        {
            Destroy(gameObject);
            Debug.Log("Collide");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Bullets")
        {
            Destroy(gameObject);
            Debug.Log("Collide");
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Bullets")
        {
            Destroy(gameObject);
            Debug.Log("Collide");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullets")
        {
            Destroy(gameObject);
            Debug.Log("Collide");
        }
    }
}
