using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ArcherButton : MonoBehaviour
{
    public void OnButtonPress()
    {
        PlayerPrefs.SetString("Player_Class", ((int)Classes.ARCHER).ToString());
        SceneManager.LoadScene("Game");
    }
}
