using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class QuitGameButton : MonoBehaviour
{
    public Character connectedPlayer;

    public void OnButtonPress()
    {
        StartCoroutine(SaveUserInfo());
    }

    IEnumerator SaveUserInfo()
    {
        string siteUrl = "https://w5zfcqda33.execute-api.us-east-1.amazonaws.com/default/SavePlayerData";
        
        siteUrl += "?Username=" + connectedPlayer.playerRef.Username + "&Level=" + connectedPlayer.level + "&Score=" + connectedPlayer.score + "&Health=" + connectedPlayer.health + "&PlayerClass=" + (int)connectedPlayer.currentClass;

        UnityWebRequest www = UnityWebRequest.Get(siteUrl);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
        }
    }
}
