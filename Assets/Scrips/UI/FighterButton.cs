using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FighterButton : MonoBehaviour
{
    public void OnButtonPress()
    {
        PlayerPrefs.SetString("Player_Class", ((int)Classes.FIGHTER).ToString());
        SceneManager.LoadScene("Game");
    }
}
