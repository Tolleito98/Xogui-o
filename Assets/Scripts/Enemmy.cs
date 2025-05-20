using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Enemmy : MonoBehaviour
{
    public float radioBusqueda;
    public LayerMask capaPlayer;
    public Transform transformPlayer;

    public AudioClip morreuSonido;

    public EstadosMovimiento estadoActual;

    public float vidaEnemigo = 2;
    public float fuerzaGolpe = 5;
    public float velocidadMovimiento;
    public float stunTime = 1f;
    private float stunTimer = 0f;
    private bool estaEnPausa;

    public float distanciaMaxima;
    public Vector3 posicionInicial;

    public bool mirandoDerecha;

    private Rigidbody2D rigidbody;
    private BoxCollider2D col;

    public Animator animator;

    public enum EstadosMovimiento 
    { 
        Esperando,
        Siguiendo,
        Volviendo
    }


    private void Start()
    {
        posicionInicial = transform.position;
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
    }


    private void Update()
    {

        if (estaEnPausa)
        {
            stunTimer -= Time.deltaTime;

            Debug.Log(stunTimer);

            if (stunTimer <= 0f)
            {

                estaEnPausa = false;
            }
        }

        switch (estadoActual)
        {
            case EstadosMovimiento.Esperando:
                EstadoEsperando();
                break;
            case EstadosMovimiento.Siguiendo:
                EstadoSiguiendo();
                break;
            case EstadosMovimiento.Volviendo:
                EstadoVolviendo();
                break;

        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MainCharacter"))
        {
  
            GameManagger.Instance.perderVida();
            collision.gameObject.GetComponent<PlayerController>().recibirGolpe(transform.position);
            
        }

       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.transform.parent.GetComponent<PlayerController>();

        if (collision.gameObject.CompareTag("Ataque"))
        {
            Debug.Log("Si joder si");
            recibirGolpe(collision.transform.position);
            vidaEnemigo--;


            if (vidaEnemigo <= 0)
            {
                AudioManagger.Instance.reproducirSonido(morreuSonido);
                Morir();
            }

        }
    }

    private void Morir()
    {
        col.enabled = false;
        rigidbody.gravityScale = 0;
        animator.SetTrigger("morreu");
        estaEnPausa = true; 
        rigidbody.linearVelocity = Vector2.zero;
        StartCoroutine(EsperarAnimacionMuerte());
    }

    private IEnumerator EsperarAnimacionMuerte()
    {
        
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        Destroy(this.gameObject);
    }


    private void EstadoEsperando()
    {
        if (estaEnPausa) return;


        Collider2D jugadorCollider = Physics2D.OverlapCircle(transform.position, radioBusqueda, capaPlayer);

        if (jugadorCollider)
        {
            
            if (Vector2.Distance(posicionInicial, jugadorCollider.transform.position) <= distanciaMaxima)
            {
                transformPlayer = jugadorCollider.transform;
                estadoActual = EstadosMovimiento.Siguiendo;
            }
        }
        else
        {
            rigidbody.linearVelocity = Vector2.zero;
        }


    }

    public void EstadoSiguiendo()
    {

        if (estaEnPausa) return;
        animator.SetBool("isChasing", true);

        

        if (transformPlayer == null)
        {
            estadoActual = EstadosMovimiento.Volviendo;
            return;
        }


        if (Vector2.Distance(posicionInicial, transformPlayer.position) > distanciaMaxima)
        {
            estadoActual = EstadosMovimiento.Volviendo;
            transformPlayer = null;
            return;
        }

        float direccion = Mathf.Sign(transformPlayer.position.x - transform.position.x);
        rigidbody.linearVelocity = new Vector2(direccion * velocidadMovimiento, rigidbody.linearVelocity.y);

        mirarObjetivo(transformPlayer.position);
    }


    private void EstadoVolviendo()
    {
        if (estaEnPausa) return;


        animator.SetBool("isChasing", false);

        
        Collider2D jugadorCollider = Physics2D.OverlapCircle(transform.position, radioBusqueda, capaPlayer);

        if (jugadorCollider)
        {
            if (Vector2.Distance(posicionInicial, jugadorCollider.transform.position) <= distanciaMaxima)
            {
                transformPlayer = jugadorCollider.transform;
                estadoActual = EstadosMovimiento.Siguiendo;
                return;
            }
        }

        float distanciaAInicio = Vector2.Distance(transform.position, posicionInicial);

        if (distanciaAInicio < 0.2f)
        {
            rigidbody.linearVelocity = Vector2.zero;
            estadoActual = EstadosMovimiento.Esperando;
            return;
        }

        float direccion = Mathf.Sign(posicionInicial.x - transform.position.x);
        rigidbody.linearVelocity = new Vector2(direccion * velocidadMovimiento, rigidbody.linearVelocity.y);

        mirarObjetivo(posicionInicial);
    }

    public void recibirGolpe(Vector2 sourcePosition)
    {
        
        Vector2 direccion = ((Vector2)transform.position - sourcePosition).normalized;

        rigidbody.linearVelocity = Vector2.zero;  
        rigidbody.AddForce(direccion * fuerzaGolpe, ForceMode2D.Impulse);  

        estaEnPausa = true;
        stunTimer = stunTime;
    }

    private void mirarObjetivo(Vector3 objetivo)
    {
        if (estaEnPausa) return;

        if (objetivo.x > transform.position.x && !mirandoDerecha)
        {
            Girar();
        } else if (objetivo.x < transform.position.x && mirandoDerecha)
        {
            Girar();
        }
        
    }

    private void Girar()
    {
        mirandoDerecha = !mirandoDerecha;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);

    }
        

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioBusqueda);
        Gizmos.DrawWireSphere(posicionInicial, distanciaMaxima);

    }
}
