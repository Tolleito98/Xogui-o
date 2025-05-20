using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagger : MonoBehaviour
{
    public static GameManagger Instance {get; private set;}

    public HUD hud;

    private int vidas = 3;
    public bool llaveConseguida = false;

    public int PuntosTotales { get { return puntosTotales; } }
    private int puntosTotales;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }else
        {
            Debug.Log("Hay más de una instancia de GameManagger");
        }
    }

    public void perderVida()
    {
        vidas -= 1;
        hud.desactivarVida(vidas);
        if (vidas <= 0) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void recuperarVida()
    {
        hud.activarVidas(vidas);
        vidas += 1;
        
    }

    public void ConseguirLLave()
    {
        llaveConseguida = true;
    }

    public void QuitarLlave()
    {
        llaveConseguida = false;
    }

}
