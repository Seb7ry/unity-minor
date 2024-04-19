using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_movement : MonoBehaviour
{
    public GameObject player;
    public Vector2 minPosition;
    public Vector2 maxPosition;

       void Update()
    {
        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.Clamp(player.transform.position.x, minPosition.x, maxPosition.x);
        newPosition.y = Mathf.Clamp(player.transform.position.y, minPosition.y, maxPosition.y);
        newPosition.z = transform.position.z;

        transform.position = newPosition;
    }
}
