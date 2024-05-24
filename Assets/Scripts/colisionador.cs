using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colisionador : MonoBehaviour
{
    public GameObject menuMuerte; // Asigna el objeto del menú de muerte desde el Inspector de Unity

    void Start()
    {
        menuMuerte.SetActive(false); // Asegúrate de que el menú esté desactivado al inicio
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("¡Me caí!");
            menuMuerte.SetActive(true); // Activa el menú de muerte al caerse
        }
    }

    // Método para cerrar el menú de muerte
    public void CerrarMenuMuerte()
    {
        menuMuerte.SetActive(false); // Desactiva el menú de muerte
    }
}
