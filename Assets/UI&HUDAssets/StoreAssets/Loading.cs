using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public TextMeshProUGUI loadingText;
    public Button playButton;
    public float loadingDuration = 3f;
    public float animationSpeed = 0.5f;

    private int dotCount = 1;
    private float timer;

    void Start()
    {
        if (loadingText == null || playButton == null)
        {
            Debug.LogError("TextMeshPro or Button component not assigned!");
            return;
        }

        timer = animationSpeed;
        loadingText.text = "LOADING";
        playButton.gameObject.SetActive(false);

        // Start the loading animation
        InvokeRepeating("UpdateLoadingText", 0f, animationSpeed);
        // Schedule the button activation after loadingDuration seconds
        Invoke("ActivateButton", loadingDuration);
    }

    void UpdateLoadingText()
    {
        // Update the loading text with the appropriate number of dots
        loadingText.text = "LOADING" + GetLoadingText();

        // Cycle through dot count (1, 2, 3)
        dotCount = (dotCount % 3) + 1;
    }

    void ActivateButton()
    {
        // Disable loading text and enable the play button
        loadingText.gameObject.SetActive(false);
        playButton.gameObject.SetActive(true);
    }

    string GetLoadingText()
    {
        // Return a string with the current number of dots
        return new string('.', dotCount);
    }
}
