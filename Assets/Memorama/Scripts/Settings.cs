using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class Settings : MonoBehaviour
{
    [Header("Sound")]
    public static Settings Instance;
    public SoundManager[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;
    
    [Header("Volume")] 
    [SerializeField] private AudioMixer audioController;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;

    [Header("Brightness")]  
    public Slider brightneSlider;
    private float _sliderValue;
    public Image brightnessPanel;

    [Header("Fade")]
    public Image fadeimage;
    public GameObject fadecon;
    public int fadetime;
    public float fadetimeout;
    bool fadetoscene;
    public string scene;

    [Header("Canvas")]
    public Canvas CanvasCategorias;

    [Header("GameMode")]
    public ContenedorCartas[] ContenedorCartas;

    [Header("Players name")]
    public static InputText inputText;
    public TMP_InputField inputField;

    public string Player;



    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        fadecon.SetActive(true);
        fadetoscene = false;
    }

    void Start()
    {
        brightneSlider.value = .75f;
        brightnessPanel.color = new Color(brightnessPanel.color.r, brightnessPanel.color.g, brightnessPanel.color.b,
            .99f - brightneSlider.value);
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
            SetSFXVolume();
        }

        fadeimage.CrossFadeAlpha(0, fadetime, false);//Opacidad (0 o 1) // duracion // ignorar TimeScale
        StartCoroutine(fadefalser());
    }

    private IEnumerator fadefalser()
    {
        yield return new WaitForSeconds(fadetime + 1);
        fadecon.SetActive(false);
        StopCoroutine(fadefalser());
    }

    public void ChangeBrightness(float value)
    {
        _sliderValue = value;
        PlayerPrefs.SetFloat("Brightness",_sliderValue);
        brightnessPanel.color = new Color(brightnessPanel.color.r, brightnessPanel.color.g, brightnessPanel.color.b,
            .99f - brightneSlider.value);
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        audioController.SetFloat("Music", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("musicVolume",volume);
    }
    public void SetSFXVolume()
    {
        float volume = SFXSlider.value;
        audioController.SetFloat("SFX", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("SFXVolume",volume);
    }

    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        SetMusicVolume();
        SetSFXVolume();
    }

    public void SetName()
    {
        Player = inputField.text;
    }

    public void PlayMusic(string name)
    {
        SoundManager s = Array.Find(musicSounds, x => x.Name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            musicSource.clip = s.Clip;
            musicSource.Play();
        }
    }

    public void PlaySfx(string name)
    {
        SoundManager s = Array.Find(sfxSounds, x => x.Name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            sfxSource.PlayOneShot(s.Clip);
        }
    }

    public void ChangeScene(int sceneindex)
    {
        fadecon.SetActive(true);
        fadeimage.CrossFadeAlpha(1, fadetime, false);//Opacidad (0 o 1) // duracion // ignorar TimeScale
        Settings.Instance.PlaySfx("Contador");

        StartCoroutine(FadeTruer());

        IEnumerator FadeTruer()
        {
            yield return new WaitForSeconds(fadetimeout);

            SceneManager.LoadScene(sceneindex);

            StopCoroutine(FadeTruer());
        }
    }

    public void RestartScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
 
    public void ShowOriginalCanvas()
    {
        CanvasCategorias.gameObject.SetActive(true);
    }

    public void HideOriginalCanvas()
    {
        CanvasCategorias.gameObject.SetActive(false);
    }
    public void SetGameCards(int cards)
    {
        PlayerPrefs.SetInt("GameCards",cards);
        PlayerPrefs.Save();
    }
    public void SetGameMode(int gameMode)
    {
        PlayerPrefs.SetInt("GameMode", gameMode);
        PlayerPrefs.Save();
    }
    public int GetGameMode()
    {
        int gameMode = PlayerPrefs.GetInt("GameMode");
        return gameMode;
    }
    public ContenedorCartas GetCarts()
    {
        int gameMode = PlayerPrefs.GetInt("GameCards"); 
        return ContenedorCartas[gameMode]; 
    }
    //Enums de referencia de modos de juego
    public enum GameMode
    {
        Normal = 0,
        Contrareloj = 1,
        ContraRelosPlus = 2,
        SinFallos = 3,
        ModoVs = 4
    }
    // Enums de referencia de tipos de cartas
    public enum CardType
    {
        Animales = 0,
        Arquitectura = 1,
        Artesanias = 2,
        Alimentos = 3,
        Dioses = 4,
        DiversidadCultural = 5,
        Dulces = 6,
        Vestimenta = 7
    }
}