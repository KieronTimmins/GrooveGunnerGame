using UnityEngine;
using TMPro;  // Import TextMeshPro namespace
using System.Collections;

public class SubtitleManager : MonoBehaviour
{
    public TextMeshProUGUI subtitleText;
    public AudioSource narratorAudio;

    [System.Serializable]
    public class Subtitle
    {
        [TextArea]
        public string text;
        public float startTime;
        public float endTime;
    }

    public Subtitle[] subtitles;

    private void Start()
    {
        // Clear subtitle text initially
        subtitleText.text = "";

        // Load the "Speak" audio clip dynamically
        narratorAudio = GameObject.Find("Talk").AddComponent<AudioSource>(); // Assuming "Talk" is the name of your empty GameObject
        narratorAudio.clip = Resources.Load<AudioClip>("Speak");

        // Start the subtitle display
        DisplaySubtitles();
    }

    private void DisplaySubtitles()
    {
        // Start a coroutine to check audio playback time and display subtitles accordingly
        StartCoroutine(SubtitleCoroutine());
    }

    private IEnumerator SubtitleCoroutine()
    {
        // Play the audio
        narratorAudio.Play();

        // Loop through the subtitles
        foreach (Subtitle subtitle in subtitles)
        {
            // Wait until the audio reaches the subtitle start time
            while (narratorAudio.time < subtitle.startTime)
            {
                yield return null;
            }

            // Display the subtitle text
            subtitleText.text = subtitle.text;

            // Wait until the audio reaches the subtitle end time
            while (narratorAudio.time < subtitle.endTime)
            {
                yield return null;
            }

            // Clear the subtitle text
            subtitleText.text = "";
        }

        // Stop the audio when all subtitles are displayed
        narratorAudio.Stop();
    }
}
