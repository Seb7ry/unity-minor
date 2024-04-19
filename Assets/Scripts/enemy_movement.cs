using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_movement : MonoBehaviour
{
    [Header("Movimiento")]
    private Animator animator;
    public GameObject target;
    public int rutina;
    public int direccion;
    public float speedRun;
    public float cronometro;

    [Header("Attack")]
    public float rangoVision;
    public float rangoAtaque;
    public bool atack;
    public GameObject rango;
    public GameObject hit;

    void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.Find("player");
    }

    void Update()
    {
        Comportamientos();
    }


    public void Comportamientos()
    {
        if(Mathf.Abs(transform.position.x - target.transform.position.x) > rangoVision && !atack)
        {   
            animator.SetBool("run", false);
            cronometro += 1 * Time.deltaTime;
            if(cronometro >= 4)
            {
                rutina = Random.Range(0,2);
                cronometro = 0;
            }

            switch(rutina)
            {
                case 0:
                    //animator.SetBool("idle", false);
                    break;
            
                case 1:
                    direccion = Random.Range(0,2);
                    rutina++;
                    break;
                case 2:
                    switch(direccion)
                    {
                        case 0:
                            transform.rotation = Quaternion.Euler(0,0,0);
                            transform.Translate(Vector3.right * speedRun * Time.deltaTime);
                            break;
                        case 1:
                            transform.rotation = Quaternion.Euler(0,180,0);
                            transform.Translate(Vector3.right * speedRun * Time.deltaTime);
                            break;
                    }
                    break;
            }
        }else
        {
            if(Mathf.Abs(transform.position.x - target.transform.position.x) > rangoAtaque && !atack)
            {
                if(transform.position.x < target.transform.position.x)
                {
                    transform.Translate(Vector3.right * 0.6f * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(0,0,0);
                    animator.SetBool("attack", false);
                } else 
                {
                    transform.Translate(Vector3.right * 0.6f * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(0,180,0);
                    animator.SetBool("attack", false);
                }
            }
            else
            {
                if(!atack)
                {
                    if(transform.position.x < target.transform.position.x)
                    {
                        transform.rotation = Quaternion.Euler(0,0,0);
                    }
                    else
                    {
                        transform.rotation = Quaternion.Euler(0,180,0);
                    }
                    animator.SetBool("run", false);
                }
            }
        }
    }

    public void FinalAni()
    {
        animator.SetBool("attack", false);
        atack = false;
        rango.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void ColliderWeaponTrue()
    {
        hit.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void ColliderWeaponFalse()
    {
        hit.GetComponent<BoxCollider2D>().enabled = false;
    }

}
