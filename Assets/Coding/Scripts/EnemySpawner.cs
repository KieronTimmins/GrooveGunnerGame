using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyGameObjects; // Array of enemy GameObjects to spawn
    public int maxEnemies = 10; // Maximum number of enemies to spawn
    public int minEnemies = 8; // Minimum number of enemies to maintain
    public float spawnRadius = 10f; // Maximum distance from spawner
    public float spawnInterval = 2f; // Interval between spawns
    public float spawnHeight = 0.3f; // Spawn height

    private float timeSinceLastSpawn;
    private int spawnedEnemiesCount;

    void Update()
    {
        if (spawnedEnemiesCount < maxEnemies)
        {
            timeSinceLastSpawn += Time.deltaTime;

            // Check if it's time to spawn a new enemy or if the number of enemies is below the minimum
            if (timeSinceLastSpawn >= spawnInterval || spawnedEnemiesCount < minEnemies)
            {
                SpawnEnemy();
                timeSinceLastSpawn = 0f;
            }
        }
    }

    void SpawnEnemy()
    {
        if (enemyGameObjects.Length == 0)
            return;

        Vector3 randomSpawnPosition = transform.position + Random.onUnitSphere * spawnRadius;
        randomSpawnPosition.y = spawnHeight;

        GameObject selectedEnemy = enemyGameObjects[Random.Range(0, enemyGameObjects.Length)];
        GameObject newEnemy = Instantiate(selectedEnemy, randomSpawnPosition, Quaternion.identity);

        // Assign the spawner as a parent (optional, for organization)
        newEnemy.transform.parent = transform;

        Monster monsterComponent = newEnemy.GetComponent<Monster>();
        if (monsterComponent != null)
        {
            monsterComponent.spawner = this;
        }

        spawnedEnemiesCount++;
    }

    // Public method to be called by an enemy when it is destroyed
    public void OnEnemyDestroyed()
    {
        if (spawnedEnemiesCount > 0)
            spawnedEnemiesCount--;
    }
}
