using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth = 100;

    public Image healthCircle;  // Reference to the UI Image representing the circle
    public GameObject splatterImage; // Reference to the GameObject containing the "splatter" Image

    private RectTransform healthRectTransform; // Reference to the health circle's RectTransform
    private float initialHealthSize; // Initial size of the health circle

    void Start()
    {
        if (healthCircle == null || splatterImage == null)
        {
            Debug.LogError("Health circle or splatter image reference not set in the inspector!");
            return;
        }

        // Get the RectTransform component
        healthRectTransform = healthCircle.rectTransform;

        // Initialize currentHealth to be equal to maxHealth
        currentHealth = maxHealth;

        // Store the initial size of the health circle
        initialHealthSize = healthRectTransform.sizeDelta.x;

        UpdateHealthCircle(); // Call this to update the health circle initially
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Subtract 10 from health when colliding with an enemy
            currentHealth -= 10;

            Debug.Log("Player collided with an enemy. Current health: " + currentHealth);

            // You can add additional logic here, such as checking if the player has died.
        }
    }

    void UpdateHealthCircle()
    {
        // Calculate the new size of the health circle based on the player's health
        float newHealthSize = initialHealthSize * (currentHealth / maxHealth);

        // Set the sizeDelta of the health circle's RectTransform to adjust its size
        healthRectTransform.sizeDelta = new Vector2(newHealthSize, newHealthSize);
    }

    void Update()
    {
        UpdateHealthCircle();

        // Activate/deactivate the splatter image based on the player's health
        splatterImage.SetActive(currentHealth <= 30);
    }
}
