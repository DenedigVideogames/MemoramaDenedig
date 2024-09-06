using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class finalScore : MonoBehaviour
{
    public TMP_Text tiempo1;
    public TMP_Text textScore;




    void Start()
    {
        // Obtener el tiempo guardado en PlayerPrefs

        float tiempoGuardado = PlayerPrefs.GetFloat("TiempoGuardado", 0f);


        // Mostrar el tiempo en el Text
        tiempo1.text = "Tiempo: " + tiempoGuardado.ToString("F2");




    }
    void Update()
    {

        puntuacion();
    }

    void puntuacion()
    {
        Debug.Log("Empece puntuacion");
        int puntuacion = PlayerPrefs.GetInt("PuntajeGuardado");

        textScore.text = "Felicidades obtuviste " + puntuacion;


    }
    void errores()
    {
        
    }
}



