using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    public string nombreSiguienteNivel;
    public Collider2D colliderFisico;

    private bool gotJKey = false;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (GameManagger.Instance.llaveConseguida)
        {
            animator.SetBool("gotKey", true);
            colliderFisico.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MainCharacter"))
        {
            if (GameManagger.Instance.llaveConseguida)
            {
                
                SceneManager.LoadScene(nombreSiguienteNivel);
            }
            else
            {
                Debug.Log("Necesitas la llave para abrir esta puerta.");
                
            }
        }
    }
}
