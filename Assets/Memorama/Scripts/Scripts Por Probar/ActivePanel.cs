using UnityEngine;

public class ActivePanel : MonoBehaviour

{
    public GameObject panelAcierto; // Aqu� va el panel acierto 
    /*public GameObject panelError;   // Aqu� va el panel de error*/
    public MovimientoCamara moviminetoCamara;

    private bool panelAciertoActivo = false;
    private bool panelErrorActivo = false;



    private void Update()
    {
       
        if (Comparisons.Checktimerend == true)
        {
            
            ActivarPanel(panelAcierto);
           
        }
        
        if (Comparisons.Errortimerend == true)
        {
            
            /*ActivarPanel(panelError);*/
            StartCoroutine(moviminetoCamara.Shake());
        }
    }

    private void ActivarPanel(GameObject panel)
    {
       
        panel.SetActive(true);
        Invoke("DesactivarPanel", 1.0f);
    }

    private void DesactivarPanel()
    {
        
        panelAcierto.SetActive(false);
        /*panelError.SetActive(false);*/

        panelAciertoActivo = false;
        panelErrorActivo = false;
    }
}


