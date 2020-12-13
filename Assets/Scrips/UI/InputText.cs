using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InputText : MonoBehaviour
{
    public string username;
    public TMP_InputField userInputField;
    public TextMeshProUGUI userWarning;

    public string password;
    public TMP_InputField passInputField;
    public TextMeshProUGUI passWarning;

    public void setUsername()
    {
        if (userInputField.text == "")
        {
            userWarning.text = "Input username";
        }
        else
        {
            Debug.Log("username found");
            username = userInputField.text;
            PlayerPrefs.SetString("Player_Username", username);
        }
    }
    public void setPassword()
    {

        if (passInputField.text == "")
        {
            passWarning.text = "Input password";
        }
        else
        {
            Debug.Log("password found");

            password = passInputField.text;
            PlayerPrefs.SetString("Player_Password", password);
        }
    }

    public void sceneLoad()
    {
        if(password != "" && username != "")
        {
            SceneManager.LoadScene("Character Select");
        }
    }

}
