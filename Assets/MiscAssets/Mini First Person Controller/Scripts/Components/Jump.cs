using UnityEngine;

public class Jump : MonoBehaviour
{
    Rigidbody rigidbody;
    public float jumpStrength = 5;
    public float quickFallMultiplier = 5;  // Adjust this value for quicker descent
    public int maxJumps = 2;
    private int jumpsRemaining;
    public event System.Action Jumped;

    [SerializeField, Tooltip("Prevents jumping when the transform is in mid-air.")]
    GroundCheck groundCheck;

    public AudioClip[] jumpSoundClips;
    public AudioSource audioSource;

    private bool isFalling;  // Flag to track if the player is falling

    void Reset()
    {
        groundCheck = GetComponentInChildren<GroundCheck>();
    }

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        jumpsRemaining = maxJumps;
        isFalling = false;
    }

    void LateUpdate()
    {
        // Check for jump input and remaining jumps
        if (Input.GetButtonDown("Jump") && jumpsRemaining > 0)
        {
            // Jump when on the ground or during the first jump in the air
            if (!groundCheck || groundCheck.isGrounded || jumpsRemaining == maxJumps)
            {
                rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);
                rigidbody.AddForce(Vector3.up * 100 * jumpStrength);
                Jumped?.Invoke();

                // Play a random jump sound if available
                if (audioSource != null && jumpSoundClips != null && jumpSoundClips.Length > 0)
                {
                    int randomIndex = Random.Range(0, jumpSoundClips.Length);
                    audioSource.clip = jumpSoundClips[randomIndex];
                    audioSource.Play();
                }

                jumpsRemaining--;

                // Reset the counter if grounded
                if (groundCheck && groundCheck.isGrounded)
                {
                    jumpsRemaining = maxJumps;
                    isFalling = false;
                }
            }
        }

        // Check if the player is falling
        if (rigidbody.velocity.y < 0 && !isFalling)
        {
            isFalling = true;
            rigidbody.velocity += Vector3.up * Physics.gravity.y * (quickFallMultiplier - 1) * Time.deltaTime;  // Quicker descent
        }
        else if (isFalling && groundCheck && groundCheck.isGrounded)
        {
            isFalling = false;
        }
    }
}
