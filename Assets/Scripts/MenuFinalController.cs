using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuFinalController : MonoBehaviour
{
    public TextMeshProUGUI tiempoText;

    void Start()
    {
        if (Cronometro.instance != null)
        {
            Cronometro.instance.StopTimer();
            float tiempoFinal = Cronometro.instance.GetFinalTime();

            int minutos = Mathf.FloorToInt(tiempoFinal / 60f);
            int segundos = Mathf.FloorToInt(tiempoFinal % 60f);
            tiempoText.text = $"Tiempo total:\n {minutos:00}:{segundos:00}";
        }
    }

    public void ReiniciarJuego()
    {
        if (Cronometro.instance != null)
        {
            Cronometro.instance.ResetTimer();
        }

        SceneManager.LoadScene("Nivel1"); 
    }

    public void SalirDelJuego()
    {
        Application.Quit();
    }
}
