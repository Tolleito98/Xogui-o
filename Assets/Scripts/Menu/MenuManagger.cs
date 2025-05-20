using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManagger : MonoBehaviour
{

    public GameObject panelCreditos;

     public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void AbrirCreditos()
    {
        panelCreditos.SetActive(true);
    }

    public void CerrarCreditos()
    {
        panelCreditos.SetActive(false);
    }

     public void Exit()
    {
        Application.Quit();
        Debug.Log("saliendo xd...");
    }
}
