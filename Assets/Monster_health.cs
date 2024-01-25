using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_health : MonoBehaviour
{
    // Public variable to adjust the damage from the bullet
    public int bulletDamage = 10;

    // Health of the monster
    public int health = 100; // You can adjust this initial health as needed

    // Update is called once per frame
    void Update()
    {
        // Check if health has dropped to 0 or below
        if (health <= 0)
        {
            // Trigger death animation here
            // For example, Animator.SetTrigger("Die");

            // Optionally, destroy the monster after the animation
            Destroy(gameObject); // You can also use a delay if needed, like Destroy(gameObject, 2f);
        }
    }

    // Method to handle collision with bullets
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object has the tag 'Bullet'
        if (collision.gameObject.tag == "Bullet")
        {
            // Reduce health by bullet damage
            Debug.Log("Monster hit by bullet"); 
            health -= bulletDamage;

            // Destroy the bullet after hitting
            Destroy(collision.gameObject);
        }
    }
}

