using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMenu : MonoBehaviour
{
    void Start()
    {
        Settings.Instance.PlayMusic("MainMusic");
    }
}
