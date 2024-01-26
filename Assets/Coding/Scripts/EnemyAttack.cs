using UnityEngine;

public class EnemyFollowAndAttack : MonoBehaviour
{
    public Transform playerTransform; // Assign this to the player's transform in the inspector
    public float chaseSpeed = 3.0f;
    public float attackRange = 2.0f; // Range within which the enemy can attack
    public float damagePerSecond = 10.0f; // Damage dealt to the player per second
    public float rotationSpeed = 5.0f; // Speed of rotation towards the player

    private float lastAttackTime;

    void Update()
    {
        if (playerTransform == null)
        {
            Debug.LogError("Player Transform not set on " + gameObject.name);
            return;
        }

        // Calculate the distance to the player
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= attackRange)
        {
            // If within attack range, deal damage over time
            if (Time.time - lastAttackTime >= 1f) // 1 second between attacks
            {
                playerTransform.GetComponent<PlayerHealth>().currentHealth -= damagePerSecond;
                lastAttackTime = Time.time;
            }
        }
        else
        {
            // Move towards the player
            Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
            transform.position += directionToPlayer * chaseSpeed * Time.deltaTime;

            // Rotate towards the player
            Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        }
    }
}

