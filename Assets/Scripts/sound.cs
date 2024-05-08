using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound : MonoBehaviour
{
   public Transform player; // Referencia al transform del jugador
    public AudioSource audioSource; // Referencia al AudioSource del efecto de sonido
    public float maxDistance = 10f; // Distancia máxima a la que se escuchará el sonido a su volumen máximo

    void Update()
    {
        // Calcula la distancia entre el jugador y el objeto
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Calcula el volumen basado en la distancia usando una función de atenuación
        float volume = 1f; // Volumen máximo por defecto
        if (distanceToPlayer > maxDistance)
        {
            volume = 0f; // Si está más allá de la distancia máxima, el volumen es cero
        }
        else
        {
            // Calcula el volumen basado en la distancia usando una atenuación inversa cuadrática
            volume = 1f / (distanceToPlayer * distanceToPlayer);
        }

        // Aplica el volumen al AudioSource
        audioSource.volume = volume;
    }
}
