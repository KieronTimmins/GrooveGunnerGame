using UnityEngine;
using UnityEngine.UI;

public class RotateImage : MonoBehaviour
{
    public float rotationSpeed = 20f; // Adjust the speed as needed

    void Update()
    {
        // Rotate the image continuously
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
