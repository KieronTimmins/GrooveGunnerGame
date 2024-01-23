using System.Collections;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    public GameObject rocketPrefab; // Reference to the rocket prefab
    public Transform firePoint; // Reference to the transform point where the rocket is fired from
    public AudioClip fireSound; // Sound played when firing
    public float rocketSpeed = 10f; // Speed of the rocket
    public float recoilForce = 5f; // Force applied to push the player back
    public float cooldownTime = 1f; // Time between rocket launches

    private bool canFire = true;
    private float lastFireTime;

    void Update()
    {
        // Check for input to fire the rocket (e.g., Left Mouse Button)
        if (Input.GetMouseButtonDown(0) && canFire)
        {
            FireRocket();
        }
    }

    void FireRocket()
    {
        if (rocketPrefab != null && firePoint != null)
        {
            // Check if enough time has passed since the last rocket was fired
            if (Time.time - lastFireTime > cooldownTime)
            {
                // Play the firing sound
                if (fireSound != null)
                {
                    AudioSource.PlayClipAtPoint(fireSound, firePoint.position);
                }

                // Instantiate the rocket prefab at the fire point
                GameObject rocket = Instantiate(rocketPrefab, firePoint.position, firePoint.rotation);

                // Access the rocket's Rigidbody component
                Rigidbody rocketRb = rocket.GetComponent<Rigidbody>();

                // Apply force to the rocket
                rocketRb.velocity = rocket.transform.forward * rocketSpeed;

                // Apply recoil force to push the player back
                ApplyRecoilForce();

                // Update the last fire time
                lastFireTime = Time.time;
            }
        }
        else
        {
            Debug.LogError("RocketPrefab or FirePoint not assigned to RocketLauncher script.");
        }
    }

    void ApplyRecoilForce()
    {
        // Calculate the recoil force direction (opposite to the forward direction of the rocket launcher)
        Vector3 recoilForceDirection = -transform.forward;

        // Apply the recoil force to push the player back
        GetComponent<Rigidbody>().AddForce(recoilForceDirection * recoilForce, ForceMode.Impulse);
    }
}
