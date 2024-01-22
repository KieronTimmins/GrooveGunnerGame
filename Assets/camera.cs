using UnityEngine;

public class FreeRoamCamera : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float rotationSpeed = 2f;

    void Update()
    {
        // Camera movement
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        // Calculate movement direction based on input
        Vector3 moveDirection = new Vector3(horizontalMovement, 0f, verticalMovement).normalized;

        // Transform the movement direction to be relative to the camera's orientation
        moveDirection = transform.TransformDirection(moveDirection);

        // Apply movement to the camera's position
        transform.Translate(moveDirection * movementSpeed * Time.deltaTime, Space.World);

        // Camera rotation (looking left and right)
        float mouseX = Input.GetAxis("Mouse X");
        Vector3 rotationAmount = new Vector3(0f, mouseX, 0f) * rotationSpeed;

        // Apply rotation to the camera
        transform.Rotate(rotationAmount);
    }
}
