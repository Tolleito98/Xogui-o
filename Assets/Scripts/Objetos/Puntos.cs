using UnityEngine;

public class Puntos : MonoBehaviour
{
    public int valor = 1;
    public GameManagger gameManagger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
          if (collision.CompareTag("Ataque"))
        {
            gameManagger.sumarPutos(valor);
            Destroy(this.gameObject);

        }
    }
}
