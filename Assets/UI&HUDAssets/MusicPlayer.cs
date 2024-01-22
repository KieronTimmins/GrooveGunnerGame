using UnityEngine;
using UnityEngine.UI;

public class MusicPlayer : MonoBehaviour
{
    public Text songNameText;
    public Image songImage;
    public Button nextButton;
    public AudioSource audioSource;

    // Define a class to represent a song
    [System.Serializable]
    public class Song
    {
        public string name;
        public AudioClip audioClip; // Use AudioClip for the actual audio file
        public Sprite image;
        // Add any other song-related properties here
    }

    public Song[] songs;
    private int currentSongIndex = 0;

    void Start()
    {
        // Initialize the UI with the first song
        DisplayCurrentSong();

        // Play the first song on awake
        PlayCurrentSong();

        // Attach the button click event
        nextButton.onClick.AddListener(NextButtonClicked);
    }

    void NextButtonClicked()
    {
        // Increment the current song index
        currentSongIndex = (currentSongIndex + 1) % songs.Length;

        // Display the new current song
        DisplayCurrentSong();

        // Play the new song
        PlayCurrentSong();
    }

    void DisplayCurrentSong()
    {
        // Set the UI elements based on the current song
        songNameText.text = songs[currentSongIndex].name;
        songImage.sprite = songs[currentSongIndex].image;

        // Implement any other logic related to changing the song here
    }

    void PlayCurrentSong()
    {
        // Stop the current audio and play the new audio
        audioSource.Stop();
        audioSource.clip = songs[currentSongIndex].audioClip;
        audioSource.Play();
    }
}
