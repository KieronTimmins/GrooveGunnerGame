using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToTheBeat : MonoBehaviour
{
    public float basePower = 10f; // Base power of the gun
    public float maxPower = 100f; // Maximum power the gun can reach
    public float powerIncreaseAmount = 20f; // Amount of power increased on correct timing
    public float timingThreshold = 0.2f; // Threshold for correct timing (in seconds)

    private float currentPower;
    private float songStartTime;
    public float beatsPerMinute = 94f;
    private float secondsPerBeat;
    public GameObject bulletPrefab; // Reference to the bullet prefab
    public Transform firePoint;
    public AudioClip[] musicClips;



    private AudioSource audioSource;

    void Start()
    {
        currentPower = basePower;
        audioSource = gameObject.AddComponent<AudioSource>();
         
        audioSource.playOnAwake = false;
        audioSource.Play();

        
        secondsPerBeat = 60f / beatsPerMinute;
        songStartTime = Time.time;
        if (musicClips.Length > 0)
        {
            audioSource.clip = musicClips[Random.Range(0, musicClips.Length)];
            audioSource.Play(); 
        }
        else
        {
            Debug.LogError("No music clips assigned to the array.");
        }




    }

    void Update()
    {
        
        // Check for input to fire the gun (e.g., Space key)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Check the timing with the music beat (replace this with your music analysis logic)
            if (IsOnBeat())
            {
                IncreasePower();
                Debug.Log("upgrade");
            }

            // Fire the gun with the current power
            FireGun();
        }
    }

    void IncreasePower()
    {
        // Increase the power of the gun, but ensure it doesn't exceed the maximum
        currentPower = Mathf.Min(currentPower + powerIncreaseAmount, maxPower);
    }

    void FireGun()
    {
        // Implement your gun firing logic here using the current power
        Debug.Log("Firing Gun with Power: " + currentPower);

        // Reset the power for the next shot
        currentPower = basePower;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Access the bullet's Rigidbody component
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        // Apply a force to the bullet (customize this based on your game)
        float bulletForce = currentPower; // Use the current power as the force
        bulletRb.AddForce(firePoint.up * bulletForce, ForceMode.Impulse);

        // Optional: Play gun firing sound or add visual effects

        // Reset the power for the next shot
        currentPower = basePower;
    }

    bool IsOnBeat()
    {


        
        float songElapsedTime = Time.time - songStartTime;
        Debug.Log(songElapsedTime);

        // Calculate the current beat index based on the elapsed time
        int currentBeatIndex = Mathf.FloorToInt(songElapsedTime / secondsPerBeat);
        Debug.Log(currentBeatIndex);
        // Calculate the exact time of the current beat
        float expectedBeatTime = currentBeatIndex * secondsPerBeat;
        Debug.Log(expectedBeatTime);

        // Check if the current time is close enough to the expected beat time
        return Mathf.Abs(songElapsedTime - expectedBeatTime) < timingThreshold;
    }
}
