using UnityEngine;

public class BobbingMotion : MonoBehaviour
{
    public float bobbingSpeed = 1.0f; // Adjust the speed of the bobbing motion
    public float bobbingAmount = 0.1f; // Adjust the amount of bobbing motion

    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        // Calculate the vertical offset based on sine function
        float yOffset = Mathf.Sin(Time.time * bobbingSpeed) * bobbingAmount;

        // Apply the offset to the object's position
        transform.position = originalPosition + new Vector3(0f, yOffset, 0f);
    }
}
