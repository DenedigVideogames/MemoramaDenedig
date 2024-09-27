using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckName : MonoBehaviour
{
    [SerializeField] private GameObject textalert;
    [SerializeField] private GameObject textalert2;
    [SerializeField] private GameObject panelMenu;
    [SerializeField] private GameObject panelSeleccion;
    public void CheckPlayer1()
    {
        if(Settings.Instance.Player1 == "")
        {
            textalert.SetActive(true);
        }
        else
        {
            panelSeleccion.SetActive(true);
            panelMenu.SetActive(false);
        }
    }
    public void CheckPlayer2()
    {
        if (Settings.Instance.Player2 == "")
        {
            textalert2.SetActive(true);
        }
        else
        {
            Settings.Instance.ChangeScene(1);
        }
    }
}
