using System;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    public static TextManager Instance { get; private set; }

    public TextAsset idiomaData;
    private Dictionary<int, string[]> dataDictionary;

    public delegate void OnLanguageChanged();
    public event OnLanguageChanged LanguageChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
            LoadData(); 
        }
        else if (Instance != this)
        {
            Destroy(gameObject); 
        }
    }

    private void LoadData()
    {
        if (idiomaData != null)
        {
            dataDictionary = new Dictionary<int, string[]>();
            string[] dataLines = idiomaData.text.Split(new string[] { "\n" }, StringSplitOptions.None);

            foreach (string line in dataLines)
            {
                string[] entries = line.Split(new string[] { "," }, StringSplitOptions.None);
                if (entries.Length > 1)
                {
                    int id = int.Parse(entries[0]);
                    string[] translations = new string[entries.Length - 1];
                    Array.Copy(entries, 1, translations, 0, translations.Length);
                    dataDictionary.Add(id, translations);
                }
            }
        }
        else
        {
            Debug.LogError("idiomaData no está asignado en el TextManager.");
        }
    }

    public string setText(int id)
    {
        if (dataDictionary != null && dataDictionary.ContainsKey(id))
        {
            int languageIndex = PlayerPrefs.GetInt("Idioma", 0); // 0 es el idioma predeterminado
            if (languageIndex < dataDictionary[id].Length)
            {
                return dataDictionary[id][languageIndex];
            }
            else
            {
                Debug.LogError("Índice de idioma fuera de rango.");
                return "Idioma no disponible";
            }
        }
        else
        {
            Debug.LogError("ID no encontrado en los datos.");
            return "Texto no disponible";
        }
    }

    public void UpdateText()
    {
        LanguageChanged?.Invoke();
    }

    public void SetLanguage(int language)
    {
        PlayerPrefs.SetInt("Idioma", language);
        UpdateText();
    }
}
