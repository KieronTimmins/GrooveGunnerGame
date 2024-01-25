using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform[] destinations;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TeleportPlayer(other.transform);
        }
    }

    private void TeleportPlayer(Transform objectToTeleport)
    {
        if (destinations != null && destinations.Length > 0)
        {
            // Select a random destination from the array
            Transform randomDestination = destinations[Random.Range(0, destinations.Length)];
            objectToTeleport.position = randomDestination.position;
        }
        else
        {
            Debug.LogError("Teleport destinations array is not set or is empty!");
        }
    }
}