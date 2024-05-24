using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_combat : MonoBehaviour
{
    [SerializeField] private float vida;
    private Animator animator;
    private enemy_movement enemyMovement;

    private void Start()
    {
        animator = GetComponent<Animator>();
        enemyMovement = GetComponent<enemy_movement>();
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
            enemyMovement.enabled = false;
        }
    }
}
