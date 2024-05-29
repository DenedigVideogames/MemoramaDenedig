using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshot : MonoBehaviour
{
    void Awake()
    {
         ScreenCapture.CaptureScreenshot("screenshot.png");  
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ScreenCapture.CaptureScreenshot("screenshot.png");  
            Debug.Log("A screenshot was taken!"); 
        }
    }
}
