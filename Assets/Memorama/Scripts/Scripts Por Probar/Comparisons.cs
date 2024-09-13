using System.Collections;
using TMPro;
using UnityEngine;

public class Comparisons : MonoBehaviour
{
    public static Comparisons Instance { get; private set; }

    public TextMeshProUGUI CheckText;
    public TextMeshProUGUI WrongText;

    public static int aciertos = 0;
    public static int errores = 0;
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

    public TimerManager timeManager; // Cambiado de TimeCount a TimerManager

    private static int rachaCount = 0;  // Agregado contador de rachas
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
        aciertos = 0;
        errores = 0;
        if(Settings.Instance.GetGameMode() == 2)
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
        aciertos++;
        rachaCount++;  // Incrementa la racha en cada acierto
        UpdateScore();  // Actualiza el puntaje

        if (timerPlus)
        {
            timeManager.AddTime(3);  // A�ade tiempo
            addTimeCanvas.gameObject.SetActive(true);
            Invoke("DesactivarTimeCanvas", 2.5f);  // Desactiva el canvas despu�s de un tiempo
        }

        Settings.Instance.PlaySfx("Completado");
        StartCoroutine(Makebig());  // Inicia la animaci�n de agrandar
        CheckTextAciertos();  // Actualiza el texto de aciertos
        CartasVolteadas = 0;  // Reinicia el contador de cartas volteadas
    }

    private void HandleMismatch()
    {
        errores++;
        rachaCount = 0;  // Reinicia el contador de rachas en caso de error
        WrongText.text = errores.ToString();

        if (spawner.modoSinFallos && errores == 1)
        {
            GameOver();
        }

        Settings.Instance.PlaySfx("Error");
        StartCoroutine(Corspaw());

        CartasVolteadas = 0;
    }

    private void CheckTextAciertos()
    {
        CheckText.text = aciertos + " / " + (spawner.modoSinFallos ? "28" : "28");
    }

    private void UpdateScore()
    {
        if (rachaCount >= 2)
        {
            timeManager?.AddPoints(rachaCount);  // A�ade puntos basados en la racha
            rachaCanvas.gameObject.SetActive(true);
            Invoke("DesactivarRachaCanvas", 3.7f);  // Desactiva el canvas de racha despu�s de un tiempo
        }
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
    }

    public void ActivateAllColliders()
    {
        foreach (Collider collider in colliders)
        {
            collider.enabled = true;
        }
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
    }

    private void WaitTime()
    {
        cartavol1.GetComponent<Animator>().SetTrigger("Close");
        cartavol2.GetComponent<Animator>().SetTrigger("Close");
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

        if (spawner.modoSinFallos)
        {
            CheckMemoramaModoSinFallosConditions();
        }
        else
        {
            CheckNormalConditions();
        }

        ResetAfterSpawn();
    }

    private void CheckMemoramaModoSinFallosConditions()
    {
        if (aciertos == 4 || aciertos == 10 || aciertos == 18 || aciertos == 28)
        {
            setcanvas = true;
            Cuentalugar = 0;
        }

        if (aciertos == 37)
        {
            canvascore.SetActive(true);
            setcanvascore = true;
        }
    }

    private void CheckNormalConditions()
    {
        if (aciertos == 4 || aciertos == 10 || aciertos == 18 || aciertos == 28)
        {
            setcanvas = true;
            Cuentalugar = 0;
        }

        if (aciertos == 28)
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
