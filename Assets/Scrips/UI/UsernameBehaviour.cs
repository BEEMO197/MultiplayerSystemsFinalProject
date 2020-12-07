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
    public GameObject usernameOverHead;
    // Start is called before the first frame update
    void Start()
    {
        username.SetText(playerCube.GetComponent<PlayerCube>().playerRef.id);
        usernameOverHead.GetComponent<TextMeshPro>().SetText(playerCube.GetComponent<PlayerCube>().playerRef.id);
    }

    
}
