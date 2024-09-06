using System.Collections;
using UnityEngine;

public class MovimientoCamara : MonoBehaviour
{
    public float duration;
    public float magnitude;
    private Vector3 _originalPosition;
    private bool _able;

    private void Start()
    {
        _originalPosition = transform.position;
    }

    private void Update()
    {
        if (_able == true)
        {
            transform.localPosition = _originalPosition;
        }   
    }

    public IEnumerator Shake()
    {
        _able = false;
        float x = Random.Range(-1f, 1f) * magnitude;
        float y = Random.Range(-1f, 1f) * magnitude;
        transform.localPosition = new Vector3(x, y, _originalPosition.z);
        yield return new WaitForSeconds(duration);
        _able = true;
    }
}