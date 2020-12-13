using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UsernameBehaviour : MonoBehaviour
{
    public GameObject playerCube;
    public TextMeshProUGUI username;
    // Start is called before the first frame update
    void Start()
    {
        
        if(PlayerPrefs.GetString("Player_Username") == "")
        {
            username.SetText(playerCube.GetComponent<PlayerCube>().playerRef.id);
        }
        else
        {
            
        }
    }

    
}
