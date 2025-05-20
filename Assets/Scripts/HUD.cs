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
        
        if (GameManagger.Instance.llaveConseguida) iconoLlave.SetActive(true);
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
