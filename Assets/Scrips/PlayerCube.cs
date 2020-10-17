using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCube : MonoBehaviour
{
    
    public NetworkMan netWorkManRef;
    public NetworkMan.Player playerRef;
    public Camera cubeCamera;
    public Character characterRef;
    public Vector3 Velocity;

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

            Debug.Log(Velocity);
                
            transform.position += Velocity;

            //transform.position = new Vector3(netWorkManRef.lastestNewPlayer.newPlayer.color.R * 5, netWorkManRef.lastestNewPlayer.newPlayer.color.G * 5, netWorkManRef.lastestNewPlayer.newPlayer.color.B * 5);
        }
    }


}
