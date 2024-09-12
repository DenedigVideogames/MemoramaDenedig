using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeCount : MonoBehaviour
{
    public static bool _isPaused;
    public TMP_Text timetext;
    public TMP_Text FinalTimeText;
    public TMP_Text Errors;
    public TMP_Text textScore;
    public TMP_Text additionalTextScore;

    public float time;
    public GameObject canvasScoreBoard;

    public float puntos = 0;

    public AudioSource soundtiempo;
    private int i = 0;


    public static void GamePaused()
    {
        _isPaused = !_isPaused;
    }

    public static void GameUnPaused()
    {
        _isPaused = false;
    }

    public void pausasonidotiempo()
    {
        soundtiempo.Stop();
    }

    public void playsonidotiempo()
    {
        soundtiempo.Play();
    }

    void Start()
    {
        soundtiempo.Play();
        _isPaused = false;
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isPaused) return;
        if (Comparisons.aciertos < 28 && !Comparisons.timepause && !Comparisons.setcanvas)
        {
            time += Time.deltaTime;
            DisplayTime(time);
            puntos -= 3 * Time.deltaTime;

            if (puntos <= 0)
            {
                puntos = 0;
            }
        }

        if (Comparisons.aciertos >= 28)
        {
            time += 0;
            DisplayTime(time);
            soundtiempo.Stop();
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
            soundtiempo.Stop();
        }

        additionalTextScore.text = puntos.ToString("0");
        textScore.text = puntos.ToString("0");
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
}
