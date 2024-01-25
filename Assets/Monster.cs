using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float health = 100f;
    public EnemySpawner spawner;

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();

        }
    }

    void Die()
    {
        if (spawner != null)
        {
            spawner.OnEnemyDestroyed();
        }
        // Code for monster death, like playing an animation
        Destroy(gameObject); // Destroy the monster object
    }
}
