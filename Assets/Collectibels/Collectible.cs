using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public AudioClip collectSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Access the AudioSource component from the player
            AudioSource playerAudioSource = other.gameObject.GetComponent<AudioSource>();

            if (playerAudioSource != null)
            {
                // Play the collect sound using the player's AudioSource
                playerAudioSource.PlayOneShot(collectSound);
            }
            else
            {
                Debug.LogWarning("AudioSource component not found on the player");
            }

            // Optional: Disable or destroy the collectable
            gameObject.SetActive(false);
            // or Destroy(gameObject);
        }
    }
}
