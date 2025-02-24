using TMPro;
using UnityEngine;
using UnityEngine.UI; // Add this line to include the UI namespace
using CodeMonkey.HealthSystemCM;
using System.Collections.Generic; // Add this line to include the Collections.Generic namespace

public class MusicLooper : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip fullTrack;

    public float introEndTime = 13.71f; // The exact point where looping should start
    private bool isLooping = false;

    void Start()
    {
        if (audioSource == null || fullTrack == null)
        {
            Debug.LogError("MusicLooper: Missing AudioSource or AudioClip!");
            return;
        }

        audioSource.clip = fullTrack;
        audioSource.Play();
    }

    void Update()
    {
        if (!isLooping && audioSource.time >= introEndTime)
        {
            StartLooping();
        }
    }

    void StartLooping()
    {
        isLooping = true;
        audioSource.time = introEndTime; // Jump to the loop start point
        audioSource.loop = true; // Enable looping
        audioSource.Play();
    }
}