using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGameButton : MonoBehaviour
{
    public void OnButtonPress()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }
}
