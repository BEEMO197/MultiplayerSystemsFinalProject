using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FighterButton : MonoBehaviour
{
    public void OnButtonPress()
    {
        PlayerPrefs.SetInt("Character_Selected_Class", (int)Classes.FIGHTER);
        SceneManager.LoadScene("Game");
    }
}
