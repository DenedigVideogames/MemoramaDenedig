using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class aparecerdesaparecerboton : MonoBehaviour
{
    public GameObject canvaspunt;
    public GameObject botoncont;

    public TextMeshProUGUI textopar;

    public float tiempoEspera = 0.5f;

   
    //private bool botonActivo = false;


    void Start()
    {
        canvaspunt.SetActive(false);
        botoncont.SetActive(false);
    }

    // Update is called once per frame

    
    void Update()
    {
       
        string texto = textopar.text;

        
        if (texto.Contains("4 / 20")) 
        {
            canvaspunt.SetActive(true);
            botoncont.SetActive(true);
        }
        
        if (texto.Contains("10 / 20"))
        {
            canvaspunt.SetActive(true);
            botoncont.SetActive(true);
        }
        
        if (texto.Contains("4 / 20")) 
        {
            canvaspunt.SetActive(true);
            botoncont.SetActive(true);
        }
        
        if (texto.Contains("10 / 20"))
        {
            canvaspunt.SetActive(true);
            botoncont.SetActive(true);
        }
    }

    public void falsear()
    {
        canvaspunt.SetActive(false);
        botoncont.SetActive(false);
    }





    /* private void Update()
     {
         logicaOleadas();
     }*/

    /*private void logicaOleadas()
    {
        if (Cuentalugar == 5 )
        {
            boton.SetActive(true);
            botonActivo = true;
            Debug.Log("Estoy prendiendo el botón");
            if (boton == true)
            {
                boton.SetActive(false);
            }
        }

        if (Cuentalugar == 9)
        {
            Debug.Log("Estoy prendiendo el botón 2");
        }

    }

    public void DesactivarBoton()
    {
        boton.SetActive(false);
        botonActivo = false;
        Debug.Log("Estoy apagando el botón");
    

        if (Cuentalugar == 12)
        {
            boton.gameObject.SetActive(true);
        }
        if (Cuentalugar == 16)
        {
            boton.gameObject.SetActive(true);
        }
        if (Cuentalugar == 20)
        {
            boton.gameObject.SetActive(true);
        }

    }
    
     else if (texto.Contains("5 / 20"))
        {
            boton.gameObject.SetActive(false);
        }
    else if (texto.Contains("11 / 20"))
        {
            boton.gameObject.SetActive(false);
        }
    else if (texto.Contains("5 / 20"))
        {
            boton.gameObject.SetActive(false);
        }
     else if (texto.Contains("11 / 20"))
        {
            boton.gameObject.SetActive(false);
        }
        
     
     
     
     */

}
