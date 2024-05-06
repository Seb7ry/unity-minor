using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_combat : MonoBehaviour
{
    [SerializeField] private Transform controladorgolpe;
    [SerializeField] private float radioGolpe;
    [SerializeField] private float dañoGolpe;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.V)){
            Golpe();
        }
    }

    private void Golpe(){
        animator.SetTrigger("golpe");
        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorgolpe.position, radioGolpe);

        foreach(Collider2D colisionador in objetos){
            if(colisionador.CompareTag("Enemy")){
                colisionador.transform.GetComponent<enemy_combat>().TomarDaño(dañoGolpe);
            }
        }
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorgolpe.position, radioGolpe);
    }
}
