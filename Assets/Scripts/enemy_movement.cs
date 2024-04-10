using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_movement : MonoBehaviour
{
    private Animator animator;
    public GameObject target;
    
    [Header("Movimiento")]
    public int rutina;
    public int direccion;
    public bool atack;
    public float speedRun;
    public float cronometro;

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
                animator.SetBool("idle", false);
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
    }

}
