using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoDestruirObj : MonoBehaviour
{
    Canvas botonCanvas;
    public static NoDestruirObj inst;
    private void Awake()
    {
        if (NoDestruirObj.inst == null)
        {
            NoDestruirObj.inst = this;
            DontDestroyOnLoad(gameObject);
            botonCanvas = GetComponent<Canvas>();
        }
        else
        {
            Destroy(gameObject);
        }
       // NoDestruirObj
        
    }
}
