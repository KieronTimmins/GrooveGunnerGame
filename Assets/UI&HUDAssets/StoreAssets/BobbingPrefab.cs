using UnityEngine;

public class BobbingPrefab : MonoBehaviour
{
    public float bobbingHeight = 1.0f; // Set the height of the bobbing motion
    public float bobbingSpeed = 1.0f; // Set the speed of the bobbing motion

    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        // Calculate the new Y position based on sine function to create a bobbing motion
        float newY = originalPosition.y + Mathf.Sin(Time.time * bobbingSpeed) * bobbingHeight;

        // Update the position of the prefab
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
