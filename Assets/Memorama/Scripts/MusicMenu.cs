using UnityEngine;

public class MusicMenu : MonoBehaviour
{
    void Start()
    {
        Settings.Instance.PlayMusic("MainMusic");
    }
}
