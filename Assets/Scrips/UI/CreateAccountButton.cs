using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CreateAccountButton : MonoBehaviour
{
    
    public void OnButtonPress()
    {
        SceneManager.LoadScene("Create Account");
    }
}
