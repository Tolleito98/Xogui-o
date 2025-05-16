using UnityEngine;

public class GameManagger : MonoBehaviour
{
    public int PuntosTotales { get { return puntosTotales; } }
    private int puntosTotales;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void sumarPutos(int puntosObtenidos) 
    {
        puntosTotales +=  puntosObtenidos;
        Debug.Log(puntosTotales);
    }
}
