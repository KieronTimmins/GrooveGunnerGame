using UnityEngine;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth; // Reference to the PlayerHealth script
    public GameObject gameUI; // Reference to the main game UI
    public GameObject gameOverUI; // Reference to the game over UI
    public TMP_Text finalScoreText;
    public TMP_Text enemiesKilledText;
    public TMP_Text harmonyTokensText;

    public AudioSource music;

    private bool isGameOver = false;

    void Start()
    {
        if (playerHealth == null)
        {
            Debug.LogError("PlayerHealth script not assigned to GameOverManager.");
        }

        if (gameUI == null || gameOverUI == null || finalScoreText == null || enemiesKilledText == null || harmonyTokensText == null)
        {
            Debug.LogError("One or more UI elements not assigned to GameOverManager.");
        }

        if (music == null)
        {
            Debug.LogError("AudioSource not assigned to GameOverManager for music.");
        }

        // Ensure the game over UI is initially inactive
        gameOverUI.SetActive(false);
    }

    void Update()
    {
        if (!isGameOver && playerHealth != null && playerHealth.currentHealth <= 0)
        {
            HandleGameOver();
        }
    }

    void HandleGameOver()
    {
        isGameOver = true;

        // Pause the game
        Time.timeScale = 0f;

        // Deactivate the main game UI
        gameUI.SetActive(false);

        // Activate the game over UI
        gameOverUI.SetActive(true);

        // Play game over music
        if (music != null)
        {
            music.Play();
        }

        // Calculate harmony tokens
        int finalScore = CalculateFinalScore();
        int harmonyTokens = Mathf.CeilToInt(finalScore / 10000f);

        // Display the final score, enemies killed, and harmony tokens earned
        finalScoreText.text = "Final Score: " + finalScore;
        enemiesKilledText.text = "Enemies Killed: " + PlayerProfile.Instance.EnemiesKilled; // Assuming you have a PlayerProfile script
        harmonyTokensText.text = "Harmony Tokens Earned: " + harmonyTokens;

        // Add harmony tokens to the current player profile
        PlayerProfile.Instance.AddCurrency(harmonyTokens);
    }

    int CalculateFinalScore()
    {
        // Replace this with your own logic to calculate the final score
        // For example, you might want to add up points based on player performance.
        // This is just a placeholder.
        return 10000;
    }
}
