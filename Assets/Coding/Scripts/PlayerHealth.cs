using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public float currentHealth = 0;
    public int healthRegenerationRate = 5; // Adjust this rate as needed

    public Image healthCircle;
    public GameObject splatterImage;
    public TextMeshProUGUI healthText; // Reference to the TextMeshPro element for displaying health
    private RectTransform healthRectTransform;
    private float initialHealthSize;

    void Start()
    {
        if (healthCircle == null || splatterImage == null)
        {
            Debug.LogError("Health circle or splatter image reference not set in the inspector!");
            return;
        }

        healthRectTransform = healthCircle.rectTransform;
        currentHealth = maxHealth;
        initialHealthSize = healthRectTransform.sizeDelta.x;

        UpdateHealthCircle();
        UpdateHealthText(); 
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            currentHealth -= 10;
            Debug.Log("Player collided with an enemy. Current health: " + currentHealth);
        }
    }

    void UpdateHealthCircle()
    {
        float newHealthSize = initialHealthSize * (currentHealth / maxHealth);
        healthRectTransform.sizeDelta = new Vector2(newHealthSize, newHealthSize);
        healthText.text = currentHealth.ToString();

        // Change the color of the health circle based on the player's health
        Color healthColor;
        if (currentHealth < 25)
        {
            healthColor = Color.red;
        }
        else if (currentHealth < 45)
        {
            healthColor = new Color(1.0f, 0.5f, 0.0f); // Orange
        }
        else
        {
            healthColor = Color.green;
        }
        healthCircle.color = healthColor;
    }

    void UpdateHealthText()
    {
        // Update the TextMeshPro element with the current health value


        int intValue = Mathf.RoundToInt(currentHealth);
        healthText.text = intValue.ToString();
        // Remove the "Health:" prefix

        // Change the color of the health text based on the player's health
        Color textColor;
        if (currentHealth < 25)
        {
          
            textColor = Color.red;
        }
        else if (currentHealth < 45)
        {
            textColor = new Color(1.0f, 0.5f, 0.0f);
           // Orange
        }
        else
        {
            textColor = Color.green;
            
        }
        healthText.color = textColor;
       

        // Optionally, set the anchored position and size manually
        healthText.rectTransform.anchoredPosition = new Vector2(0f, 0f); // Adjust as needed
        healthText.rectTransform.sizeDelta = new Vector2(172, 38); // Adjust as needed
       
        
    }

    void Update()
    {
        
        UpdateHealthCircle();
        UpdateHealthText();

        if (currentHealth < maxHealth)
        {
            // Gradually increase health over time
            currentHealth += healthRegenerationRate * Time.deltaTime;
            

            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            

            UpdateHealthCircle();
            UpdateHealthText();
            int intValue = Mathf.RoundToInt(currentHealth);

        }

        splatterImage.SetActive(currentHealth <= 30);

        if (currentHealth <= 0)
        {
            HandlePlayerDeath();
        }

    }

    private void HandlePlayerDeath()
    {
        // Load the end game scene
        SceneManager.LoadScene("end game");
    }
}
