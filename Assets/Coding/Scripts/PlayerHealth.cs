using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth = 100;

    public Image healthCircle; // Reference to the UI Image representing the circle
    private float initialSize; // Initial size of the circle

    void Start()
    {
        if (healthCircle == null)
        {
            Debug.LogError("Health circle reference not set in the inspector!");
        }

        // Initialize currentHealth to be equal to maxHealth
        currentHealth = maxHealth;

        // Store the initial size of the circle
        initialSize = healthCircle.rectTransform.sizeDelta.x;

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
        // Calculate the fill amount based on the player's health
        float fillAmount = Mathf.Clamp01(currentHealth / maxHealth);

        // Update the fill amount of the health circle based on the player's health
        healthCircle.fillAmount = fillAmount;

        // Calculate the new size of the circle based on the fill amount
        float newSize = initialSize * fillAmount;

        // Set the sizeDelta of the RectTransform to adjust the size of the circle
        healthCircle.rectTransform.sizeDelta = new Vector2(newSize, newSize);
    }

    void Update()
    {
        UpdateHealthCircle();
    }
}
