using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToTheBeat : MonoBehaviour
{
    public float basePower = 40f; // Base power of the gun
    public float maxPower = 100f; // Maximum power the gun can reach
    public float powerIncreaseAmount = 20f; // Amount of power increased on correct timing
    public float timingThreshold = 0.2f; // Threshold for correct timing (in seconds)

    private float currentPower;
    private float songStartTime;
    public float beatsPerMinute = 88f;
    private float secondsPerBeat;
    public GameObject bulletPrefab; // Reference to the bullet prefab
    public Transform firePoint;
    public AudioClip[] musicClips;
    public AudioClip[] shotSounds; // Array of shot sounds
    private int currentShotIndex = 0; 
    private int currentMusicIndex = 0;
    
    private float currentTempo;
    public float InitialTempo ;
    public int scoreToIncreaseTempo = 1;

    public Text Combo;
    public Text Score;
    public float tempoIncreaseRate ;
    public int comboscore;
    public int score;
    // Index of the current shot sound




    private AudioSource audioSource;

    void Start()
    {
        currentPower = basePower;
        audioSource = gameObject.AddComponent<AudioSource>();
        currentTempo = InitialTempo;
         
        
        
        
        

        audioSource.pitch = currentTempo / 60f;
        
        secondsPerBeat = 60f / beatsPerMinute;
        songStartTime = Time.time;
       
        




    }

    void Update()
    {
        
        
        {
            if (Input.GetMouseButtonDown(0)) // 0 represents the left mouse button
            {
                score+= 25000;
                // Check the timing with the music beat (replace this with your music analysis logic)
                if (IsOnBeat())
                {
                    IncreasePower();
                    Debug.Log("upgrade");
                    IncreaseTempo();
                    comboscore +=1;
                    score+=50000;
                }

                // Play the next shot sound from the array
                PlayNextShotSound();

                // Fire the gun with the current power
                FireGun();
            }

          ScoreUI();
        }

        void IncreasePower()
        {
            // Increase the power of the gun, but ensure it doesn't exceed the maximum
            currentPower = Mathf.Min(currentPower + powerIncreaseAmount, maxPower);
        }

        void FireGun()
        {
            // Instantiate the bullet at the firePoint position and rotation
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            // Access the bullet's Rigidbody component
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

            // Determine the forward direction (assuming firePoint is aligned with the player's view)
            Vector3 fireDirection = firePoint.forward;

            // Calculate the force to apply to the bullet
            float bulletForce = currentPower; // Use the current power as the force

            // Apply force in the forward direction
            bulletRb.AddForce(fireDirection * bulletForce, ForceMode.Impulse);

            // Reset the power for the next shot
            currentPower = basePower;

            // Optional: Play gun firing sound or add visual effects
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


        void IncreaseTempo()
        {

            currentTempo += tempoIncreaseRate * Time.deltaTime;

            audioSource.pitch = currentTempo / 60f;

            Debug.Log("working");


        }
      


        void PlayNextShotSound()
        {
            if (shotSounds.Length > 0)
            {
                // Play the next shot sound in the array
                audioSource.PlayOneShot(shotSounds[currentShotIndex]);

                // Update the index for the next shot sound
                currentShotIndex = (currentShotIndex + 1) % shotSounds.Length;
            }
            else
            {
                Debug.LogError("No shot sounds assigned to the array.");
            }
        }

        void ScoreUI()
        {
            Combo.text = "X" + comboscore;
            Score.text = "Score: " + score;


        }

    }
}
