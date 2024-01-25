using UnityEngine;
using TMPro;

public class ShopUpdateTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float countdownDuration = 3600; // 12 hours in seconds

    private static float startTime;
    private float remainingTime;

    // Singleton instance
    private static ShopUpdateTimer _instance;

    void Start()
    {
        // Check if an instance already exists
        if (_instance == null)
        {
            // If not, set the instance to this and don't destroy on load
            _instance = this;
            DontDestroyOnLoad(gameObject);

            // Initialize or retrieve remaining time
            if (startTime == 0f)
            {
                startTime = Time.time;
                remainingTime = countdownDuration;
            }
            else
            {
                // Calculate remaining time based on elapsed time
                float elapsedTime = Time.time - startTime;
                remainingTime = Mathf.Max(countdownDuration - elapsedTime, 0f);
            }

            UpdateTimerDisplay();
        }
        else
        {
            // If an instance already exists, destroy this one
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            UpdateTimerDisplay();
        }
        else
        {
            // Handle shop update event when the timer reaches zero
            HandleShopUpdate();
        }
    }

    void UpdateTimerDisplay()
    {
        // Format the remaining time into hours, minutes, and seconds
        
        int minutes = Mathf.FloorToInt((remainingTime % 3600) / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);

        // Update the text display
        timerText.text = "SHOP UPDATES IN: " + string.Format("{1:D2}:{2:D2}", minutes, seconds);
    }

    void HandleShopUpdate()
    {
        // Handle the shop update event when the timer reaches zero
        Debug.Log("Shop Updated!");

        // Reset start time and remaining time for the next update
        startTime = Time.time;
        remainingTime = countdownDuration;
        UpdateTimerDisplay();
    }
}
