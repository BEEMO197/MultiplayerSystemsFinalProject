using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCube : Character
{
    
    public NetworkMan netWorkManRef;
    public NetworkMan.Player playerRef;
    public Camera cubeCamera;
    public Vector3 Velocity;
    public GameObject bulletRef;

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
            Velocity.x = Input.GetAxis("Horizontal");
            Velocity.z = Input.GetAxis("Vertical");

                
            transform.position += Velocity;

            if (Input.GetAxis("Fire1") > 0)
            {
                GameObject.Instantiate(bulletRef, transform.position, transform.rotation);
            }

            //transform.position = new Vector3(netWorkManRef.lastestNewPlayer.newPlayer.color.R * 5, netWorkManRef.lastestNewPlayer.newPlayer.color.G * 5, netWorkManRef.lastestNewPlayer.newPlayer.color.B * 5);
        }
    }

}


