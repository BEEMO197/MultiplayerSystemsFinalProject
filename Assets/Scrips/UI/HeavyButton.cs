using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class HeavyButton : MonoBehaviour
{
    public void OnButtonPress()
    {
        PlayerPrefs.SetString("Player_Class", ((int)Classes.HEAVY).ToString());
        SceneManager.LoadScene("Game");
    }
}
