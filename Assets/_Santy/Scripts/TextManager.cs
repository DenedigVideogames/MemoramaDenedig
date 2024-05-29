using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextManager : MonoBehaviour
{
    public static TextManager Instance = null;
    public TextAsset idiomaData;
    private string[] data;
    private int num2;
    
    public void SetLanguage(int num)
    {
        PlayerPrefs.SetInt("Idioma",num);
        num2 = num;
    }

    void Update()
    {
        setText(num2);   
    }

    private string result;
    public string setText(int id)
    {
        data = idiomaData.text.Split(new string[] { ",", "\n" }, System.StringSplitOptions.None);
        for (int i = 0; i < data.Length; i++)
        {
            if (id.ToString() == data[i])
            {   
                result = data[i + PlayerPrefs.GetInt("Idioma")+1];
            }
        }
        return result;
    }


}
