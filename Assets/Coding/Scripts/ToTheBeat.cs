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
    public float score;

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
    public float shakeDuration = 0.8f;
    public float shakeMagnitude = 0.8f;

    private Vector3 originalCameraPosition;
    public GameObject timeError;
    private float elapsedShakeDuration;

    public float comboDisplayTime = 0.5f; 

    private float comboTimer;
    public GameObject SuperBoost;

    
    private AudioSource audioSource;
    public GameObject epicCombo;
    public GameObject KeepItUp;
    public GameObject MegaCombo;
    public GameObject OnFire;
    public GameObject Legendary;

    public TextMeshProUGUI textMeshProShake;
    public TextMeshProUGUI textMeshProShake2;
    public TextMeshProUGUI textMeshProShake3;
    public TextMeshProUGUI textMeshProShake4;
    public TextMeshProUGUI textMeshProShake5;
    private Vector3 originalPosition;

    public AudioClip epicComboAudio;
    public AudioClip KeepItUpAudio;
    public AudioClip MegaComboAudio;
    public AudioClip OnFireAudio;
    public AudioClip LegendaryAudio;

    private AudioSource audioSource2;

    public float deactivateDelay = 0.5f;

    public Transform cameraTransform;

    void Start()
    {
        comboscore = 1;
        currentPower = basePower;
        audioSource = gameObject.AddComponent<AudioSource>();
        currentTempo = InitialTempo;

       
        Legendary.SetActive(false);
        OnFire.SetActive(false);
        MegaCombo.SetActive(false);
        KeepItUp.SetActive(false);
        epicCombo.SetActive(false);


        level1Object.SetActive(true);
        level2Object.SetActive(false);
        level3Object.SetActive(false);

        audioSource.pitch = currentTempo / 60f;
        
        secondsPerBeat = 60f / beatsPerMinute;
        songStartTime = Time.time;

        // Start playing the first music clip

        level1Object.transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        
            // Assuming the script is attached to the camera, set the camera as the mainCamera
            originalCameraPosition = cameraTransform.localPosition;
        

        






    }
    IEnumerator DeactivateEffect()
    {

        yield return new WaitForSeconds(deactivateDelay);
        timeError.SetActive(false);

        


    }
    IEnumerator DeactivateEffectCombo()
    {

        yield return new WaitForSeconds(deactivateDelay);
        SuperBoost.SetActive(false);

        


    }


    IEnumerator ShakeCoroutine()
    {
        float elapsedShakeTime = 0f;

        while (elapsedShakeTime < shakeDuration)
        {
            // Calculate a random offset for the shake effect
            Vector3 shakeOffset = Random.insideUnitSphere * shakeMagnitude;

            // Apply the offset to the TextMeshPro object's position
            textMeshProShake.rectTransform.localPosition = originalPosition + shakeOffset;
            textMeshProShake2.rectTransform.localPosition = originalPosition + shakeOffset;
            textMeshProShake3.rectTransform.localPosition = originalPosition + shakeOffset;
            textMeshProShake4.rectTransform.localPosition = originalPosition + shakeOffset;
            textMeshProShake5.rectTransform.localPosition = originalPosition + shakeOffset;
            

            // Increment the elapsed time
            elapsedShakeTime += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

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
    void ShowCombo()
    {
        // Update the combo text
        Combo.text = "x" + comboscore;

        // Show the combo text
        Combo.enabled = true;

        // Set the timer for combo display
        comboTimer = comboDisplayTime;
        Debug.Log("working");
    }
    void Update()
    {
        audioSource2.pitch = 1.0f;
        if (comboTimer > 0f)
        {
            comboTimer -= Time.deltaTime;


            // Hide the combo text when the timer expires
            if (comboTimer <= 0f)
            {
                Combo.enabled = false;
            }
        }

        


       

        int intValue = Mathf.RoundToInt(comboGainer);
        DecreasePercentage();
        CheckPercentageThresholds();


        if (Input.GetKeyDown(KeyCode.Keypad1))

        {
            
            currentLevel = 0;
            level1Object.SetActive(true);
            level2Object.SetActive(false);
            level3Object.SetActive(false);
            ResetPercentage();
            PlayMusicClip(0);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))

        {
            
            currentLevel = 0;
            level1Object.SetActive(true);
            level2Object.SetActive(false);
            level3Object.SetActive(false);
            ResetPercentage();
            PlayMusicClip(1);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))

        {
            
            currentLevel = 0;
            level1Object.SetActive(true);
            level2Object.SetActive(false);
            level3Object.SetActive(false);
            ResetPercentage();
            PlayMusicClip(2);
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))

        {
            
            currentLevel = 0;
            level1Object.SetActive(true);
            level2Object.SetActive(false);
            level3Object.SetActive(false);
            ResetPercentage();
            PlayMusicClip(3);
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))

        {
            
            currentLevel = 0;
            level1Object.SetActive(true);
            level2Object.SetActive(false);
            level3Object.SetActive(false);
            ResetPercentage();
            PlayMusicClip(4);
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))

        {
            
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


                Legendary.SetActive(false);
                OnFire.SetActive(false);
                MegaCombo.SetActive(false);
                KeepItUp.SetActive(false);
                epicCombo.SetActive(false);
                comboGainer += 1;
                // Check the timing with the music beat (replace this with your music analysis logic)
                if (IsOnBeat())
                {
                    IncreasePower();
                    Debug.Log("upgrade");
                    ShowCombo();
                    comboscore +=1;
                    score+=1000;
                    comboGainer += 5;
                    
                }
                if(!IsOnBeat())
                {

                    timeError.SetActive(true);

                    
                    StartCoroutine(DeactivateEffect());

                    SuperBoost.SetActive(false);
                    comboscore = 1;


                }
                if(comboscore == 6)
                {
                    audioSource2.PlayOneShot(epicComboAudio, 4.5f);
                    SuperBoost.SetActive(true);
                    score = score * 1.2f;
                    StartCoroutine(DeactivateEffectCombo());
                    
                    epicCombo.SetActive(true);
                    StartCoroutine(ShakeCoroutine());

                }
                if (comboscore == 10)
                {
                    audioSource.pitch = 1.0f;
                    audioSource2.PlayOneShot(KeepItUpAudio, 4.5f);
                    SuperBoost.SetActive(true);
                    StartCoroutine(DeactivateEffectCombo());
                    score = score * 1.4f;
                    
                    
                    KeepItUp.SetActive(true);

                    StartCoroutine(ShakeCoroutine());

                }
                if (comboscore == 16)
                {

                    audioSource2.PlayOneShot(MegaComboAudio, 4.5f);
                    score = score * 1.6f;
                    SuperBoost.SetActive(true);
                    StartCoroutine(DeactivateEffectCombo());
                    

                   
                    MegaCombo.SetActive(true);
                    StartCoroutine(ShakeCoroutine());

                }
                if (comboscore == 26)
                {
                    audioSource2.PlayOneShot(OnFireAudio, 4.5f);
                    SuperBoost.SetActive(true);
                    score = score * 1.8f;
                    StartCoroutine(DeactivateEffectCombo());
                    
                    OnFire.SetActive(true);
                    StartCoroutine(ShakeCoroutine());


                }
                if (comboscore == 36)
                {
                    audioSource2.PlayOneShot(LegendaryAudio, 4.5f);
                    SuperBoost.SetActive(true);
                    score = score * 2;
                    StartCoroutine(DeactivateEffectCombo());

                    Legendary.SetActive(true);
                    StartCoroutine(ShakeCoroutine());

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
            
            
            timeError.SetActive(false);

            
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
            
           
            int intValue = Mathf.RoundToInt(comboGainer);
            


            ComboPercent.text = intValue.ToString() + "%";
            Score.text = "Score: " + score;

            // Show the combo text


            // Set the timer for combo display



        }


    }
}
