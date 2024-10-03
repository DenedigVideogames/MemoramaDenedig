using System.Collections;
using TMPro;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public enum GameMode { CountDown, CountUp, TwoPlayer }
    public GameMode currentMode;
    public static TimerManager Instance { get; private set; }

    public static bool _isPaused;
    public TMP_Text timetext;
    public TMP_Text FinalTimeText;
    public TMP_Text Errors;
    public TMP_Text textScore;
    public TMP_Text additionalTextScore;

    public float timePlayer1;
    public float timePlayer2;
    public bool isPlayer1Turn = true;
    public GameObject canvasScoreBoard;
    public GameObject gameOverCanvas;

    public float puntos = 0;
    public AudioSource soundtiempo;
    private int i = 0;
    private Collider[] colliders;

    public bool changePlayerTurn;

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
        if (Settings.Instance.GetGameMode() == 0 || Settings.Instance.GetGameMode() == 3)
        {
            currentMode = GameMode.CountUp;
        }
        else if (Settings.Instance.GetGameMode() == 1 || Settings.Instance.GetGameMode() == 2)
        {
            currentMode = GameMode.CountDown;
        }
        else if (Settings.Instance.GetGameMode() == 4)
        {
            currentMode = GameMode.TwoPlayer;
            timePlayer1 = 0;
            timePlayer2 = 0;
        }
        if (Settings.Instance.GetGameMode() == 1)
        {
            timePlayer1 = 180;
        }
        if(Settings.Instance.GetGameMode() == 2)
        {
            timePlayer1 = 30;
        }
    }
    void Start()
    {
        soundtiempo.Play();
        _isPaused = false;

        if (currentMode == GameMode.CountDown || currentMode == GameMode.TwoPlayer)
        {
            gameOverCanvas.SetActive(false);
        }
        else
        {
            timePlayer1 = 0;
        }
    }

    void Update()
    {
        if (_isPaused) return;
        if (currentMode == GameMode.TwoPlayer)
        {
            HandleTwoPlayerTimer();
        }
        else if (currentMode == GameMode.CountDown)
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
        if (Comparisons.aciertosPlayer1 < 28 && !Comparisons.timepause && !Comparisons.setcanvas)
        {
            timePlayer1 -= Time.deltaTime;
            DisplayTime(timePlayer1);
            puntos -= 2 * Time.deltaTime;

            if (puntos <= 0) puntos = 0;
        }

        if (timePlayer1 <= 0)
        {
            timePlayer1 = 0;
            GameOver();
            GamePaused();
            soundtiempo.Stop();
        }

        if (Comparisons.aciertosPlayer1 >= 28)
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
        if (Comparisons.aciertosPlayer1 < 28 && !Comparisons.timepause && !Comparisons.setcanvas)
        {
            timePlayer1 += Time.deltaTime;
            DisplayTime(timePlayer1);
            puntos -= 3 * Time.deltaTime;

            if (puntos <= 0) puntos = 0;
        }

        if (Comparisons.aciertosPlayer1 >= 28)
        {
            DisplayFinalStats();
        }

        if (Comparisons.timepause || Comparisons.setcanvas || Comparisons.setcanvascore)
        {
            soundtiempo.Stop();
        }
    }

    void HandleTwoPlayerTimer()
    {
        if (isPlayer1Turn && Comparisons.aciertosPlayer1 + Comparisons.aciertosPlayer2 < 28 && !Comparisons.timepause &&
            !Comparisons.setcanvas)
        {
            timePlayer1 += Time.deltaTime;
            DisplayTimeTwoPlayers(timePlayer1, timetext);
        }
        else if (!isPlayer1Turn && Comparisons.aciertosPlayer1 + Comparisons.aciertosPlayer2 < 28 &&
                 !Comparisons.timepause && !Comparisons.setcanvas)
        {
            timePlayer2 += Time.deltaTime;
            DisplayTimeTwoPlayers(timePlayer2, timetext);
        }
        if (Comparisons.timepause || Comparisons.setcanvas || Comparisons.setcanvascore)
        {
            soundtiempo.Stop();
        }
    }

    public void ChangeTurn()
    {
        StartCoroutine(SwitchPlayerTurn());
        HandleTwoPlayerTimer();
        if(isPlayer1Turn)
        {
            SceneChanger.Instance.ActivePlayer(0);
        }
        else
        {
            SceneChanger.Instance.ActivePlayer(1);
        }
    }

    void DisplayTimeTwoPlayers(float TimetoDisplay, TMP_Text textToUpdate)
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

        textToUpdate.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    IEnumerator SwitchPlayerTurn()
    {
        isPlayer1Turn = !isPlayer1Turn; // Cambia de jugador
        _isPaused = true;
        soundtiempo.Stop();
        yield return new WaitForSeconds(1); // Delay de 1 segundo

        _isPaused = false;
        soundtiempo.Play();
        Debug.Log("pausa");
    }
    void DisplayFinalStats()
    {
        soundtiempo.Stop();
        FinalTimeText.text = timePlayer1.ToString("0") + " segundos";
        Errors.text = Comparisons.erroresPlayer1.ToString();
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
        Debug.Log("GameOver");
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
        timePlayer1 += secondsToAdd;
        DisplayTime(timePlayer1);
    }
}
