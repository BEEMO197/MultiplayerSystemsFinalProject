using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;

public class PlayerCube : Character
{
    
    public NetworkMan netWorkManRef;
    public NetworkMan.Player playerRef;
    public Camera cubeCamera;
    public Canvas cubeCanvas;

    public GameObject bulletRef;
    public Rigidbody rigidBody;
    public float speed = 20.0f;
    public Character characterRef;

    // Start is called before the first frame update
    void Start()
    {
        if(netWorkManRef.uniqueID.uniqueID == playerRef.id)
        {
            cubeCamera.enabled = true;
            cubeCanvas.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (netWorkManRef.uniqueID.uniqueID == playerRef.id)
        {
            //Velocity.x = Input.GetAxis("Horizontal");
            //Velocity.z = Input.GetAxis("Vertical");

            transform.Rotate(0.0f, Input.GetAxis("Mouse X"), 0.0f);

            if (Input.GetAxis("Horizontal") != 0)
            {
                rigidBody.velocity = (transform.right * Input.GetAxis("Horizontal")) * speed;
            }

            if(Input.GetAxis("Vertical") != 0)
            {
                rigidBody.velocity = (transform.forward * Input.GetAxis("Vertical")) * speed;
            }

            //rigidBody.velocity *= speed;
            //Vector3.ClampMagnitude(rigidBody.velocity, speed);
            //transform.position += Velocity;

            if (Input.GetAxis("Fire1") > 0)
            {
                if (Time.frameCount % 40 == 0)
                {
                    GameObject.Instantiate(bulletRef, transform.position + transform.forward, transform.rotation);
                }
            }

            //transform.position = new Vector3(netWorkManRef.lastestNewPlayer.newPlayer.color.R * 5, netWorkManRef.lastestNewPlayer.newPlayer.color.G * 5, netWorkManRef.lastestNewPlayer.newPlayer.color.B * 5);
        }
    }

}


