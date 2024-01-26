using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyGameObjects; // Array of enemy GameObjects to spawn
    public Transform playerTransform; // Reference to the player's transform
    public int maxEnemies = 10; // Maximum number of enemies to spawn
    public int minEnemies = 8; // Minimum number of enemies to maintain
    public float spawnRadius = 10f; // Maximum distance from spawner
    public float spawnInterval = 2f; // Interval between spawns
    public float spawnHeight = 0.3f; // Spawn height

    private float timeSinceLastSpawn;
    private int spawnedEnemiesCount;

    void Update()
    {
        // Check if the number of enemies is below the minimum threshold
        if (spawnedEnemiesCount < minEnemies)
        {
            timeSinceLastSpawn += Time.deltaTime;

            // Spawn an enemy if the spawn interval has passed
            if (timeSinceLastSpawn >= spawnInterval)
            {
                SpawnEnemy();
                timeSinceLastSpawn = 0f;
            }
        }
        else if (spawnedEnemiesCount < maxEnemies)
        {
            timeSinceLastSpawn += Time.deltaTime;

            // Spawn an enemy if the spawn interval has passed
            if (timeSinceLastSpawn >= spawnInterval)
            {
                SpawnEnemy();
                timeSinceLastSpawn = 0f;
            }
        }
    }

    void SpawnEnemy()
    {
        if (enemyGameObjects.Length == 0 || playerTransform == null)
            return;

        Vector3 randomSpawnPosition = transform.position + Random.onUnitSphere * spawnRadius;
        randomSpawnPosition.y = spawnHeight;

        GameObject selectedEnemy = enemyGameObjects[Random.Range(0, enemyGameObjects.Length)];
        GameObject newEnemy = Instantiate(selectedEnemy, randomSpawnPosition, Quaternion.identity);
        newEnemy.transform.parent = transform; // Optional, for organization

        // Set the playerTransform reference on the spawned enemy
        EnemyFollowAndAttack enemyScript = newEnemy.GetComponent<EnemyFollowAndAttack>();
        if (enemyScript != null)
        {
            enemyScript.playerTransform = playerTransform;
        }

        Monster monsterScript = newEnemy.GetComponent<Monster>();
        if (monsterScript != null)
        {
            monsterScript.spawner = this;
        }

        spawnedEnemiesCount++;
    }

    public void OnEnemyDestroyed()
    {
        if (spawnedEnemiesCount > 0)
            spawnedEnemiesCount--;
    }
}

