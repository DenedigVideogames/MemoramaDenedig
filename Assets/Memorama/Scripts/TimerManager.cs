using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public enum GameMode { CountDown, CountUp }
    public GameMode currentMode;
    public static TimerManager Instance { get; private set; }

    public static bool _isPaused;
    public TMP_Text timetext;
    public TMP_Text FinalTimeText;
    public TMP_Text Errors;
    public TMP_Text textScore;
    public TMP_Text additionalTextScore;

    public float time;
    public GameObject canvasScoreBoard;
    public GameObject gameOverCanvas;

    public float puntos = 0;
    public AudioSource soundtiempo;
    private int i = 0;
    private Collider[] colliders;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        if (Settings.Instance.GetGameMode() == 0 || Settings.Instance.GetGameMode() == 3 || Settings.Instance.GetGameMode() == 4)
        {
            currentMode = GameMode.CountUp;
        }
        else if (Settings.Instance.GetGameMode() == 1 || Settings.Instance.GetGameMode() == 2)
        {
            currentMode = GameMode.CountUp;
        }
    }
    void Start()
    {
        soundtiempo.Play();
        _isPaused = false;

        if (currentMode == GameMode.CountDown)
        {
            gameOverCanvas.SetActive(false);
        }
        else
        {
            time = 0;
        }
    }

    void Update()
    {
        if (_isPaused) return;

        // Verifica si es el modo de cuenta regresiva
        if (currentMode == GameMode.CountDown)
        {
            HandleCountDownTimer();
        }
        else
        {
            HandleCountUpTimer();
        }
    }

    void HandleCountDownTimer()
    {
        if (Comparisons.aciertos < 28 && !Comparisons.timepause && !Comparisons.setcanvas)
        {
            time -= Time.deltaTime;
            DisplayTime(time);
            puntos -= 2 * Time.deltaTime;

            if (puntos <= 0) puntos = 0;
        }

        if (time <= 0)
        {
            time = 0;
            GameOver();
            GamePaused();
            soundtiempo.Stop();
        }

        if (Comparisons.aciertos >= 28)
        {
            DisplayFinalStats();
        }

        if (Comparisons.timepause || Comparisons.setcanvas || Comparisons.setcanvascore)
        {
            soundtiempo.Stop();
        }
    }

    void HandleCountUpTimer()
    {
        if (Comparisons.aciertos < 28 && !Comparisons.timepause && !Comparisons.setcanvas)
        {
            time += Time.deltaTime;
            DisplayTime(time);
            puntos -= 3 * Time.deltaTime;

            if (puntos <= 0) puntos = 0;
        }

        if (Comparisons.aciertos >= 28)
        {
            DisplayFinalStats();
        }

        if (Comparisons.timepause || Comparisons.setcanvas || Comparisons.setcanvascore)
        {
            soundtiempo.Stop();
        }
    }

    void DisplayFinalStats()
    {
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

    void GameOver()
    {
        gameOverCanvas.SetActive(true);
        DeactivateAllColliders();
    }

    void DeactivateAllColliders()
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
