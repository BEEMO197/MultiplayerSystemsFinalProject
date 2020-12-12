using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RougeButton : MonoBehaviour
{

    public void OnButtonPress()
    {
        PlayerPrefs.SetInt("Character_Selected_Class", (int)Classes.ROGUE);
        SceneManager.LoadScene("Game");
    }
}
