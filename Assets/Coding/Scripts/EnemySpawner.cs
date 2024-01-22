using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // The enemy prefab to spawn
    public int maxEnemies = 10; // Maximum number of enemies to spawn
    public float spawnRadius = 10f; // Maximum distance from spawner (increased spawn radius)
    public float spawnInterval = 2f;
    public float spawnHeight = 0.3f;

    private float timeSinceLastSpawn;
    private int spawnedEnemiesCount;

    void Update()
    {
        // Check if the maximum number of enemies has been reached
        if (spawnedEnemiesCount < maxEnemies)
        {
            // Update the timer
            timeSinceLastSpawn += Time.deltaTime;

            // Check if it's time to spawn a new enemy
            if (timeSinceLastSpawn >= spawnInterval)
            {
                SpawnEnemy();
                timeSinceLastSpawn = 0f; // Reset the timer
            }
        }
    }

    void SpawnEnemy()
    {
        // Generate a random position within the specified spawn radius
        Vector3 randomSpawnPosition = transform.position + Random.onUnitSphere * spawnRadius;
        randomSpawnPosition.y = spawnHeight;

        // Instantiate a new enemy at the random spawn position
        Instantiate(enemyPrefab, randomSpawnPosition, Quaternion.identity);

        // Increase the count of spawned enemies
        spawnedEnemiesCount++;
    }


}
