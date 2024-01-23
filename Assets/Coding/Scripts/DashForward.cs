using System.Collections;
using UnityEngine;

public class DashForward : MonoBehaviour
{
    public float dashDistance = 5f;
    public float dashDuration = 0.5f;
    public KeyCode dashKey = KeyCode.F;
    public AudioClip[] dashAudioClips;  // Use an array to store multiple audio clips
    public AudioSource audioSource;

    private bool isDashing;

    void Awake()
    {
        // Preload the first audio clip.
        if (audioSource != null && dashAudioClips != null && dashAudioClips.Length > 0)
        {
            audioSource.clip = dashAudioClips[0];
        }
    }

    void Update()
    {
        // Check for dash input.
        if (Input.GetKeyDown(dashKey) && !isDashing)
        {
            // Toggle between audio clips.
            ToggleAudioClip();

            // Play dash audio cue immediately.
            if (audioSource != null && audioSource.clip != null)
            {
                audioSource.Play();
            }

            StartCoroutine(Dash());
        }
    }

    void ToggleAudioClip()
    {
        // Toggle between audio clips.
        if (dashAudioClips != null && dashAudioClips.Length > 1)
        {
            int currentIndex = System.Array.IndexOf(dashAudioClips, audioSource.clip);
            int nextIndex = (currentIndex + 1) % dashAudioClips.Length;
            audioSource.clip = dashAudioClips[nextIndex];
        }
    }

    IEnumerator Dash()
    {
        // Set isDashing to true.
        isDashing = true;

        // Save the current position.
        Vector3 originalPosition = transform.position;

        // Calculate the target position for the dash.
        Vector3 targetPosition = transform.position + transform.forward * dashDistance;

        // Use Lerp to smoothly move towards the target position over dashDuration.
        float elapsedTime = 0f;
        while (elapsedTime < dashDuration)
        {
            transform.position = Vector3.Lerp(originalPosition, targetPosition, elapsedTime / dashDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final position is exactly the target position.
        transform.position = targetPosition;

        // Set isDashing to false.
        isDashing = false;
    }
}
