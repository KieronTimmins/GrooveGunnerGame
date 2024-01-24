using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth = 100;
    public float healthRegenerationRate = 5; // Adjust this rate as needed

    public Image healthCircle;
    public GameObject splatterImage;

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
    }

    void Update()
    {
        if (currentHealth < maxHealth)
        {
            // Gradually increase health over time
            currentHealth += healthRegenerationRate * Time.deltaTime;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

            UpdateHealthCircle();
        }

        splatterImage.SetActive(currentHealth <= 30);
    }
}
