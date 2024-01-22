using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage = 10f;
    public float fireRate = 0.5f;
    public LayerMask enemyLayer;

    private float nextFireTime;

    void Update()
    {
        // Check if the player can shoot
        if (Time.time >= nextFireTime && Input.GetButtonDown("Fire1"))
        {
            // Perform shooting logic
            Shoot();

            // Update the next fire time
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void Shoot()
    {
        // Cast a ray from the camera's position forward
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, enemyLayer))
        {
            // Check if the ray hits an object with the "Enemy" tag
            if (hit.collider.CompareTag("Enemy"))
            {
                // Get the Enemy script from the hit object
                //Enemy enemy = hit.collider.GetComponent<Enemy>();

                // Check if the enemy script is not null
                //if (enemy != null)
                {
                    // Deal damage to the enemy
                    //enemy.TakeDamage(damage);
                }
            }
        }
    }
}
