using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    public int damage = 10; // Damage inflicted by the enemy per attack
    public float attackCooldown = 1.5f; // Cooldown between attacks
    public Transform target; // Reference to the player's transform

    private NavMeshAgent navMeshAgent;
    private bool canAttack = true;
    public GameObject Player;
    public float normalSpeed = 3f; // Set your initial speed here
    public float fasterSpeed = 100f;

    void Start()
    {
        // Get the NavMeshAgent component
        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.speed = 10;

        // If player is not assigned, try to find it in the scene




    }

    void Update()
    {
        
        
            // Set the destination to the player's position
            navMeshAgent.SetDestination(target.position);

            // Check if the player is within attack range
            float distanceToPlayer = Vector3.Distance(transform.position, target.position);
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("speed");
            navMeshAgent.speed = 200;
        }

    }
    

    






}
