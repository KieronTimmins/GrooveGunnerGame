using System.Collections;
using UnityEngine;

public class DashForward : MonoBehaviour
{
    public float dashDistance = 5f;
    public float dashDuration = 0.5f;
    public float dashCooldown = 5f; // New variable for cooldown duration
    public KeyCode dashKey = KeyCode.F;
    public AudioClip[] dashAudioClips;
    public AudioSource audioSource;

    private bool isDashing;
    private bool isCooldown;
    private float cooldownTimer;

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

        // Update cooldown timer
        if (isCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0)
            {
                isCooldown = false;
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
            if (CheckForWallCollision())
            {
                break;
            }

            transform.position = Vector3.Lerp(originalPosition, targetPosition, elapsedTime / dashDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        isDashing = false;
    }

    bool CheckForWallCollision()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 0.5f))
        {
            if (hit.collider.CompareTag("Wall"))
            {
                StartCooldown(); // Start cooldown if a wall is hit
                return true;
            }
        }

        return false;
    }
}
