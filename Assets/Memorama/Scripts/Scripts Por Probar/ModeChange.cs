using System.Collections;
using UnityEngine;

public class ModeChange : MonoBehaviour
{
    public ColorBlindFilter ColorBlindFilter;
    public RemoveDropTitle removeDrop;

    public enum ColorMode
    {
        Normal,
        Protanopia,
        Protanomaly,
        Deuteranopia,
        Deuteranomaly,
        Tritanopia,
        Tritanomaly,
        Achromatopsia,
        Achromatomaly
    }

    public static ColorMode currentColorMode = ColorMode.Normal;

    void Awake()
    {
        ApplyColorMode(currentColorMode);
    }

    void Update()
    {
        if (removeDrop.returnnormal)
        {
            ChangeColorMode(ColorMode.Normal);
            removeDrop.returnnormal = false;
        }
    }

    public void ColorModeChange(int index)
    {
        if (index >= 0 && index < System.Enum.GetValues(typeof(ColorMode)).Length)
        {
            ChangeColorMode((ColorMode)index);
        }
    }

    private void ChangeColorMode(ColorMode mode)
    {
        currentColorMode = mode;
        ApplyColorMode(mode);
    }

    private void ApplyColorMode(ColorMode mode)
    {
        ColorBlindFilter.mode = (ColorBlindMode)mode;
    }
}
