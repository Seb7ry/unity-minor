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

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>(); 
        gravedadInicial = Rigidbody2D.gravityScale;
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if(horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if(horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        
        Animator.SetBool("running", horizontal != 0.0f);
        
        //Preguntar
        if(Physics2D.Raycast(transform.position, Vector3.down, 0.1f))
        {
            grounded = true;
        }
        else grounded = false;   

        if(Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }

        if(Input.GetKeyDown(KeyCode.C) && puedeDash)
        {
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        if(puedeMover)
        {
            Rigidbody2D.velocity = new Vector2(horizontal * speed, Rigidbody2D.velocity.y);
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
