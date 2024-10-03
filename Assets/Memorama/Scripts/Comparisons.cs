using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Comparisons : MonoBehaviour
{
    public static Comparisons Instance { get; private set; }

    public TextMeshProUGUI CheckText;
    public TextMeshProUGUI WrongText;

    // Variables para el jugador 1 y jugador 2
    public static int currentPlayer = 1;

    public static int aciertosPlayer1 = 0;
    public static int erroresPlayer1 = 0;
    public static int aciertosPlayer2 = 0;
    public static int erroresPlayer2 = 0;

    public static int CartasVolteadas = 0;
    public static int firstID;
    public static int secondID;

    public static bool Errortimerend = false;
    public static bool Checktimerend = false;
    public static bool timepause = false;
    public static bool setcanvas = false;
    public static bool setcanvascore = false;

    public static GameObject cartavol1;
    public static GameObject cartavol2;

    public static int Cuentalugar = 20;
    public float makebigtime;

    public Vector3 size = new Vector3(0, 0, 0);
    public Vector3 centresize = new Vector3(0, 0, 0);

    public Transform tocenterleft;
    public Transform tocenterright;

    public GameObject ruleta;
    public Animator rule;

    public GameObject canvascore;

    public TimerManager timeManager;
    private static int rachaCount = 0;
    public Canvas rachaCanvas;
    public Canvas addTimeCanvas;

    private Collider[] colliders;

    public bool timerPlus;

    public Spawner spawner;
    public GameObject gameOverCanvas;

    public Transform[] pilas;


    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        timepause = false;
        canvascore.SetActive(false);
        rule.enabled = false;
        ruleta.SetActive(false);
        Cuentalugar = 1;
        aciertosPlayer1 = 0;
        erroresPlayer1 = 0;
        aciertosPlayer2 = 0;
        erroresPlayer2 = 0;
        if (Settings.Instance.GetGameMode() == 2)
        {
            timerPlus = true;
        }
    }



    public void CheckPar()
    {
        if (CartasVolteadas == 2)
        {
            if (firstID == secondID)
            {
                HandleMatch();
            }
            else
            {
                HandleMismatch();
            }
        }
    }

    private void HandleMatch()
    {
        if (currentPlayer == 1)
        {
            aciertosPlayer1++;
        }
        else
        {
            aciertosPlayer2++;
        }

        rachaCount++;
        UpdateScore();
        Debug.Log("Racha Count: " + rachaCount);

        if (timerPlus)
        {
            timeManager.AddTime(3);
            addTimeCanvas.gameObject.SetActive(true);
            Invoke("DesactivarTimeCanvas", 2.5f);
        }

        Settings.Instance.PlaySfx("Completado");
        StartCoroutine(Makebig());
        CheckTextAciertos();
        CartasVolteadas = 0;
    }

    private void HandleMismatch()
    {
        if (currentPlayer == 1)
        {
            erroresPlayer1++;
            WrongText.text = erroresPlayer1.ToString();
        }
        else
        {
            erroresPlayer2++;
            WrongText.text = erroresPlayer2.ToString();
        }
        rachaCount = 0;

        if (spawner.modoSinFallos && erroresPlayer1 == 1)
        {
            GameOver();
        }
        else if (Settings.Instance.GetGameMode() == 4)
        {
            ChangeTurn();
        }

        Settings.Instance.PlaySfx("Error");
        StartCoroutine(Corspaw());

        CartasVolteadas = 0;

        Debug.Log("Errores Player 1: " + erroresPlayer1 + ", Errores Player 2: " + erroresPlayer2);

    }

    private void CheckTextAciertos()
    {
        CheckText.text = aciertosPlayer1 + " / " + (spawner.modoSinFallos ? "28" : "28");
        Debug.Log("se checa los aciertos");
        
    }

    private void UpdateScore()
    {
        if (rachaCount >= 2)
        {
            timeManager?.AddPoints(rachaCount);
            rachaCanvas.gameObject.SetActive(true);
            Invoke("DesactivarRachaCanvas", 3.7f);
        }
        Debug.Log(" se actualizo el puntaje");
    }

    private void ChangeTurn()
    {
        currentPlayer = currentPlayer == 1 ? 2 : 1;
        timeManager.ChangeTurn();
    }
    void GameOver()
    {
        timepause = true;
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
        
        Debug.Log("se desactivaron los colliders");
    }

    public void ActivateAllColliders()
    {
        foreach (Collider collider in colliders)
        {
            collider.enabled = true;
        }
        Debug.Log("se activaron los colliders");
    }

    private void DesactivarRachaCanvas()
    {
        rachaCanvas.gameObject.SetActive(false);
    }

    private void DesactivarTimeCanvas()
    {
        addTimeCanvas.gameObject.SetActive(false);
    }

    private IEnumerator Corspaw()
    {
        yield return new WaitForSeconds(.1f);
        Invoke("WaitTime", .5f);
        EnableColliders();

        Errortimerend = false;
        StopAllCoroutines();
        Settings.Instance.PlaySfx("VolteoCartaError");
    }

    private void EnableColliders()
    {
        cartavol1.GetComponent<BoxCollider>().enabled = true;
        cartavol2.GetComponent<BoxCollider>().enabled = true;
        Debug.Log("Colliders habilitados para cartavol1 y cartavol2");

    }

    private void WaitTime()
    {
        cartavol1.GetComponent<Animator>().SetTrigger("Close");
        cartavol2.GetComponent<Animator>().SetTrigger("Close");
        Debug.Log("Activando animación 'Close' para las cartas");

    }

    private IEnumerator Makebig()
    {
        timepause = true;
        DeactivateAllColliders();

        MoveCardsToCenter();

        ruleta.SetActive(true);
        rule.enabled = true;

        yield return new WaitForSeconds(makebigtime);

        Invoke("ActivateAllColliders", .5f);
        StartCoroutine(Truespaw());
    }

    private void MoveCardsToCenter()
    {
        SetCardTransform(cartavol1, tocenterleft);
        SetCardTransform(cartavol2, tocenterright);
    }

    private void SetCardTransform(GameObject card, Transform target)
    {
        card.GetComponent<Animator>().enabled = false;
        card.transform.position = target.position;
        card.transform.localScale = centresize;
        card.transform.rotation = target.rotation;
    }

    private IEnumerator Truespaw()
    {
        yield return new WaitForSeconds(.5f);
        rule.enabled = false;
        ruleta.SetActive(false);

        CorrectParPos();
       
        CheckNormalConditions();

        ResetAfterSpawn();
    }

    private void CheckNormalConditions()
    {
        if (aciertosPlayer1 + aciertosPlayer2 == 4 || aciertosPlayer1 + aciertosPlayer2 == 10 || aciertosPlayer1 + aciertosPlayer2 == 18 || aciertosPlayer1 + aciertosPlayer2 == 28)
        {
            setcanvas = true;
            SceneChanger.Instance.SetCanvas();
            timepause = true;
            Cuentalugar = 0;
            Debug.Log("Time pause : " + timepause );
        }

        if (aciertosPlayer1 + aciertosPlayer2 == 28)
        {
            canvascore.SetActive(true);
            setcanvascore = true;
        }
    }

    private void ResetAfterSpawn()
    {
        Checktimerend = false;
        timepause = false;
        Cuentalugar++;
        timeManager.soundtiempo.Play();
        Cursor.lockState = CursorLockMode.None;
        StopAllCoroutines();
        Debug.Log(" ResetAfterSpawn");
        Debug.Log("Time pause : " + timepause );
    }

    void CorrectParPos()
    {
        if (Cuentalugar <= pilas.Length)
        {
            SetCardTransform(cartavol1, pilas[Cuentalugar - 1], new Vector3(0, 0, 0), Quaternion.Euler(-80, 90, 90));
            SetCardTransform(cartavol2, pilas[Cuentalugar - 1], new Vector3(.12f, 0, .1f), Quaternion.Euler(-90, 0, 180));
        }
    }

    private void SetCardTransform(GameObject card, Transform target, Vector3 offset = default, Quaternion rotation = default)
    {
        Vector3 finalPosition = target.position + offset;

        card.transform.position = finalPosition;
        card.transform.localScale = size;
        card.transform.rotation = rotation == default ? Quaternion.identity : rotation;

        card.GetComponent<Collider>().enabled = false;
    }
}
