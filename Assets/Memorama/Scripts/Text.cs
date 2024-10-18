using UnityEngine;
using TMPro;

public class Text : MonoBehaviour
{
    [SerializeField] int id;
    public void UpdateID(int _id)
    {
        id = _id;
        UpdateText();
    }
    private void Start()
    {
        TextManager.Instance.LanguageChanged += UpdateText;
        UpdateText();
    }

    private void OnDisable()
    {
        if (TextManager.Instance != null)
        {
            TextManager.Instance.LanguageChanged -= UpdateText;
        }
    }

    public void UpdateText()
    {
        transform.GetComponent<TextMeshProUGUI>().text = TextManager.Instance.setText(id);
    }
}
