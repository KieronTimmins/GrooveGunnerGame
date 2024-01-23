using System.Collections;
using UnityEngine;

public class TommyGun : MonoBehaviour
{
    public GameObject bulletPrefab; // Reference to the bullet prefab
    public Transform firePoint; // Reference to the transform point where bullets are fired from
    public AudioClip shootSound; // Sound played when shooting
    public float bulletForce = 10f; // Force applied to the bullets
    public float recoilForce = 2f; // Force applied for recoil
    public float recoilSpeed = 5f; // Speed of the recoil motion
    public float maxRecoilAngle = 10f; // Maximum angle of recoil
    public float cooldownTime = 0.1f; // Cooldown time between shots

    private bool isShooting = false;
    private float currentRecoilAngle = 0f;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Shoot());
        }

        if (Input.GetMouseButton(0) && !isShooting)
        {
            StartCoroutine(ContinuousShoot());
        }
    }

    IEnumerator Shoot()
    {
        if (bulletPrefab != null && firePoint != null && !isShooting)
        {
            isShooting = true;

            // Play the shooting sound continuously
            audioSource.clip = shootSound;
            audioSource.loop = true;
            audioSource.Play();

            // Instantiate the bullet prefab at the fire point
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            // Access the bullet's Rigidbody component
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

            // Apply force to the bullet
            bulletRb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);

            // Apply recoil force
            ApplyRecoilForce();

            // Wait for cooldown time
            yield return new WaitForSeconds(cooldownTime);

            isShooting = false;

            // Stop playing the shooting sound
            audioSource.Stop();
            audioSource.loop = false;
        }
    }

    IEnumerator ContinuousShoot()
    {
        while (Input.GetMouseButton(0))
        {
            StartCoroutine(Shoot());
            yield return null;
        }
    }

    void ApplyRecoilForce()
    {
        // Calculate the recoil force direction (opposite to the forward direction of the Tommy Gun)
        Vector3 recoilForceDirection = -transform.forward;

        // Apply the recoil force to push the Tommy Gun back
        GetComponent<Rigidbody>().AddForce(recoilForceDirection * recoilForce, ForceMode.Impulse);

        // Apply the recoil motion by rotating the Tommy Gun
        StartCoroutine(RecoilMotion());
    }

    IEnumerator RecoilMotion()
    {
        float targetAngle = currentRecoilAngle + maxRecoilAngle;

        while (currentRecoilAngle < targetAngle)
        {
            // Rotate the Tommy Gun back and forth for recoil effect
            transform.Rotate(Vector3.right, recoilSpeed * Time.deltaTime);

            currentRecoilAngle += recoilSpeed * Time.deltaTime;

            yield return null;
        }

        // Reset the recoil angle
        currentRecoilAngle = 0f;
    }
}
