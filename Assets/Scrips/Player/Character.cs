using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // Client VARIABLES

    // Game Variables
    private float health = 100.0f;
    private float damage = 10.0f;
    private float speed = 10.0f;
    private float range = 100.0f;

    // Ui Variables
    private int level = 1;
    private int score = 0;

    // Components
    public Camera characterCamera;
    public Canvas characterCanvas;
    public AudioListener characterAudioListener;

    public GameObject bulletRef;
    public Rigidbody rigidBody;

    // Server Variables
    public NetworkClient networkManRef;
    public NetworkObjects.NetworkPlayer playerRef;

    // Start is called before the first frame update
    void Start()
    {
        if (networkManRef.clientID == playerRef.id)
        {
            characterCamera.enabled = true;
            characterCanvas.enabled = true;
            characterAudioListener.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (networkManRef.clientID == playerRef.id)
        {
            //Velocity.x = Input.GetAxis("Horizontal");
            //Velocity.z = Input.GetAxis("Vertical");

            transform.Rotate(0.0f, Input.GetAxis("Mouse X"), 0.0f);

            Vector3 velocityR = new Vector3(0.0f, 0.0f, 0.0f);
            Vector3 velocityF = new Vector3(0.0f, 0.0f, 0.0f);

            if (Input.GetAxis("Horizontal") != 0)
            {
                velocityR = (transform.right * Input.GetAxis("Horizontal")) * speed;
            }

            if (Input.GetAxis("Vertical") != 0)
            {
                velocityF = (transform.forward * Input.GetAxis("Vertical")) * speed;
            }

            rigidBody.velocity = velocityR + velocityF;

            velocityR = new Vector3(0.0f, 0.0f, 0.0f);
            velocityF = new Vector3(0.0f, 0.0f, 0.0f);

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
        }
    }
    public float getHealth()
    {
        return health;
    }

    public float getDamage()
    {
        return damage;
    }

    public float getSpeed()
    {
        return speed;
    }

    public float getRange()
    {
        return range;
    }

    public int getLevel()
    {
        return level;
    }

    public int getScore()
    {
        return score;
    }

    public void setHealth(float _health)
    {
        health = _health;
    }

    public void setDamage(float _damage)
    {
        damage = _damage;
    }
    public void setSpeed(float _speed)
    {
        speed = _speed;
    }

    public void setRange(float _range)
    {
        range = _range;
    }

    public void setLevel(int _level)
    {
        level = _level;
    }

    public void setScore(int _score)
    {
        score = _score;
    }


}


