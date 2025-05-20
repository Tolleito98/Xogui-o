using UnityEngine;

public class KeyController : MonoBehaviour
{

    public AudioClip sonidoLlave;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MainCharacter"))
        {
            AudioManagger.Instance.reproducirSonido(sonidoLlave);
            GameManagger.Instance.ConseguirLLave();
            Destroy(gameObject);
        }
    }
}
