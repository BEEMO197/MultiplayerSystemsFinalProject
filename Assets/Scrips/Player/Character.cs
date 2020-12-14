using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public enum Classes
{
    DEFAULT,
    ARCHER,
    FIGHTER,
    HEAVY,
    ROGUE,
    count
};

public class Character : MonoBehaviour
{
    // Client VARIABLES
    public Classes currentClass = Classes.DEFAULT;
    public Mesh[] characterMesh;
    public MeshFilter meshFilter;
    // Game Variables
    public float health;
    public float damage;
    public float playerSpeed;
    public float bulletSpeed;
    public float range;
    public float upgradeVariables = 0;
    public bool isSetToDie = false;

    // Ui Variables
    public int level = 1;
    public int score = 0;
    public float xpNum;
    public float maxXp = 100;
    public TextMeshProUGUI healthText, speedText, damageText, rangeText, bulletSpeedText, upgrades, levels, scoreText;
    public GameObject upgradePanel;

    // Components
    public Camera characterCamera;
    public Canvas characterCanvas;
    public AudioListener characterAudioListener;
    public HealthBar healthBar;
    public GameObject bulletRef;
    public Rigidbody rigidBody;
    public TextMeshProUGUI username;
    public GameObject usernameOverhead;
    public XpBar xp;
    // Server Variables
    public NetworkClient networkManRef;
    public NetworkObjects.NetworkPlayer playerRef;

    // Start is called before the first frame update
    void Start()
    {
        //meshFilter.mesh = characterMesh[PlayerPrefs.GetInt("Character_Selected_Class") - 1];
        //GetComponent<MeshFilter>().mesh = characterMesh[0];
        if (networkManRef != null)
        {
            if (networkManRef.clientID == playerRef.id)
            {
                characterCamera.enabled = true;
                characterCanvas.enabled = true;
                characterAudioListener.enabled = true;

                playerRef.cube = gameObject;

                setStats();

            }
        }
        maxXp = xp.maxXP;
    }

    // Update is called once per frame
    void Update()
    {
        if (networkManRef != null)
        {
            if (networkManRef.clientID == playerRef.id)
            {
                //Velocity.x = Input.GetAxis("Horizontal");
                //Velocity.z = Input.GetAxis("Vertical");

                //transform.Rotate(0.0f, Input.GetAxis("Mouse X"), 0.0f);

                Vector3 velocityR = new Vector3(0.0f, 0.0f, 0.0f);
                Vector3 velocityF = new Vector3(0.0f, 0.0f, 0.0f);

                if (Input.GetAxis("Horizontal") != 0)
                {
                    //velocityR = (transform.right * Input.GetAxis("Horizontal")) * playerSpeed;
                    transform.Rotate(0.0f, Input.GetAxis("Horizontal") * 0.5f, 0.0f);
                }

                if (Input.GetAxis("Vertical") != 0)
                {
                    velocityF = (transform.forward * Input.GetAxis("Vertical")) * playerSpeed;
                }

                rigidBody.velocity = velocityR + velocityF;

                velocityR = new Vector3(0.0f, 0.0f, 0.0f);
                velocityF = new Vector3(0.0f, 0.0f, 0.0f);

                //rigidBody.velocity *= speed;
                //Vector3.ClampMagnitude(rigidBody.velocity, speed);
                //transform.position += Velocity;

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    //if (Time.frameCount % 40 == 0)
                    //{
                    //GameObject.Instantiate(bulletRef, transform.position + (characterCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.0f)) - transform.position).normalized, Quaternion.LookRotation((characterCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.0f)) - transform.position).normalized));
                    //Debug.Log(characterCamera.ScreenToWorldPoint(Input.mousePosition));
                    //Debug.Log(characterCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.0f)));
                    //Debug.Log("World mouse position: " + characterCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition, characterCamera.nearClipPlane));
                    Ray ray = characterCamera.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        Vector3 clickPosition = hit.point;
                        clickPosition.y = 1.0f;
                        //Debug.Log(hit.point);
                        //Debug.Log("Player Position: " + transform.position);
                        bulletRef.GetComponent<BulletBehaviour>().range = getRange();
                        bulletRef.GetComponent<BulletBehaviour>().speed = getBulletSpeed();
                        bulletRef.GetComponent<BulletBehaviour>().damage = getDamage();

                        GameObject.Instantiate(bulletRef, (transform.position + (clickPosition - transform.position).normalized), Quaternion.LookRotation((clickPosition - transform.position).normalized));

                    }


                    //}
                }

                playerRef.cubPos = transform.position;
                playerRef.cubRot = transform.rotation;
            }
        }
        if(xpNum >= maxXp)
        {
            xpNum -= maxXp;
            maxXp *= 1.1f;
            //levelup
            AddLevels();
        }

        healthText.SetText(health.ToString());
        speedText.SetText(playerSpeed.ToString());
        bulletSpeedText.SetText(bulletSpeed.ToString());
        rangeText.SetText(range.ToString());
        damageText.SetText(damage.ToString());
        upgrades.SetText(upgradeVariables.ToString());
        levels.SetText(level.ToString());
        scoreText.SetText(score.ToString());
        if (upgradeVariables >= 1)
        {
            upgradePanel.SetActive(true);
        }
        else
        {
            upgradePanel.SetActive(false);
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

    public float getPlayerSpeed()
    {
        return playerSpeed;
    }

    public float getBulletSpeed()
    {
        return bulletSpeed;
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
    public void setPlayerSpeed(float _speed)
    {
        playerSpeed = _speed;
    }

    public void setBulletSpeed(float _speed)
    {
        bulletSpeed = _speed;
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

    public void setStats()
    {
        // Set Class
        gameObject.GetComponent<MeshFilter>().mesh = characterMesh[int.Parse(PlayerPrefs.GetString("Player_Class"))];
        setClass((Classes)int.Parse(PlayerPrefs.GetString("Player_Class")));

        // Set Username
        username.SetText(PlayerPrefs.GetString("Player_Username"));
        usernameOverhead.GetComponent<TextMeshPro>().SetText(PlayerPrefs.GetString("Player_Username"));

        setLevel(int.Parse(PlayerPrefs.GetString("Player_Level")));
        setScore(int.Parse(PlayerPrefs.GetString("Player_Score")));
        setHealth(int.Parse(PlayerPrefs.GetString("Player_Health")));
    }

    public void setClass(Classes newClass)
    {
        currentClass = newClass;
        //set stats based on newClass
        //use switch case -Monkey man
        switch(currentClass)
        {
            case Classes.ARCHER:
                setRange(200.0f);
                setDamage(5.0f);
                setPlayerSpeed(15.0f);
                setBulletSpeed(5.0f);
                setHealth(80.0f);
                break;
                
            case Classes.FIGHTER:
                setRange(75.0f);
                setDamage(15.0f);
                setPlayerSpeed(10.0f);
                setBulletSpeed(0.5f);
                setHealth(110.0f);
                break;
                
            case Classes.HEAVY:
                setRange(50.0f);
                setDamage(50.0f);
                setPlayerSpeed(5.0f);
                setBulletSpeed(0.2f);
                setHealth(200.0f);
                break;

            case Classes.ROGUE:
                setRange(50.0f);
                setDamage(12.0f);
                setPlayerSpeed(20.0f);
                setBulletSpeed(3.0f);
                setHealth(60.0f);
                break;
            default:
                Debug.Log("No class set! Default attributes have been set.");
                setRange(50.0f);
                setDamage(10.0f);
                setPlayerSpeed(20.0f);
                setBulletSpeed(1.0f);
                setHealth(100.0f);
                break;
        }
        healthBar.SetMaxHealth(getHealth());
        
    }

    public void takeDamage(float damageTaken)
    {
        health -= damageTaken;
        if(health <= 0)
        {
            isSetToDie = true;
        }
        else
        {
            healthBar.SetHealth(health);
            //healthBar.slider.value = 50.0f;
            
        }
    }

    public void GainXp(float _xp)
    {
        xpNum += _xp;
        xp.SetXp(xpNum);
    }

    public void AddLevels()
    {
        level += 1;
        xp.LevelUp();
        upgradeVariables += 1;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Take damage");
            takeDamage(10.0f);
            other.gameObject.GetComponent<EnemyBehaviour>().TakeDamage(10.0f);
            GainXp(10);
        }
    }


}


