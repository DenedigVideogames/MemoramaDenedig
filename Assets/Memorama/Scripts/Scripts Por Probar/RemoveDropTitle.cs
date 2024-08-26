using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class RemoveDropTitle : MonoBehaviour, ISelectHandler
{
    public ColorBlindFilter ColorBlindFilter;
    public TMP_Dropdown.OptionData title;
    public bool Wasntselected = true;
    TMP_Dropdown dropdown;
    public bool returnnormal = false;

    // Start is called before the first frame update
    void Start()
    {
        returnnormal = false;
        dropdown = GetComponent<TMP_Dropdown>();

        dropdown.options.Insert(dropdown.value, title);
        dropdown.RefreshShownValue();
    }
    public void OnSelect(BaseEventData eventData)
    {
        if (Wasntselected)
        {
            RemoveTitle();
            ColorBlindFilter.mode = ColorBlindMode.Normal;
            returnnormal = true;
        }     

        Wasntselected = false;
    }

    public void RemoveTitle()
    {
        dropdown.options.RemoveAt(dropdown.value);
        dropdown.RefreshShownValue();
    }
}
