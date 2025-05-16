using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public GameManagger gameManagger;
    public TextMeshProUGUI puntos;

    void Start()
    {
        
    }

  
    void Update()
    {
        puntos.text = gameManagger.PuntosTotales.ToString();
    }
}
