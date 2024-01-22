using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;




public class Program : MonoBehaviour
{

    static void Main()
    {
        var audioFile = "Assets/Music/classical - mozart remix.mp3";
        var rhythmManager = new RhythmManager(audioFile);

        rhythmManager.Start();

        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();


    }


}
class RhythmManager : MonoBehaviour
{

    private string audioFilePath;
    private WaveOutEvent waveOutEvent;
    private AudioFileReader audioFileReader;

    public RhythmManager(string audioFilePath)
    {
        this.audioFilePath = audioFilePath;
        waveOutEvent = new WaveOutEvent();
        audioFileReader = new AudioFileReader(audioFilePath);
        waveOutEvent.Init(audioFileReader);


        // Set up an event handler for when the playback stops

        waveOutEvent.PlaybackStopped += (sender, args) => Stop();
    
    }

    public void Start()
    {
        Console.WriteLine("Rhythm Manager Started.");

        //start audio playback
        waveOutEvent.Play();

        //start a seperate thread for processing the beats

        var beatDetectionThread = new System.Threading.Thread(() => DetectBeats());
        beatDetectionThread.Start();
    }

    private void DetectBeats()
    {
        var beatDetector = new BeatDetector(audioFilePath);

        while (waveOutEvent.PlaybackState == PlaybackState.Playing)
        {
            // Check if a beat occurred
            if (beatDetector.BeatDetected())
            {
                // Trigger your movement logic here
                Console.WriteLine("Movement triggered at beat!");

                // You can replace the above line with your actual movement code
            }

            // Adjust the sleep time based on your desired precision
            System.Threading.Thread.Sleep(10);
        }
    }

    private void Stop()
    {
        Console.WriteLine("Rhythm Manager stopped.");
    }
}

class BeatDetector : MonoBehaviour
{
    private string audioFilePath;
    private BeatInfo beatInfo;

    public BeatDetector(string audioFilePath)
    {
        this.audioFilePath = audioFilePath;
        this.beatInfo = new BeatInfo();

        // You may need to adjust the sensitivity based on your audio file
        var beatTracker = new beatTracker(audioFilePath, 120); // 120 BPM as an example, adjust as needed
        beatTracker.BeatDetected += (sender, args) => beatInfo.BeatDetected();
        beatTracker.Start();
    }

    public bool BeatDetected()
    {
        return beatInfo.ConsumeBeat();
    }
}

class BeatInfo : MonoBehaviour
{
    private bool beatDetected;

    public void BeatDetected()
    {
        beatDetected = true;
    }

    public bool ConsumeBeat()
    {
        if (beatDetected)
        {
            beatDetected = false;
            return true;
        }
        return false;
    }







}

public class beatTracker
{
    private string audioFilePath;
    private int beatsPerMinute;
    

    // Event to notify when a beat is detected
    public event EventHandler BeatDetected;

    public beatTracker(string audioFilePath, int beatsPerMinute)
    {
        this.audioFilePath = audioFilePath;
        this.beatsPerMinute = beatsPerMinute;

        // Initialize your beat detection library or class
        
    }
    public void Start()
    {
        // Subscribe to the BeatDetected event of your beat detection library
       
    }

    private void OnBeatDetected(object sender, EventArgs args)
    {
        // Notify subscribers that a beat is detected
        BeatDetected?.Invoke(this, EventArgs.Empty);
    }
}





