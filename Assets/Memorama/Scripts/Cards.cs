using UnityEngine;
using TMPro;

public class Cards : MonoBehaviour
{
    public int id { get; set; }
    public SpriteRenderer cartaImagen;
    public TextMeshPro textoimage;

    private int _selectcart;
    private GameObject _cartaselec;
    public Animator animopen;

    public void OnMouseDown()
    {
        if (Comparisons.CartasVolteadas == 2) return;
        animopen.SetTrigger("Open");
        Settings.Instance.PlaySfx("VolteoCarta");
        Comparisons.CartasVolteadas += 1;
        _selectcart = this.id;
        _cartaselec = this.gameObject;

        if (Comparisons.CartasVolteadas == 1)
        {
            this.GetComponent<BoxCollider>().enabled = false;
            Comparisons.firstID = _selectcart;
            Comparisons.cartavol1 = _cartaselec;
        }
        else if (Comparisons.CartasVolteadas == 2)
        {
            this.GetComponent<BoxCollider>().enabled = false;
            Comparisons.secondID = _selectcart;
            Comparisons.cartavol2 = _cartaselec;
            Comparisons.Instance.CheckPar();  // Llamada a la verificación
        }
    }

    public void AsignarImagendeCarta(Sprite imagen)
    {
        cartaImagen.sprite = imagen;
    }

    public void AsignarTextodeCarta(string texto)
    {
        textoimage.text = texto;
    }
}
