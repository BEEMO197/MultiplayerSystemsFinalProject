using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{

    public float range;
    public float damage;
    public float speed;
    Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Velocity = transform.forward * speed;

        transform.position += Velocity;
        



        if (Vector3.Distance(startPosition, transform.position) >= range)
        {
            Destroy(gameObject);
        }

    }
}
