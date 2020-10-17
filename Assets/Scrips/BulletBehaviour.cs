using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{

    public Vector3 Velocity = new Vector3(0.0f, 0.0f, 0.2f);


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Velocity;
        Destroy(gameObject, 0.3f);
    }
}
