using UnityEngine;

[RequireComponent(typeof(AudioSource))]


public class AudioManagger : MonoBehaviour
{

    public static AudioManagger Instance { get; private set; }

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }else
        {
            Debug.Log("Ya hay una instancia de audioManagger");   
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void reproducirSonido(AudioClip audio) 
    {
        audioSource.PlayOneShot(audio);
    }
}
