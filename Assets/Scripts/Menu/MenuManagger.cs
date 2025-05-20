using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManagger : MonoBehaviour
{

     public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


     public void Exit()
    {
        Application.Quit();
        Debug.Log("saliendo xd...");
    }
}
