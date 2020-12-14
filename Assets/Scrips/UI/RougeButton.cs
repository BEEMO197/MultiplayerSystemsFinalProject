using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RougeButton : MonoBehaviour
{

    public void OnButtonPress()
    {
        PlayerPrefs.SetString("Player_Class", ((int)Classes.ROGUE).ToString());
        SceneManager.LoadScene("Game");
    }
}
