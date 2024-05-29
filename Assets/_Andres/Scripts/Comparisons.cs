using System.Collections;
using TMPro;
using UnityEngine;


public class Comparisons : MonoBehaviour
{
    //si algun dia llega a venir otro dev a este codigo, quiero pedir perdon por este codigo tan culero, cuando llegue, ya estaba asi, yo solo implemente lo de timer plus, 
    // la racha, el timer normal y el memorama plus, el equipo que se encargo de hacer la mecanica principal del juego hizo todo lo demas... suerte entendiendo este codigo y los demas (esta muy cabron)

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

    public TimeCount timeCount;
    public Timer timerCount;

    private int rachaCount = 0;
    public Canvas rachaCanvas;
    public Canvas addTimeCanvas;

    private Collider[] colliders;

    public bool timerPlus;

    public Spawner spawner;
    public GameObject gameOverCanvas;

    //Transforms
    #region
    public Transform pila1;
    public Transform pila2;
    public Transform pila3;
    public Transform pila4;
    public Transform pila5;
    public Transform pila6;
    public Transform pila7;
    public Transform pila8;
    public Transform pila9;
    public Transform pila10;
    public Transform pila11;
    public Transform pila12;
    #endregion

    void Start()
    {
        timepause = false;
        canvascore.SetActive(false);
        rule.enabled = false;
        ruleta.SetActive(false);
        Cuentalugar = 1;
        aciertos = 0;
        errores = 0;
    }

    void Update()
    {
        if (spawner.MemoramaPlus == true)
        {
            CheckText.text = aciertos.ToString() + " / 28";
        }

        if (CartasVolteadas == 2)
        {
            if (firstID == secondID)
            {
                Cursor.lockState = CursorLockMode.Locked;
                aciertos = aciertos + 1;
                CheckText.text = aciertos.ToString() + " / 28";
                CartasVolteadas = 0;
                Checktimerend = true;
                rachaCount++;
                UpdateScore();

                if(timerPlus == true)
                {
                    timerCount.AddTime(3);
                    addTimeCanvas.gameObject.SetActive(true);
                    Invoke("DesactivarTimeCanvas", 3.7f);
                }

                if (Checktimerend == true)
                {
                    Settings.Instance.PlaySfx("Completado");
                    StartCoroutine(Makebig());   
                }
            }

            if (firstID != secondID)
            {
                errores = errores + 1;
                WrongText.text = errores.ToString();
                CartasVolteadas = 0;
                Errortimerend = true;
                rachaCount = 0;
                print("racha perdida");

                if (spawner.MemoramaPlus == true && errores == 1) // Verifica si MemoramaPlus está activo y si es el primer error
                {
                    GameOver();
                    print("Que bruto pongale 0");
                }

                if (Errortimerend == true)
                {
                    Settings.Instance.PlaySfx("Error");
                    StartCoroutine(Corspaw());
                }
            }
        }
    }

    private void UpdateScore()
    {
        if (rachaCount >= 2)
        {
            print("racha ganada");
            TimeCount timeCount = FindObjectOfType<TimeCount>();
            if (timeCount != null)
            {
                timeCount.AddPoints(rachaCount); 
                rachaCanvas.gameObject.SetActive(true);
                print("+50");
                Invoke("DesactivarRachaCanvas", 3.7f);
            }
            Timer timerCount = FindObjectOfType<Timer>();
            if (timerCount != null)
            {
                timerCount.AddPoints(rachaCount);
                rachaCanvas.gameObject.SetActive(true);
                print("+50");
                Invoke("DesactivarRachaCanvas", 3.7f);
            }
            else
            {
                print("racha perdida");
            }
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

        cartavol1.GetComponent<BoxCollider>().enabled = true;
        cartavol2.GetComponent<BoxCollider>().enabled = true;

        Errortimerend = false;
        StopAllCoroutines();
        Settings.Instance.PlaySfx("VolteoCartaError");
    }

    private void WaitTime()
    {
        cartavol1.GetComponent<Animator>().SetTrigger("Close");
        cartavol2.GetComponent<Animator>().SetTrigger("Close");
    }

    private IEnumerator Truespaw()
    {
        yield return new WaitForSeconds(.8f);
        rule.enabled = false;
        ruleta.SetActive(false);

        CorrectParPos();

        if (spawner.MemoramaPlus == true)
        {
            if (aciertos == 4) //10
            {
                setcanvas = true;
                Cuentalugar = 0;
            }
        
            if (aciertos == 10) //12 
            {
                setcanvas = true;
                Cuentalugar = 0;
            }
        
            if (aciertos == 18) //14
            {
                setcanvas = true;
                Cuentalugar = 0;
            }
        
            if (aciertos == 28) //16
            {
                setcanvas = true;
                Cuentalugar = 0;
            }

            if (aciertos == 37) //18
            {
                canvascore.SetActive(true);
                setcanvascore = true;
            }
        }

        if (spawner.MemoramaPlus==false)
        {
            if (aciertos == 4) //8 cartas
            {
                setcanvas = true;
                Cuentalugar = 0;
            }

            if (aciertos == 10) //+12 cartas
            {
                setcanvas = true;
                Cuentalugar = 0;
            }

            if (aciertos == 18) //+16 cartas
            {
                setcanvas = true;
                Cuentalugar = 0;
            }

            if (aciertos == 28) //+20 cartas
            {
                canvascore.SetActive(true);
                setcanvascore = true;
            }
        }

        Checktimerend = false;
        timepause = false;
        Cuentalugar = Cuentalugar + 1;
        timeCount.sontiempo.Play();
        timerCount.sontiempo.Play();
        Cursor.lockState = CursorLockMode.None;
        StopAllCoroutines();
    }

    private IEnumerator Makebig()
    {
        timepause = true;
        DeactivateAllColliders();

        cartavol1.GetComponent<Animator>().enabled = false;
        cartavol2.GetComponent<Animator>().enabled = false;

        cartavol1.transform.position = tocenterleft.transform.position;
        cartavol2.transform.position = tocenterright.transform.position;

        cartavol1.transform.localScale = centresize;
        cartavol2.transform.localScale = centresize;

        cartavol1.transform.rotation = tocenterleft.transform.rotation;
        cartavol2.transform.rotation = tocenterright.transform.rotation;

        ruleta.SetActive(true);
        rule.enabled = true;

        yield return new WaitForSeconds(makebigtime);

        Invoke("ActivateAllColliders", .5f);

        StopCoroutine(Makebig());
        StartCoroutine(Truespaw());
    }

    void CorrectParPos()//sonidos en los if cuando aciertos 
    {
        if (Cuentalugar == 1)
        {
            cartavol1.transform.position = pila1.transform.position;
            cartavol1.transform.localScale = size;
            cartavol1.GetComponent<Collider>().enabled = false;

            cartavol2.transform.position = pila1.transform.position + new Vector3(.12f,0,.1f);
            cartavol2.transform.localScale = size;
            cartavol2.GetComponent<Collider>().enabled = false;
        }

        if (Cuentalugar == 2)
        {
            cartavol1.transform.position = pila2.transform.position;
            cartavol1.transform.localScale = size;
            cartavol1.GetComponent<Collider>().enabled = false;

            cartavol2.transform.position = pila2.transform.position + new Vector3(.12f, 0, .1f);
            cartavol2.transform.localScale = size;
            cartavol2.GetComponent<Collider>().enabled = false;
        }

        if (Cuentalugar == 3)
        {
            cartavol1.transform.position = pila3.transform.position;
            cartavol1.transform.localScale = size;
            cartavol1.GetComponent<Collider>().enabled = false;

            cartavol2.transform.position = pila3.transform.position + new Vector3(.12f, 0, .1f);
            cartavol2.transform.localScale = size;
            cartavol2.GetComponent<Collider>().enabled = false;
        }

        if (Cuentalugar == 4)
        {
            cartavol1.transform.position = pila4.transform.position;
            cartavol1.transform.localScale = size;
            cartavol1.GetComponent<Collider>().enabled = false;

            cartavol2.transform.position = pila4.transform.position + new Vector3(.12f, 0, .1f);
            cartavol2.transform.localScale = size;
            cartavol2.GetComponent<Collider>().enabled = false;
        }

        if (Cuentalugar == 5)
        {
            cartavol1.transform.position = pila5.transform.position;
            cartavol1.transform.localScale = size;
            cartavol1.GetComponent<Collider>().enabled = false;

            cartavol2.transform.position = pila5.transform.position + new Vector3(.12f, 0, .1f);
            cartavol2.transform.localScale = size;
            cartavol2.GetComponent<Collider>().enabled = false;
        }

        if (Cuentalugar == 6)
        {
            cartavol1.transform.position = pila6.transform.position;
            cartavol1.transform.localScale = size;
            cartavol1.GetComponent<Collider>().enabled = false;

            cartavol2.transform.position = pila6.transform.position + new Vector3(.12f, 0, .1f);
            cartavol2.transform.localScale = size;
            cartavol2.GetComponent<Collider>().enabled = false;
        }

        if (Cuentalugar == 7)
        {
            cartavol1.transform.position = pila7.transform.position;
            cartavol1.transform.localScale = size;
            cartavol1.GetComponent<Collider>().enabled = false;

            cartavol2.transform.position = pila7.transform.position + new Vector3(.12f, 0, .1f);
            cartavol2.transform.localScale = size;
            cartavol2.GetComponent<Collider>().enabled = false;
        }

        if (Cuentalugar == 8)
        {
            cartavol1.transform.position = pila8.transform.position;
            cartavol1.transform.localScale = size;
            cartavol1.GetComponent<Collider>().enabled = false;

            cartavol2.transform.position = pila8.transform.position + new Vector3(.12f, 0, .1f);
            cartavol2.transform.localScale = size;
            cartavol2.GetComponent<Collider>().enabled = false;
        }

        if (Cuentalugar == 9)
        {
            cartavol1.transform.position = pila9.transform.position;
            cartavol1.transform.localScale = size;
            cartavol1.GetComponent<Collider>().enabled = false;

            cartavol2.transform.position = pila9.transform.position + new Vector3(.12f, 0, .1f);
            cartavol2.transform.localScale = size;
            cartavol2.GetComponent<Collider>().enabled = false;
        }

        if (Cuentalugar == 10)
        {
            cartavol1.transform.position = pila10.transform.position;
            cartavol1.transform.localScale = size;
            cartavol1.GetComponent<Collider>().enabled = false;

            cartavol2.transform.position = pila10.transform.position + new Vector3(.12f, 0, .1f);
            cartavol2.transform.localScale = size;
            cartavol2.GetComponent<Collider>().enabled = false;
        }

        if (Cuentalugar == 11)
        {
            cartavol1.transform.position = pila11.transform.position;
            cartavol1.transform.localScale = size;
            cartavol1.GetComponent<Collider>().enabled = false;

            cartavol2.transform.position = pila11.transform.position + new Vector3(.12f, 0, .1f);
            cartavol2.transform.localScale = size;
            cartavol2.GetComponent<Collider>().enabled = false;
        }

        if (Cuentalugar == 12)
        {
            cartavol1.transform.position = pila12.transform.position;
            cartavol1.transform.localScale = size;
            cartavol1.GetComponent<Collider>().enabled = false;

            cartavol2.transform.position = pila12.transform.position + new Vector3(.12f, 0, .1f);
            cartavol2.transform.localScale = size;
            cartavol2.GetComponent<Collider>().enabled = false;
        }
    }
}
