using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static bool _isPaused;
    public TMP_Text timetext;
    public TMP_Text FinalTimeText;
    public TMP_Text Errors;
    public TMP_Text textScore;

    public float time;
    public GameObject canvasScoreBoard;
    public GameObject gameOverCanvas;

    public float puntos = 0;

    public AudioSource sontiempo;
    private int i = 0;

    private Collider[] colliders;


    public static void GamePaused()//Sonido pausa 
    {
        _isPaused = !_isPaused;
    }

    public static void GameUnPaused()
    {
        _isPaused = false;
    }

    public void pausasonidotiempo()
    {
        sontiempo.Stop();
    }

    public void playsonidotiempo()
    {
        sontiempo.Play();
    }

    void Start()
    {
        sontiempo.Play();
        _isPaused = false;
        gameOverCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_isPaused) return;
        if (Comparisons.aciertos < 28 && !Comparisons.timepause && !Comparisons.setcanvas)
        {
            time -= Time.deltaTime;
            DisplayTime(time);
            puntos -= 2 * Time.deltaTime;

            if (puntos <= 0)
            {
                puntos = 0;
            }
        }

        if (time <= 0)
        {
            time = 0; 
            GameOver();
            GamePaused();
            pausasonidotiempo();
        }

        if (Comparisons.aciertos >= 28)
        {
            time += 0;
            DisplayTime(time);
            sontiempo.Stop();
            FinalTimeText.text = time.ToString("0") + " segundos";
            Errors.text = Comparisons.errores.ToString();
            textScore.text = puntos.ToString("0"); 
            canvasScoreBoard.SetActive(true);

            if (i == 0)
            {
                Settings.Instance.PlaySfx("Final");
                i++;
            }
        }

        if (Comparisons.timepause || Comparisons.setcanvas || Comparisons.setcanvascore)
        {
            time += 0;
            DisplayTime(time);
            sontiempo.Stop();
        }
    }

    void GameOver()
    {
        gameOverCanvas.SetActive(true); 
        DeactivateAllColliders(); 
    }

    public void DeactivateAllColliders()
    {
        colliders = FindObjectsOfType<Collider>();

        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }
    }

    void DisplayTime(float TimetoDisplay)
    {
        if (TimetoDisplay < 0)
        {
            TimetoDisplay = 0;
        }
        else if (TimetoDisplay > 0)
        {
            TimetoDisplay += 1;
        }

        float minutes = Mathf.FloorToInt(TimetoDisplay / 60);
        float seconds = Mathf.FloorToInt(TimetoDisplay % 60);

        timetext.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void ResetFinal()
    {
        i = 0;
    }

    public void AddPoints(int rachaCount)
    {
        float puntosAIncrementar = rachaCount + 20;
        puntos += puntosAIncrementar;
    }

    public void AddTime(float secondsToAdd)
    {
        time += secondsToAdd;
        DisplayTime(time);
    }
}
