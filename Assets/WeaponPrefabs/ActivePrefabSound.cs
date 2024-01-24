using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PrefabSoundPair
{
    public GameObject prefab;
    public AudioClip sound;
}

public class ActivePrefabSound : MonoBehaviour
{
    public List<PrefabSoundPair> prefabSoundPairs = new List<PrefabSoundPair>(); // List of prefab and sound pairs
    private AudioSource audioSource; // AudioSource component for playing sounds

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        // Check for active prefabs in each frame
        UpdateActivePrefab();

        // Play the sound based on the active prefab
        PlayPrefabSound();
    }

    void UpdateActivePrefab()
    {
        // Check each prefab to see if it is active
        foreach (PrefabSoundPair pair in prefabSoundPairs)
        {
            if (pair.prefab.activeInHierarchy)
            {
                return; // Exit the loop once an active prefab is found
            }
        }
    }

    void PlayPrefabSound()
    {
        // Find the active prefab
        GameObject activePrefab = null;
        foreach (PrefabSoundPair pair in prefabSoundPairs)
        {
            if (pair.prefab.activeInHierarchy)
            {
                activePrefab = pair.prefab;
                break;
            }
        }

        // Play the sound based on the active prefab
        if (activePrefab != null)
        {
            audioSource.clip = FindSoundForPrefab(activePrefab);
            if (audioSource.clip != null && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            // If no active prefab is found or sound is not defined, stop playing
            audioSource.Stop();
        }
    }

    AudioClip FindSoundForPrefab(GameObject prefab)
    {
        foreach (PrefabSoundPair pair in prefabSoundPairs)
        {
            if (pair.prefab == prefab)
            {
                return pair.sound;
            }
        }
        return null;
    }
}
