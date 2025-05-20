using UnityEngine;

public class Pinchos : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MainCharacter"))
        {

            GameManagger.Instance.perderVida();
            collision.gameObject.GetComponent<PlayerController>().recibirGolpePinchos(transform.position);

        }


    }
}
