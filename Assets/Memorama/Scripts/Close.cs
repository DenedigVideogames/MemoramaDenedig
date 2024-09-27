using UnityEngine;

public class Close : MonoBehaviour
{
    [SerializeField] private GameObject gameObject;
    public void CloseCanvas()
    {
        gameObject.SetActive(false);
    }
}