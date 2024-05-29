using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class SceneChanger : MonoBehaviour
{
    public TextMeshProUGUI playername;
    public GameObject falseresolver;
    public GameObject roundcanva;
    private int i=0;

    public void Awake()
    {
        roundcanva.SetActive(false);
        falseresolver.SetActive(false);
        playername.text = InputText.inputText.Player;
    }

    void Update()
    {
        if (Comparisons.setcanvas == true)
        {
            if(i == 0)
            {
                Settings.Instance.PlaySfx("Ronda");
                i++;
            }
            roundcanva.SetActive(true);
        }

        if (Comparisons.setcanvas == false)
        {
            roundcanva.SetActive(false);
        }
    }

    public void SceneLoader(int SceneIndex)
    {
        SceneManager.LoadScene(SceneIndex);
    }

    public void falsear()
    {
        Comparisons.setcanvas = false;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void OnPause()
    {
        falseresolver.SetActive(true);
    }

    public void UnPause()
    {
        falseresolver.SetActive(false);
        DontDestroyField.isonmyFirstScene = true;
    }

    public void ResetSound()
    {
        i = 0;
    }

    public void RechargeScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

}
