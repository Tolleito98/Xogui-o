using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{

    public GameObject menuPause;
    public bool isPaused = false;
   
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) 
            {
                reanudar();
            }
            else
            {
                pausar();
            }
        }

        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        
    }

    public void reanudar() 
    {
        menuPause.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }

    public void pausar()
    {
        menuPause.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }

    public void returnMainMenu() 
    {
        SceneManager.LoadScene("Main menu");
    }
}
