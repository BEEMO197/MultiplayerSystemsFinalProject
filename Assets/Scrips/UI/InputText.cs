using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

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
            //Debug.Log("username found");
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
            //Debug.Log("password found");

            password = passInputField.text;
            PlayerPrefs.SetString("Player_Password", password);
        }
    }

    public void sceneLoad()
    {
        
        if (password != "" && username != "")
        {
            StartCoroutine(GetUserInfo());
            //if (data == "User Created")
            //{
            //    //Debug.Log("User was Created");
            //    //SceneManager.LoadScene("Character Select");
            //}
            //
            //else if(data == "Bad Password... Try again?")
            //{
            //    //Debug.Log("BadPassword");
            //    userWarning.text = "User Exists, Password is bad";
            //    passWarning.text = data;
            //}
            //
            //else
            //{
            //    //Debug.Log("Found User Welcome");
            //    //SceneManager.LoadScene("Character Select");
            //}
        }
    }

    IEnumerator GetUserInfo()
    {
        string siteUrl = "https://w5zfcqda33.execute-api.us-east-1.amazonaws.com/default/CheckUsernameAndPassword";
        //string data = "";
        //?Username=User&Password=Password
        siteUrl += "?Username=" + username + "&Password=" + password;

        NetworkObjects.NetworkPlayerJson data = new NetworkObjects.NetworkPlayerJson();
        UnityWebRequest www = UnityWebRequest.Get(siteUrl);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            data = JsonUtility.FromJson<NetworkObjects.NetworkPlayerJson>(www.downloadHandler.text);
        }


        if (data.NewUserCheck == "True")
        {
            Debug.Log("Creating a new User");
            PlayerPrefs.SetString("Player_Username", data.Username);
            PlayerPrefs.SetString("Player_Password", data.Password);
            PlayerPrefs.SetString("Player_Level", data.Level);
            PlayerPrefs.SetString("Player_Score", data.Score);
            PlayerPrefs.SetString("Player_Health", data.Health);

            PlayerPrefs.SetString("Player_Class", data.playerClass);
            SceneManager.LoadScene("Character Select");
        }
        else if (data.Username == "Bad Password, try again")
        {
            Debug.Log("BAd Password Try again");
        }
        else
        {
            PlayerPrefs.SetString("Player_Username", data.Username);
            PlayerPrefs.SetString("Player_Password", data.Password);
            PlayerPrefs.SetString("Player_Level", data.Level);
            PlayerPrefs.SetString("Player_Score", data.Score);
            PlayerPrefs.SetString("Player_Health", data.Health);

            PlayerPrefs.SetString("Player_Class", data.playerClass);

            Debug.Log(data);
            SceneManager.LoadScene("Game");
        }
    }

}
