using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;

    [Header("Movimiento")]
    private float horizontal;
    private bool grounded;
    public float speed;
    public float jumpForce;

    [Header("Animacion")]
    private Animator Animator;

    [Header("Dash")]
    [SerializeField] private float speedDash;
    [SerializeField] private float timeDash;
    private float gravedadInicial;
    private bool puedeDash = true;
    private bool puedeMover = true;

    public LayerMask groundLayers;
    private float groundCheckDistance;

    private float wallCheckDistance;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>(); 
        gravedadInicial = Rigidbody2D.gravityScale;

        // get the distance from the player's collider center, to the bottom of the collider, plus a little bit more
        groundCheckDistance = GetComponent<Collider2D>().bounds.extents.y + 0.1f;
        wallCheckDistance = GetComponent<Collider2D>().bounds.extents.x + 0.02f;
    }

    bool canMoveLeft = true;
    bool canMoveRight = true;
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        canMoveLeft = true;
        canMoveRight = true;

        // Validamos si el Player está tocando alguna pared a la izquierda para evitar su movimiento
        if(IsTouchingWall(Vector2.left))
        {     
            canMoveLeft = false;       
        } 

        if(horizontal < 0.0f && canMoveLeft) 
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);                
        }                
     

        // Validamos si el Player está tocando alguna pared a la derecha para evitar su movimiento
        if(IsTouchingWall(Vector2.right))
        {
            canMoveRight = false;
        }    
        
        if(horizontal > 0.0f && canMoveRight) 
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }       
                    

        // Validamos si el Player está sobre el Suelo (Ground) para permitir saltar
        if(IsGrounded())
        {
            grounded = true;
        }
        else 
        {
            grounded = false;
        }   

        // Se ejecuta Jump() solo si se presiona W y está sobre el suelo
        if(Input.GetKeyDown(KeyCode.W) && grounded)
        {
            Jump();
        }

        if(Input.GetKeyDown(KeyCode.C) && puedeDash)
        {
            StartCoroutine(Dash());
        }
    }

    // Usamos un RaycastHit2D para verificar si el Player, está tocando la capa (groundLayers) que configuremos
    // A raycast is used to detect objects that lie along the path of a ray and is conceptually like firing a laser beam into the scene and observing which objects are hit by it.
    // https://docs.unity3d.com/es/530/ScriptReference/RaycastHit2D.html
    private bool IsGrounded() {
        Ray2D ray = new Ray2D(transform.position, Vector2.down);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, groundCheckDistance, groundLayers);
        if(hit) {
            Debug.DrawLine(ray.origin, hit.point, Color.green);
            return true;
        } else {
            Debug.DrawRay(ray.origin, ray.direction * groundCheckDistance, Color.red);
            return false;
        }
    }

    private bool IsTouchingWall(Vector2 vector2D) {
        Ray2D ray = new Ray2D(transform.position + new Vector3(0.0f, -0.02f, 0.0f), vector2D);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, wallCheckDistance, groundLayers);
        if(hit) {
            Debug.DrawLine(ray.origin, hit.point, Color.green);
            return true;
        } else {
            Debug.DrawRay(ray.origin, ray.direction * wallCheckDistance, Color.red);
            return false;
        }
    }

    private void FixedUpdate()
    {
        if(puedeMover)
        {
            if(canMoveLeft && horizontal < 0.0f || canMoveRight && horizontal > 0.0f)
            {
                Rigidbody2D.velocity = new Vector2(horizontal * speed, Rigidbody2D.velocity.y);
                Animator.SetBool("running", horizontal != 0.0f);
            }        
            else
            {
                Animator.SetBool("running", false);
            }        
        }
    }

    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * jumpForce);
    }

    private IEnumerator Dash()
    {
        puedeMover = false;
        puedeDash = false;
        Rigidbody2D.gravityScale = 0;
        Rigidbody2D.velocity = new Vector2(speedDash * transform.localScale.x,0);
        Animator.SetTrigger("dash");

        yield return new WaitForSeconds(timeDash);

        puedeMover = true;
        puedeDash = true;
        Rigidbody2D.gravityScale = gravedadInicial;
    }
}
