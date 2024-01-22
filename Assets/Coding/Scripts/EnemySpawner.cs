using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // The enemy prefab to spawn
    public float spawnInterval = 2f; // Time interval between spawns
    public int maxEnemies = 10; // Maximum number of enemies to spawn
    public Transform spawnPoint; // The point where enemies will be spawned

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
        // Instantiate a new enemy at the spawn point
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

        // Increase the count of spawned enemies
        spawnedEnemiesCount++;
    }

}
