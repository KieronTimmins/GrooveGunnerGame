using UnityEngine;

public class ToggleCanvas : MonoBehaviour
{
    public Canvas uiCanvas; // Reference to the UI canvas

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ToggleUI();
        }
    }

    void ToggleUI()
    {
        // Check if the UI canvas is not null
        if (uiCanvas != null)
        {
            // Toggle the visibility of the UI canvas
            uiCanvas.gameObject.SetActive(!uiCanvas.gameObject.activeSelf);
        }
        else
        {
            Debug.LogError("UI Canvas not assigned to the ToggleCanvas script.");
        }
    }
}
