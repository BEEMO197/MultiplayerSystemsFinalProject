using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputText : MonoBehaviour
{
    public InputField input;
    public Text WarningText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (string.IsNullOrEmpty(input.text))
        {
            WarningText.text = "Input is empty";
            WarningText.gameObject.SetActive(true);
        }
        else
            WarningText.gameObject.SetActive(false);
    }
}
