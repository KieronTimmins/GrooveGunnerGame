using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

using TMPro;

public class ToTheBeat : MonoBehaviour
{
    public float basePower = 40f; // Base power of the gun
    public float maxPower = 100f; // Maximum power the gun can reach
    public float powerIncreaseAmount = 20f; // Amount of power increased on correct timing
    public float timingThreshold = 0.2f; // Threshold for correct timing (in seconds)

    private float currentPower;
    private float songStartTime;
    public float beatsPerMinute ;
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
    public float comboGainer;
    public float decreaseRate = 1.0f;

     public TextMeshProUGUI Combo;
    public TextMeshProUGUI Score;
    public TextMeshProUGUI ComboPercent;
    public float tempoIncreaseRate = 0.05f ;
    public int comboscore;
    public int score;

    public float tempoChangeRate = 0.05f;

    public GameObject level1Object;
    public GameObject level2Object;
    public GameObject level3Object;

    public float currentPercentage;
    public int currentLevel = 1;
    public float rotationSpeed = 20f;

    // Index of the current shot sound
    //CameraMovement
    public Camera mainCamera;
    public float shakeDuration = 0.2f;
    public float shakeMagnitude = 0.2f;

    private Vector3 originalCameraPosition;
    public GameObject timeError;
    private float elapsedShakeDuration;





    private AudioSource audioSource;

    void Start()
    {
        currentPower = basePower;
        audioSource = gameObject.AddComponent<AudioSource>();
        currentTempo = InitialTempo;

        
        comboGainer = 0;
        

        level1Object.SetActive(true);
        level2Object.SetActive(false);
        level3Object.SetActive(false);

        audioSource.pitch = currentTempo / 60f;
        
        secondsPerBeat = 60f / beatsPerMinute;
        songStartTime = Time.time;

        // Start playing the first music clip

        level1Object.transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        if (mainCamera == null)
        {
            // Assuming the script is attached to the camera, set the camera as the mainCamera
            mainCamera = Camera.main;
        }

        originalCameraPosition = mainCamera.transform.position;






    }
    void StartScreenShake()
    {
        // Start the screen shake by setting the elapsed duration
        elapsedShakeDuration = shakeDuration;
    }
    void CheckPercentageThresholds()
    {
       

        // Example: Activate level 2 object when percentage reaches 40%
        if (comboGainer == 100f  && currentLevel == 1)
        {
            level1Object.SetActive(false);
            level2Object.SetActive(true);
            rotationSpeed =+ 30f;
            level2Object.transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
            ChangeTempo(tempoChangeRate);
            beatsPerMinute = beatsPerMinute * 1.01f;

            // Level change detected, reset percentage to 0
            int intValue = Mathf.RoundToInt(comboGainer);
            ResetPercentage();
        }
        else if (comboGainer == 100f  && currentLevel == 2)
        {
            level1Object.SetActive(false);
            level2Object.SetActive(false);
            level3Object.SetActive(true);
            rotationSpeed = +60f;
            ChangeTempo(tempoChangeRate);
            beatsPerMinute = beatsPerMinute * 1.02f;

            level3Object.transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
            // Level change detected, reset percentage to 0
            ResetPercentage();
            int intValue = Mathf.RoundToInt(comboGainer);
        }
    }
    void ResetPercentage()
    {
        
        // Reset the percentage to 0
        comboGainer = 0f;

        // Increase the current level
        currentLevel++;
        int intValue = Mathf.RoundToInt(comboGainer);

        Debug.Log("Level Change: Resetting Percentage to 0");
    }
    void PlayMusicClip(int index)
        {
            audioSource.pitch = 1.0f;
            if (index >= 0 && index < musicClips.Length)
            {
                


                // Assign the selected music clip to the AudioSource
                audioSource.clip = musicClips[index];

                // Play the music clip
                audioSource.Play();

                // Update the current music index
                currentMusicIndex = index;
            }
            else
            {
                Debug.LogError("Invalid music clip index: " + index);
            }
        }

    void ChangeTempo(float rate)
    {
        // Adjust the pitch to change tempo
        audioSource.pitch += rate;

        // Clamp the pitch to avoid extreme values
        audioSource.pitch = Mathf.Clamp(audioSource.pitch, 0.5f, 3.0f);
    }
    void DecreasePercentage()
    {
        // Calculate the decrease amount based on the decrease rate and time
        float decreaseAmount = decreaseRate * Time.deltaTime;

        // Decrease the current percentage
        
        comboGainer = Mathf.Clamp(comboGainer - decreaseAmount, 0f, 100f);
        int clampedResult = (int)comboGainer;


        int intValue = Mathf.RoundToInt(comboGainer);
        // Print or use the current percentage as needed

    }
    void Update()
    {
        if (elapsedShakeDuration > 0)
        {
            // Generate a random offset for the camera position
            Vector3 randomOffset = Random.insideUnitSphere * shakeMagnitude;

            // Apply the offset to the camera position
            mainCamera.transform.position = originalCameraPosition + randomOffset;

            // Decrease the remaining shake duration
            elapsedShakeDuration -= Time.deltaTime;
        }
        else
        {
            // Reset the camera position when the shake is complete
            mainCamera.transform.position = originalCameraPosition;
        }



        int intValue = Mathf.RoundToInt(comboGainer);
        DecreasePercentage();
        CheckPercentageThresholds();


        if (Input.GetKeyDown(KeyCode.Keypad1))

        {
            comboscore = 0;
            currentLevel = 0;
            level1Object.SetActive(true);
            level2Object.SetActive(false);
            level3Object.SetActive(false);
            ResetPercentage();
            PlayMusicClip(0);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))

        {
            comboscore = 0;
            currentLevel = 0;
            level1Object.SetActive(true);
            level2Object.SetActive(false);
            level3Object.SetActive(false);
            ResetPercentage();
            PlayMusicClip(1);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))

        {
            comboscore = 0;
            currentLevel = 0;
            level1Object.SetActive(true);
            level2Object.SetActive(false);
            level3Object.SetActive(false);
            ResetPercentage();
            PlayMusicClip(2);
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))

        {
            comboscore = 0;
            currentLevel = 0;
            level1Object.SetActive(true);
            level2Object.SetActive(false);
            level3Object.SetActive(false);
            ResetPercentage();
            PlayMusicClip(3);
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))

        {
            comboscore = 0;
            currentLevel = 0;
            level1Object.SetActive(true);
            level2Object.SetActive(false);
            level3Object.SetActive(false);
            ResetPercentage();
            PlayMusicClip(4);
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))

        {
            comboscore = 0;
            currentLevel = 0;
            level1Object.SetActive(true);
            level2Object.SetActive(false);
            level3Object.SetActive(false);
            ResetPercentage();
            PlayMusicClip(5);
        }

        {
            if (Input.GetMouseButtonDown(0)) // 0 represents the left mouse button
            {
                
                score += 25000;
                comboGainer -= 2;
                // Check the timing with the music beat (replace this with your music analysis logic)
                if (IsOnBeat())
                {
                    IncreasePower();
                    Debug.Log("upgrade");
                    
                    comboscore +=1;
                    score+=50000;
                    comboGainer += 3;
                }
                if(!IsOnBeat())
                {

                    timeError.SetActive(true);

                    StartScreenShake();
                    WaitForSecondsCoroutine();
                    timeError.SetActive(false);




                }

                // Play the next shot sound from the array
                PlayNextShotSound();

                // Fire the gun with the current power
                FireGun();
            }

          ScoreUI();
        }
        IEnumerator WaitForSecondsCoroutine()
        {
            Debug.Log("Coroutine started");

            // Wait for the specified duration
            yield return new WaitForSeconds(5f);

            // This code will be executed after the wait time
            Debug.Log("Coroutine finished after " + 5f + " seconds");
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
            int intValue = Mathf.RoundToInt(comboGainer);
            ComboPercent.text = intValue.ToString() + "%"  ;


        }


    }
}
