using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyField : MonoBehaviour
{
    public static DontDestroyField inst;
    public GameObject input;
    public string myFirstScene;
    public static bool isonmyFirstScene = true;
    Scene scene;

    void Awake()
    {
        if (inst == null)
        {
            inst = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }                  
    }

    void Update()
    {
        scene = SceneManager.GetActiveScene();
        
        if(scene.name == myFirstScene)
        {
            Canvas canvas = gameObject.GetComponent<Canvas>();
            canvas.worldCamera = Camera.main;
        }

        if(scene.name == myFirstScene && isonmyFirstScene == true)
        {
            input.SetActive(true);
        }

        if(isonmyFirstScene == false)
        {
            input.SetActive(false);
        }
    }
}
