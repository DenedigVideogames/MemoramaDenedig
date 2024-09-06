using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ModeChange : MonoBehaviour
{
    public ColorBlindFilter ColorBlindFilter;
    public RemoveDropTitle removeDrop;

    //Bools
    #region
    public static bool normalColor = false;
    public static bool protanopiaColor = false;
    public static bool protanomaliaColor = false;
    public static bool deuteranopiaColor = false;
    public static bool deuteranomaliaColor = false;
    public static bool tritanopiaColor = false;
    public static bool tritanomaliaColor = false;
    public static bool acromatopsiaColor = false;
    public static bool acronomatomaliaColor = false;
    #endregion

    void Awake()
    {        
        if (normalColor == true)
        {
            ColorBlindFilter.mode = ColorBlindMode.Normal;
        }

        if (protanopiaColor == true)
        {
            ColorBlindFilter.mode = ColorBlindMode.Protanopia;
        }

        if (protanomaliaColor == true)
        {
            ColorBlindFilter.mode = ColorBlindMode.Protanomaly;
        }

        if (deuteranopiaColor == true)
        {
            ColorBlindFilter.mode = ColorBlindMode.Deuteranopia;
        }

        if (deuteranomaliaColor == true)
        {
            ColorBlindFilter.mode = ColorBlindMode.Deuteranomaly;
        }

        if (tritanopiaColor == true)
        {
            ColorBlindFilter.mode = ColorBlindMode.Tritanopia;
        }

        if (tritanomaliaColor == true)
        {
            ColorBlindFilter.mode = ColorBlindMode.Tritanomaly;
        }

        if (acromatopsiaColor == true)
        {
            ColorBlindFilter.mode = ColorBlindMode.Achromatopsia;
        }

        if (acronomatomaliaColor == true)
        {
            ColorBlindFilter.mode = ColorBlindMode.Achromatomaly;
        }
    }

    void Update()
    {
        if(removeDrop.returnnormal == true)
        {
            normalColor = true;

            protanopiaColor = false;
            protanomaliaColor = false;
            deuteranopiaColor = false;
            deuteranomaliaColor = false;
            tritanopiaColor = false;
            tritanomaliaColor = false;
            acromatopsiaColor = false;
            acronomatomaliaColor = false;

            removeDrop.returnnormal = false;
        }
    }

    public void ColorModeChange(int index)
    {
        if(index == 0)
        {
            ColorBlindFilter.mode = ColorBlindMode.Normal;
            normalColor = true;

            protanopiaColor = false;
            protanomaliaColor = false;
            deuteranopiaColor = false;
            deuteranomaliaColor = false;
            tritanopiaColor = false;
            tritanomaliaColor = false;
            acromatopsiaColor = false;
            acronomatomaliaColor = false;
        }

        if(index == 1)
        {
            ColorBlindFilter.mode = ColorBlindMode.Protanopia;
            protanopiaColor = true;

            normalColor = false;
            protanomaliaColor = false;
            deuteranopiaColor = false;
            deuteranomaliaColor = false;
            tritanopiaColor = false;
            tritanomaliaColor = false;
            acromatopsiaColor = false;
            acronomatomaliaColor = false;
        }

        if(index == 2)
        {
            ColorBlindFilter.mode = ColorBlindMode.Protanomaly;
            protanomaliaColor = true;

            normalColor = false;
            protanopiaColor = false;
            deuteranopiaColor = false;
            deuteranomaliaColor = false;
            tritanopiaColor = false;
            tritanomaliaColor = false;
            acromatopsiaColor = false;
            acronomatomaliaColor = false;
        }

        if(index == 3)
        {
            ColorBlindFilter.mode = ColorBlindMode.Deuteranopia;
            deuteranopiaColor = true;

            normalColor = false;
            protanopiaColor = false;
            protanomaliaColor = false;
            deuteranomaliaColor = false;
            tritanopiaColor = false;
            tritanomaliaColor = false;
            acromatopsiaColor = false;
            acronomatomaliaColor = false;
        }

        if(index == 4)
        {
            ColorBlindFilter.mode = ColorBlindMode.Deuteranomaly;
            deuteranomaliaColor = true;

            normalColor = false;
            protanopiaColor = false;
            protanomaliaColor = false;
            deuteranopiaColor = false;
            tritanopiaColor = false;
            tritanomaliaColor = false;
            acromatopsiaColor = false;
            acronomatomaliaColor = false;
        }

        if(index == 5)
        {
            ColorBlindFilter.mode = ColorBlindMode.Tritanopia;
            tritanopiaColor = true;

            normalColor = false;
            protanopiaColor = false;
            protanomaliaColor = false;
            deuteranopiaColor = false;
            deuteranomaliaColor = false;
            tritanomaliaColor = false;
            acromatopsiaColor = false;
            acronomatomaliaColor = false;
        }

        if(index == 6)
        {
            ColorBlindFilter.mode = ColorBlindMode.Tritanomaly;
            tritanomaliaColor = true;

            normalColor = false;
            protanopiaColor = false;
            protanomaliaColor = false;
            deuteranopiaColor = false;
            deuteranomaliaColor = false;
            tritanopiaColor = false;
            acromatopsiaColor = false;
            acronomatomaliaColor = false;
        }

        if(index == 7)
        {
            ColorBlindFilter.mode = ColorBlindMode.Achromatopsia;
            acromatopsiaColor = true;

            normalColor = false;
            protanopiaColor = false;
            protanomaliaColor = false;
            deuteranopiaColor = false;
            deuteranomaliaColor = false;
            tritanopiaColor = false;
            tritanomaliaColor = false;
            acronomatomaliaColor = false;
        }

        if(index == 8)
        {
            ColorBlindFilter.mode = ColorBlindMode.Achromatomaly;
            acronomatomaliaColor = true;

            normalColor = false;
            protanopiaColor = false;
            protanomaliaColor = false;
            deuteranopiaColor = false;
            deuteranomaliaColor = false;
            tritanopiaColor = false;
            tritanomaliaColor = false;
            acromatopsiaColor = false;
        }
    }
    public void OnPlay()
    {
        DontDestroyField.isonmyFirstScene = false;
    }

    public void UnPause()
    {
        DontDestroyField.isonmyFirstScene = true;
    }
}