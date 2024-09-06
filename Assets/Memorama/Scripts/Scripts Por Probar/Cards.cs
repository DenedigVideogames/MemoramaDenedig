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
    public static GameObject cartavol1;
    public static GameObject cartavol2;

    public void OnMouseDown()
    {
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

        if (Comparisons.CartasVolteadas == 2)
        {
            this.GetComponent<BoxCollider>().enabled = false;
            Comparisons.secondID = _selectcart;
            Comparisons.cartavol2 = _cartaselec;
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
