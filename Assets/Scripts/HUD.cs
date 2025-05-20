using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI puntos;

    public GameObject iconoLlave;
    public GameObject[] vidas;

    void Update()
    {
        puntos.text = GameManagger.Instance.PuntosTotales.ToString();
        if (GameManagger.Instance.llaveConseguida) iconoLlave.SetActive(true);
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
