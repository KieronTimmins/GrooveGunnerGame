using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 20f;

    private void OnTriggerEnter(Collider other)
    {
        Monster monster = other.GetComponent<Monster>();
        if (monster != null)
        {
            monster.TakeDamage(damage);
            Destroy(gameObject); // Destroy the bullet on impact
        }
    }
}

