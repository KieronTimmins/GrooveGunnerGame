using UnityEngine;

public class PauseScript : MonoBehaviour
{
    public GameObject pausePanel;
    public Camera firstPersonCamera; // Reference to your first-person camera

    private bool isPaused = false;

    void Start()
    {
        if (pausePanel == null)
        {
            Debug.LogError("Pause panel reference not set in the inspector!");
            return;
        }

        if (firstPersonCamera == null)
        {
            Debug.LogError("First-person camera reference not set in the inspector!");
            return;
        }

        // Disable the pause panel initially
        pausePanel.SetActive(false);
    }

    void Update()
    {
        // Check for the "P" key to toggle pause
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;

        // Enable or disable the camera component based on the pause state
        if (firstPersonCamera != null)
        {
            firstPersonCamera.GetComponent<FirstPersonLook>().enabled = !isPaused;
        }

        // Your additional code for toggling the pause panel visibility
        pausePanel.SetActive(isPaused);

        if (isPaused)
        {
            Debug.Log("Game paused");
            Time.timeScale = 0;  // Set time scale to 0 to freeze the game
        }
        else
        {
            Debug.Log("Game unpaused");
            Time.timeScale = 1;  // Set time scale to 1 to resume normal time flow
        }

        // If the game is unpaused, hide the pause panel after a short delay
        if (!isPaused)
        {
            Invoke("HidePausePanel", 0.1f);
        }
    }

    void HidePausePanel()
    {
        pausePanel.SetActive(false);
    }
}
