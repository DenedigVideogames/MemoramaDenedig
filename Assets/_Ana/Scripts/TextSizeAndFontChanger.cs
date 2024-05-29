using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextSizeAndFontChanger : MonoBehaviour
{
    public List<TextMeshProUGUI> textMeshProTexts; // Arrastra los objetos de texto TextMeshPro aqu� en el Inspector
    public float newSize = 20f; // Tama�o del texto que deseas establecer al hacer clic en el bot�n
    private void Start()
    {
                ChangeTextSizeAndFont();
        // Aseg�rate de que al menos un objeto TextMeshPro est� asignado en el Inspector
        if (textMeshProTexts == null || textMeshProTexts.Count == 0)
        {
            Debug.LogError("No se han asignado objetos TextMeshPro en la lista en el Inspector.");
        }
    }

    public void ChangeTextSizeAndFont()
    {
        // Itera a trav�s de la lista de objetos TextMeshPro y cambia su tama�o y fuente
        foreach (var textMeshProText in textMeshProTexts)
        {
            // Cambia el tama�o del texto TextMeshPro al valor especificado
            textMeshProText.fontSize = newSize;

            // Cambia la fuente del texto TextMeshPro al valor especificado
        }
    }
}
