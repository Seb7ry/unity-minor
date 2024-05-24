using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_movement : MonoBehaviour
{
    [Header("Movimiento")]
    private Animator animator;
    public GameObject target;
    public Rigidbody2D rb2d;
    public float velocidadMovimiento;
    public LayerMask capaAbajo;
    public LayerMask capaEnfrente;
    public float distanciaAbajo;
    public float distanciaEnfrente;
    public Transform controladorAbajo;
    public Transform controladorEnfrente;
    public bool informacionAbajo;
    public bool informacionEnfrente;
    private bool mirandoALaDerecha = true;
    

    [Header("Attack")]
    public float rangoVision;
    public float rangoAtaque;
    public bool atack;
    public GameObject rango;
    public GameObject hit;

    private bool isDead; // Nueva variable para verificar si está muerto

    void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.Find("player");
   
    }

    void Update()
    {
        if (!isDead)
        {
            rb2d.velocity = new Vector2(velocidadMovimiento, rb2d.velocity.y);
            informacionEnfrente = Physics2D.Raycast(controladorEnfrente.position, transform.right, distanciaEnfrente, capaEnfrente);
            informacionAbajo = Physics2D.Raycast(controladorAbajo.position, transform.up * -1, distanciaAbajo, capaAbajo);

            if(informacionEnfrente || !informacionAbajo)
            {
                Girar();
            }
        }
    }

    private void Girar()
    {
        mirandoALaDerecha = !mirandoALaDerecha;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        velocidadMovimiento *= -1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(controladorAbajo.transform.position, controladorAbajo.transform.position + transform.up * -1 * distanciaAbajo);
        Gizmos.DrawLine(controladorEnfrente.transform.position, controladorEnfrente.transform.position + transform.right * distanciaEnfrente);
    }

    public void SetDead()
    {
        isDead = true;
        velocidadMovimiento = 0;
        animator.SetBool("run", false);
        animator.SetBool("attack", false);
    }
}
