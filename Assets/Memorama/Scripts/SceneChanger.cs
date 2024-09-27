using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;

public class SceneChanger : MonoBehaviour
{
    public TextMeshProUGUI playername;
    public GameObject falseresolver;
    public GameObject roundcanva;
    public string player1;
    public string player2;
    private int i = 0;
    public static SceneChanger Instance;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        roundcanva.SetActive(false);
        falseresolver.SetActive(false);
        playername.text = Settings.Instance.Player1;
        Debug.Log("playerName" + Settings.Instance.Player1);
        player1 = Settings.Instance.Player1;
        player2 = Settings.Instance.Player2; 
    }
    public void ActivePlayer(int playerActive)
    {
        if(playerActive == 0)
        {
            playername.text = player1;
        }
        else
        {
            playername.text = player2;
        }
    }

    public void SetCanvas()
    {
        if(Comparisons.setcanvas == true)
        {
            if(i==0)
            {
                Settings.Instance.PlaySfx("Ronda");
            }
            roundcanva.SetActive(true);
        }
        if(Comparisons.setcanvas == false)
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
