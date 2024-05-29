using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using TMPro;


public class Spawn_Max : MonoBehaviour
{
    public Sprite[] imagenesdeCartas;
    public TextMeshPro[] textodeCartas;
    public int filas = 8;
    public int columnas = 3;
    public Vector2 espacio = Vector2.zero;
    public GameObject carta;
    public float Xpos = 4.91f;
    public float Ypos = 1.55f;
    private int _cartasSpawneadas = 0;
    private int currentWave = 0;
    private int[] waveCardCounts = { 8, 12, 16, 20 };

    private List<int> CardIDs;

    public Button waveButton; // Asigna el botón de Unity desde el Inspector.

    void Start()
    {
        CardIDs = MixCards(waveCardCounts[currentWave]);
        SpawnCards(waveCardCounts[currentWave]);

        // Asigna la función OnWaveButtonClicked al botón de Unity.
        waveButton.onClick.AddListener(OnWaveButtonClicked);
    }

    void Update()
    {
        // No necesitas la detección de tecla en este caso.
    }

    public void OnWaveButtonClicked()
    {
        if (currentWave < waveCardCounts.Length - 1)
        {
            currentWave++;
            CardIDs = MixCards(waveCardCounts[currentWave]);
        }

        SpawnCards(waveCardCounts[currentWave]);
    }

    void SpawnCards(int totalCards)
    {
        // Elimina todas las cartas existentes antes de generar nuevas cartas.
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        int _cartasSpawneadas = 0;

        for (int x = 0; x < filas; x++)
        {
            for (int y = 0; y < columnas; y++)
            {
                if (_cartasSpawneadas < totalCards)
                {
                    GameObject cartas = Instantiate(carta, transform); // Agrega las cartas como hijos del objeto actual.

                    Cards cartaenturno = cartas.GetComponent<Cards>();
                    cartaenturno.id = CardIDs[_cartasSpawneadas];
                    cartaenturno.AsignarImagendeCarta(imagenesdeCartas[cartaenturno.id]);
                    cartaenturno.AsignarTextodeCarta(textodeCartas[cartaenturno.id]);

                    cartas.transform.position = new Vector2(x * espacio.x - Xpos, y * espacio.y - Ypos);
                    _cartasSpawneadas++;
                }
            }
        }
    }

    List<int> MixCards(int totalCards)
    {
        List<int> CardIDs = new List<int>();

        for (int i = 0; i < totalCards; i++)
        {
            CardIDs.Add(i / 2);
        }

        CardIDs.Shuffle();
        return CardIDs;
    }
}
