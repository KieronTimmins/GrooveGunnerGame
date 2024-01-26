using UnityEngine;
using UnityEngine.UI;

public class TextControl : MonoBehaviour
{
    public Text hintText;
    public GameObject startText;

    void Start()
    {
        // Ensure the UI text element is inactive when the scene starts
        hintText.gameObject.SetActive(false);
    }

    void Update()
    {
        // Check for player input
        if (Input.GetKeyDown(KeyCode.Alpha1) ||
            Input.GetKeyDown(KeyCode.Alpha2) ||
            Input.GetKeyDown(KeyCode.Alpha3) ||
            Input.GetKeyDown(KeyCode.Alpha4) ||
            Input.GetKeyDown(KeyCode.Alpha5) ||
            Input.GetKeyDown(KeyCode.Alpha6))
        {
            // Hide the UI text element when any of the specified keys are pressed
            hintText.gameObject.SetActive(false);
        }
    }
}
