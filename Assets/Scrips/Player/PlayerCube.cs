using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;

public class PlayerCube : Character
{
    
    public NetworkMan netWorkManRef;
    public NetworkMan.Player playerRef;
    public Camera cubeCamera;
<<<<<<< Updated upstream:Assets/Scrips/PlayerCube.cs
    public Vector3 Velocity;
    public GameObject bulletRef;
=======
    public Rigidbody rigidBody;
    public float speed = 5.0f;
    public Character characterRef;
>>>>>>> Stashed changes:Assets/Scrips/Player/PlayerCube.cs

    // Start is called before the first frame update
    void Start()
    {
        if(netWorkManRef.uniqueID.uniqueID == playerRef.id)
        {
            cubeCamera.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (netWorkManRef.uniqueID.uniqueID == playerRef.id)
        {
<<<<<<< Updated upstream:Assets/Scrips/PlayerCube.cs
            Velocity.x = Input.GetAxis("Horizontal");
            Velocity.z = Input.GetAxis("Vertical");

=======
            //Velocity.x = Input.GetAxis("Horizontal");
            //Velocity.z = Input.GetAxis("Vertical");
            rigidBody.velocity = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            rigidBody.velocity *= speed;
>>>>>>> Stashed changes:Assets/Scrips/Player/PlayerCube.cs
                
            //transform.position += Velocity;

            if (Input.GetAxis("Fire1") > 0)
            {
                GameObject.Instantiate(bulletRef, transform.position, transform.rotation);
            }

            //transform.position = new Vector3(netWorkManRef.lastestNewPlayer.newPlayer.color.R * 5, netWorkManRef.lastestNewPlayer.newPlayer.color.G * 5, netWorkManRef.lastestNewPlayer.newPlayer.color.B * 5);
        }
    }

}


