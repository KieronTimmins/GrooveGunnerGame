using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Monster : MonoBehaviour
{
    public float health = 100f;
    public EnemySpawner spawner;
    public AudioClip deathSound; // Reference to the death sound clip
    public GameObject bloodEffectPrefab; // Reference to the blood effect prefab

    private Animator animator;
    private AudioSource playerAudioSource; // Reference to the player's AudioSource

    void Start()
    {
        animator = GetComponent<Animator>();

        // Find the player's AudioSource in the scene
        // Assuming there's a tag "Player" attached to the player game object
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerAudioSource = player.GetComponent<AudioSource>();
        }
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

        // Play the death sound using the player's AudioSource
        if (playerAudioSource != null && deathSound != null)
        {
            playerAudioSource.PlayOneShot(deathSound);
        }

        // Instantiate the blood effect
        if (bloodEffectPrefab != null)
        {
            Instantiate(bloodEffectPrefab, transform.position, Quaternion.identity);
        }

        PlayerProfile.Instance.IncreaseEnemiesKilled();

        // Code for monster death, like playing an animation
        Destroy(gameObject); // Destroy the monster object
    }
}

