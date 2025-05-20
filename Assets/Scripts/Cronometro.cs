using UnityEngine;

public class Cronometro : MonoBehaviour
{
    public static Cronometro instance;

    private float elapsedTime = 0f;
    private bool isRunning = true;

    void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
        }
    }

    public void VerTiempo()
    {
        isRunning = false;
        Debug.Log("Tiempo: " + elapsedTime);
    }

    public void StopTimer()
    {
        isRunning = false;
        Debug.Log("Tiempo final: " + elapsedTime);
    }

    public float GetFinalTime()
    {
        return elapsedTime;
    }

    public void ResetTimer()
    {
        elapsedTime = 0f;
        isRunning = true;
    }
}
