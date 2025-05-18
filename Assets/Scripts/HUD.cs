using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI puntos;

    public GameObject[] vidas;

    void Update()
    {
        puntos.text = GameManagger.Instance.PuntosTotales.ToString();
    }

    public void actualizarPuntos(int puntosTotales) 
    {
        puntos.text = puntosTotales.ToString();
    }

    public void desactivarVida(int indiceVida)
    {
        vidas[indiceVida].SetActive(false);
    }

    public void activarVidas(int indiceVida)
    {
        vidas[indiceVida].SetActive(true);
    }
}
