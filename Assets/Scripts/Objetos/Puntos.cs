using UnityEngine;

public class Puntos : MonoBehaviour
{
    public int valor = 1;
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
          if (collision.CompareTag("Ataque"))
          {
            
            Destroy(this.gameObject);

          }
    }
}
