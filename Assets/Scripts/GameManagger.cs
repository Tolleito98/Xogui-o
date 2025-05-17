using UnityEngine;

public class GameManagger : MonoBehaviour
{
    public static GameManagger Instance {get; private set;}

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

    public void sumarPutos(int puntosObtenidos) 
    {
        puntosTotales +=  puntosObtenidos;
        Debug.Log(puntosTotales);
    }
}
