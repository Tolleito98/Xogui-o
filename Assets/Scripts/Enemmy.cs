using UnityEngine;
using UnityEngine.Rendering;

public class Enemmy : MonoBehaviour
{
    public float radioBusqueda;
    public LayerMask capaPlayer;
    public Transform transformPlayer;

    public EstadosMovimiento estadoActual;

    public float velocidadMovimiento;
    public float distanciaMaxima;
    public Vector3 posicionInicial;

    public bool mirandoDerecha;

    public Rigidbody2D rigidbody;
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
    }


    private void Update()
    {
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


    private void EstadoEsperando()
    {
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

    private void mirarObjetivo(Vector3 objetivo)
    {
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
