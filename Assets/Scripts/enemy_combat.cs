using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_combat : MonoBehaviour
{
    [SerializeField] private float vida;
    private Animator animator;
    private enemy_movement enemyMovement; // Referencia al script de movimiento

    private void Start()
    {
        animator = GetComponent<Animator>();
        enemyMovement = GetComponent<enemy_movement>(); // Obtén el componente de movimiento
    }

    public void TomarDaño(float daño)
    {
        vida -= daño;
        if (vida <= 0)
        {
            Muerte();
        }
    }

    private void Muerte()
    {
        animator.SetTrigger("dead");
        if (enemyMovement != null)
        {
            enemyMovement.enabled = false; // Desactiva el script de movimiento
        }
        // También puedes desactivar otros componentes o colisionadores si es necesario
    }
}
