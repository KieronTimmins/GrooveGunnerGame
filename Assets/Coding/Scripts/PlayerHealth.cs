using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public float Health = 100;
    // Start is called before the first frame update
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            // Perform actions when a collision with the player occurs
            Health = -10;
            Debug.Log("connect");
            // Add your custom logic here, such as changing a variable, playing a sound, or triggering an event.
        }
        



    }
}
