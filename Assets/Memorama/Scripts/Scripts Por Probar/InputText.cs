using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputText : MonoBehaviour
{
    public static InputText inputText;
    public TMP_InputField inputField;

    public string Player;

    void Awake()
    {
        if(inputText == null)
        {
            inputText = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void setname()
    {
        Player = inputField.text;
    }
}
