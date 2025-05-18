using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 3; //This is set is the inspector
    public float jumpForce = 3; //This is set in the inspector
    public float reboundPower = 10; //This is set in the inspector
    public float attackColdown = 0.5f; //This is set in the inspector
    public float coyoteTime = 0.2f; //This is set in the inspector

    public float fuerzaGolpe = 0;

    private float coyoteTimer;

    public AudioClip sonidoSalto;


    private BoxCollider2D boxColider; 
    private Rigidbody2D rigidbody;
    private Animator animator;

    public Collider2D groundAttackColider; //This is set in the inspector
    public Collider2D UpAtackColider; //This is set in the inspector
    public Collider2D DownAttackColider; //This is set in the inspector
    

    public LayerMask floorLayer;
    public LayerMask pointLayer;

    private bool lookingRight = true;
    private bool canMove = true;
    private float lastAttackTime = 0f;



    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxColider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        movement();
        jump();
        getStatus();
        handleAtack();

    }

    void getStatus() 
    {
        animator.SetFloat("verticalSpeed", rigidbody.linearVelocity.y);
        animator.SetBool("isGrounded", isGrounded());
    }

    void handleAtack() 
    {
        if (Time.time >= lastAttackTime + attackColdown)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {

                if (Input.GetKey(KeyCode.DownArrow))
                {
                    animator.SetTrigger("AttackDown");
                    StartCoroutine(ActivateDownAttackCollider());
                }
                else if (Input.GetKey(KeyCode.UpArrow))
                {
                    animator.SetTrigger("AttackUp");
                    StartCoroutine(ActivateUpAttackCollider());
                }
                else
                {
                    animator.SetTrigger("Attack");
                    StartCoroutine(ActivateGroundAttackCollider());

                }

                lastAttackTime = Time.time;

            }
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("muerte"))
        {
            Debug.Log("jay");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }


        if (other.CompareTag("Point"))
        {
            if (DownAttackColider.enabled)
            {
                ApplyReboundEffect();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isGrounded() && collision.contacts.Length > 0)
        {
            Vector2 normal = collision.contacts[0].normal;
            
            
            if (Mathf.Abs(normal.x) > 0.9f)
            {
                rigidbody.linearVelocity = new Vector2(0, rigidbody.linearVelocity.y);
            }
        }
    }

    void ApplyReboundEffect()
    {

        if (rigidbody != null)
        {
            if (rigidbody.linearVelocity.y <= 0)
            {
                rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocityX, 0f);
            }

            Vector2 reboundForce = new Vector2(0, reboundPower);
            rigidbody.AddForce(reboundForce, ForceMode2D.Impulse);
        }
    }

    IEnumerator ActivateDownAttackCollider()
    {
        DownAttackColider.enabled = true; 
        yield return new WaitForSeconds(0.3f); 
        DownAttackColider.enabled = false; 
    }

    

    IEnumerator ActivateGroundAttackCollider()
    {
        groundAttackColider.enabled = true; 
        yield return new WaitForSeconds(0.3f); 
        groundAttackColider.enabled = false; 
    }

    IEnumerator ActivateUpAttackCollider()
    {
        UpAtackColider.enabled = true; 
        yield return new WaitForSeconds(0.3f); 
        UpAtackColider.enabled = false; 
    }


    bool isGrounded() 
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxColider.bounds.center, new Vector2(boxColider.bounds.size.x, boxColider.bounds.size.y), 0f, Vector2.down, 0.2f, floorLayer);
        return raycastHit.collider != null;
    }

    void jump() { 

        if (isGrounded()){
            coyoteTimer = coyoteTime;
        }
        else
        {
            coyoteTimer -= Time.deltaTime;
        }
            bool canjump = coyoteTimer > 0;

        if (Input.GetKeyDown(KeyCode.Space) && canjump || Input.GetKeyDown(KeyCode.X) && canjump)
        {

            AudioManagger.Instance.reproducirSonido(sonidoSalto);

            rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                coyoteTimer = 0f;
        }

    }

    void movement()
    {
        float inputMove = Input.GetAxis("Horizontal");

        if (!canMove) return;   

        if (inputMove != 0f)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

            rigidbody.linearVelocity = new Vector2(inputMove * speed, rigidbody.linearVelocity.y);
        
        flipCharacter(inputMove);
    }

    public void recibirGolpe(Vector2 sourcePosition) 
    {
        Vector2 direccion = ((Vector2)transform.position - sourcePosition);
        float horizontal = direccion.x >= 0 ? 1 : -1;
        direccion = new Vector2(horizontal, 1).normalized;

        canMove = false;

        rigidbody.linearVelocity = Vector2.zero;
        rigidbody.AddForce(direccion * fuerzaGolpe, ForceMode2D.Impulse);

        StartCoroutine(waitToMove());
    }

    IEnumerator waitToMove()
    {
        yield return new WaitForSeconds(0.1f);

        while (!isGrounded())
        {
            yield return null;
        }

        canMove = true;
    }


    void flipCharacter(float inputMove)
    {
        if ((lookingRight == true && inputMove < 0) || (lookingRight == false && inputMove > 0))
        {
            lookingRight = !lookingRight;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }
}
