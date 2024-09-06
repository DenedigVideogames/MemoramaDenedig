using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetSelectionMode : MonoBehaviour
{
    [SerializeField] private Image[] image;
    [SerializeField] private Button[] buttons;
    [SerializeField] private Color selectedGameObject;

    public void SetButtonBackground(string hexcolor)
    {
        Color color;
        if (ColorUtility.TryParseHtmlString(hexcolor, out color))
        {
            for (int i = 0; i < image.Length; i++)
            {
                image[i].color = color;
            }
        }
        else
        {
            Debug.Log("Color incorrecto");
        }
    }

    public void SetHighlighterColor(string hexColor)
    {
        Color color;
        if (ColorUtility.TryParseHtmlString(hexColor, out color))
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                ColorBlock colorBlock = buttons[i].colors;
                colorBlock.highlightedColor = color;
                buttons[i].colors = colorBlock;
            }
        }
        else
        {
            Debug.Log("Color incorrecto");
        }
    }
}
