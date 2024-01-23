using UnityEngine;
using UnityEngine.UI;

public class WeaponWheelUI : MonoBehaviour
{
    public Button[] weaponButtons; // Reference to UI buttons representing weapon slots
    private int currentSlot = 0;

    void Start()
    {
        // Set up button click listeners
        for (int i = 0; i < weaponButtons.Length; i++)
        {
            int index = i; // Capture the current value of i
            weaponButtons[i].onClick.AddListener(() => SelectWeapon(index));
        }

        // Set the first weapon active by default
        UpdateWeaponWheel();
    }

    void Update()
    {
        // Add any additional input or update logic as needed
    }

    void SelectWeapon(int slot)
    {
        currentSlot = slot;
        UpdateWeaponWheel();
    }

    void UpdateWeaponWheel()
    {
        // Set all weapons inactive
        for (int i = 0; i < weaponButtons.Length; i++)
        {
            // Deactivate or hide the weapon based on your implementation
            // For simplicity, we're just changing the color of the button
            ColorBlock colors = weaponButtons[i].colors;
            colors.normalColor = (i == currentSlot) ? Color.green : Color.white;
            weaponButtons[i].colors = colors;
        }

        // Activate the selected weapon
        // Implement logic to activate the corresponding weapon based on currentSlot
    }
}
