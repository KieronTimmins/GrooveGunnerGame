using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    public GameObject[] collectiblePrefabs;
    public int numberOfCollectibles = 6;
    public Vector3 spawnAreaMin;
    public Vector3 spawnAreaMax;
    private List<GameObject> spawnedCollectibles = new List<GameObject>();
        private int collectedThisRound = 0;
    private bool isRoundActive = false;

    private void Start()
    {
        StartCoroutine(SpawnCollectiblesRoutine());
    }

    IEnumerator SpawnCollectiblesRoutine()
    {
        while (true)
        {
            DeleteCollectibles();
            SpawnCollectibles();
            yield return new WaitForSeconds(30f);
        }
    }

    void SpawnCollectibles()
    {
        for (int i = 0; i < numberOfCollectibles; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y),
                Random.Range(spawnAreaMin.z, spawnAreaMax.z)
            );

            GameObject collectible = Instantiate(collectiblePrefabs[i % collectiblePrefabs.Length], randomPosition, Quaternion.identity);
            spawnedCollectibles.Add(collectible);
        }
    }

    void DeleteCollectibles()
    {
        foreach (GameObject collectible in spawnedCollectibles)
        {
            Destroy(collectible);
        }
        spawnedCollectibles.Clear();
    }

    private void OnDrawGizmos()
    {
        // Draw a debug square in the scene view
        Debug.DrawLine(spawnAreaMin, new Vector3(spawnAreaMax.x, spawnAreaMin.y, spawnAreaMin.z), Color.red);
        Debug.DrawLine(spawnAreaMin, new Vector3(spawnAreaMin.x, spawnAreaMin.y, spawnAreaMax.z), Color.red);
        Debug.DrawLine(new Vector3(spawnAreaMax.x, spawnAreaMin.y, spawnAreaMin.z), spawnAreaMax, Color.red);
        Debug.DrawLine(new Vector3(spawnAreaMin.x, spawnAreaMin.y, spawnAreaMax.z), spawnAreaMax, Color.red);
        Debug.DrawLine(spawnAreaMin, new Vector3(spawnAreaMin.x, spawnAreaMax.y, spawnAreaMin.z), Color.red);
        Debug.DrawLine(new Vector3(spawnAreaMax.x, spawnAreaMin.y, spawnAreaMin.z), new Vector3(spawnAreaMax.x, spawnAreaMax.y, spawnAreaMin.z), Color.red);
        Debug.DrawLine(new Vector3(spawnAreaMin.x, spawnAreaMin.y, spawnAreaMax.z), new Vector3(spawnAreaMin.x, spawnAreaMax.y, spawnAreaMax.z), Color.red);
        Debug.DrawLine(spawnAreaMax, new Vector3(spawnAreaMin.x, spawnAreaMax.y, spawnAreaMax.z), Color.red);
    }
}
