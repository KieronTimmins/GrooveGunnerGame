using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform player; // Reference to the player's transform

    void Update()
    {
        // Check if the player reference is set
        if (player != null)
        {
            // Calculate the direction from the object to the player
            Vector3 directionToPlayer = player.position - transform.position;

            // Calculate the rotation to look downward at the player
            Quaternion lookRotation = Quaternion.LookRotation(-directionToPlayer.normalized, Vector3.up);

            // Apply the rotation
            transform.rotation = Quaternion.Euler(-90f, 0f, 0f) * lookRotation;
        }
        else
        {
            Debug.LogError("Player reference is not set in the LookAtPlayer script.");
        }
    }
}
