using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI puntos;


    void Update()
    {
        puntos.text = GameManagger.Instance.PuntosTotales.ToString();
    }
}
