using UnityEngine;
using UnityEngine.UI;

public class ActivateButtons : MonoBehaviour
{
    public Button[] buttonsToToggle;

    void Start()
    {
        // Ensure the array is not null
        if (buttonsToToggle == null)
        {
            Debug.LogError("Buttons array is not assigned!");
            return;
        }

        // Deactivate all buttons at the start
        DeactivateAllButtons();
    }

    public void OnButtonClick()
    {
        // Toggle the activation state of all buttons in the array
        ToggleAllButtons();
    }

    public void DeactivateAll()
    {
        // Deactivate all buttons in the array
        DeactivateAllButtons();
    }

    void ToggleAllButtons()
    {
        foreach (Button button in buttonsToToggle)
        {
            button.gameObject.SetActive(!button.gameObject.activeSelf);
        }
    }

    void DeactivateAllButtons()
    {
        foreach (Button button in buttonsToToggle)
        {
            button.gameObject.SetActive(false);
        }
    }
}

