using UnityEngine;

public class ShootingStar : MonoBehaviour
{
    public float speed = 5f; // Adjust as needed
    public float distance = 10f; // Adjust as needed
    public float lifetime = 2f; // Adjust as needed

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        Destroy(gameObject, lifetime); // Destroy the shooting star after a certain lifetime
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        // Calculate the new position based on oscillation
        float oscillation = Mathf.Sin(Time.time * speed) * distance;
        Vector3 newPosition = startPosition + new Vector3(oscillation, 0f, 0f);

        // Move the shooting star
        transform.position = newPosition;
    }
}
