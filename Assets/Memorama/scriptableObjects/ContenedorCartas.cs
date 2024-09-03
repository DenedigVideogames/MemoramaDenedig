using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NuevoCartasContenedor", menuName = "Memorama/CartasContenedor")]
public class ContenedorCartas : ScriptableObject
{
    public List<CartaData> cartas;
}
