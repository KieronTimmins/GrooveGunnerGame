using UnityEngine;

public class WeaponWheel : MonoBehaviour
{
    public int numberOfSlots = 6;
    public float wheelRadius = 100f;
    public GameObject[] weaponSlots; // Array to hold your weapon prefabs or references

    private int currentSlot = 0;

    void Start()
    {
        // Initialize the weapon wheel
        InitializeWeaponWheel();
    }

    void Update()
    {
        // Check for input to rotate the weapon wheel
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scrollInput) > 0.1f)
        {
            RotateWeaponWheel(-Mathf.Sign(scrollInput));
        }

        // Check for weapon selection input
        for (int i = 0; i < numberOfSlots; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SelectWeapon(i);
                break;  // Exit the loop after the first key press
            }
        }

        // You can add more keys or input methods as needed
    }

    void InitializeWeaponWheel()
    {
        // Instantiate or reference your weapon prefabs in the weaponSlots array
        weaponSlots = new GameObject[numberOfSlots];

        for (int i = 0; i < numberOfSlots; i++)
        {
            // Instantiate or assign your weapon prefabs
            // For simplicity, I'm using spheres as placeholders
            GameObject weaponPrefab = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            weaponPrefab.name = "Weapon " + (i + 1);
            weaponSlots[i] = weaponPrefab;
            weaponPrefab.SetActive(false);
        }

        // Set the first weapon active by default
        weaponSlots[currentSlot].SetActive(true);
    }

    void RotateWeaponWheel(float direction)
    {
        currentSlot = (currentSlot + Mathf.RoundToInt(direction)) % numberOfSlots;

        if (currentSlot < 0)
        {
            currentSlot = numberOfSlots - 1;
        }

        UpdateWeaponWheel();
    }

    void SelectWeapon(int slot)
    {
        if (slot >= 0 && slot < numberOfSlots)
        {
            currentSlot = slot;
            UpdateWeaponWheel();
        }
    }

    void UpdateWeaponWheel()
    {
        // Set all weapons inactive
        foreach (GameObject weapon in weaponSlots)
        {
            weapon.SetActive(false);
        }

        // Set the selected weapon active
        weaponSlots[currentSlot].SetActive(true);
    }
}
