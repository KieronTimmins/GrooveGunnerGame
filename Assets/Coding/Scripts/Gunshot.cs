using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunshot : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip gunshotSound;
    private AudioSource audioSource;
    private RhythmManager rhythmManager;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rhythmManager = FindObjectOfType<RhythmManager>(); // Find the rhythm manager in the scene
    }

    void Update()
    {
        // Check for player input or other conditions to fire the gunshot
        if (Input.GetButtonDown("Fire1") )
        {
            FireGunshot();
        }
    }

    void FireGunshot()
    {
        // Play gunshot sound
        audioSource.PlayOneShot(gunshotSound);
    }
}
