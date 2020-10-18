using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Velocity = transform.forward;
        transform.position += Velocity;
        Destroy(gameObject, 0.3f);
    }
}
