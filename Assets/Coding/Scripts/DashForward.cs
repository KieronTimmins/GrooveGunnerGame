using UnityEngine;
using TMPro; // Import TextMeshPro namespace
using System.Collections;

public class DashForward : MonoBehaviour
{
    public float dashDistance = 5f;
    public float dashDuration = 0.5f;
    public float dashCooldown = 5f;
    public KeyCode dashKey = KeyCode.F;
    public AudioClip[] dashAudioClips;
    public AudioSource audioSource;
    public TMP_Text cooldownText; // Use TMP_Text for UI text element

    private bool isDashing;
    private bool isCooldown;
    private float cooldownTimer;

    public float Delay = 0.3f;
 

    void Awake()
    {
        if (audioSource != null && dashAudioClips != null && dashAudioClips.Length > 0)
        {
            audioSource.clip = dashAudioClips[0];
        }
    }
  
    void Update()
    {
        if (Input.GetKeyDown(dashKey) && !isDashing && !isCooldown)
        {
            ToggleAudioClip();
            

            if (audioSource != null && audioSource.clip != null)
            {
                audioSource.Play();
            }

            StartCoroutine(Dash());
            StartCooldown();
        }

        // Update cooldown timer and UI text
        if (isCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0)
            {
                isCooldown = false;
                cooldownText.text = ""; // Clear UI text when cooldown ends
            }
            else
            {
                cooldownText.text = "Cooldown: " + Mathf.Round(cooldownTimer).ToString(); // Update UI text
            }
        }
    }

    void ToggleAudioClip()
    {
        if (dashAudioClips != null && dashAudioClips.Length > 1)
        {
            int currentIndex = System.Array.IndexOf(dashAudioClips, audioSource.clip);
            int nextIndex = (currentIndex + 1) % dashAudioClips.Length;
            audioSource.clip = dashAudioClips[nextIndex];
        }
    }

    void StartCooldown()
    {
        isCooldown = true;
        cooldownTimer = dashCooldown;
    }

    IEnumerator Dash()
    {
        isDashing = true;

        Vector3 originalPosition = transform.position;
        Vector3 targetPosition = transform.position + transform.forward * dashDistance;

        float elapsedTime = 0f;
        while (elapsedTime < dashDuration)
        {
            transform.position = Vector3.Lerp(originalPosition, targetPosition, elapsedTime / dashDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        isDashing = false;
    }
}
