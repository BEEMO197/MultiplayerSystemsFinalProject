using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    

    
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullets")
        {
            Debug.Log("OnTriggerEnter");
            Destroy(gameObject);

        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Bullets")
        {
            Debug.Log("OnTriggerExit");
            Destroy(gameObject);

        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Bullets")
        {
            Debug.Log("OnTriggerStay");
            Destroy(gameObject);

        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullets")
        {
            Debug.Log("OnCollisionEnter");
            Destroy(gameObject);

        }
    }
}
