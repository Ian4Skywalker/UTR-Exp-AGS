/*
 * This Unity script plays a sound 5 seconds after the object appears in the scene,
 * and continues to play it repeatedly at intervals equal to the length of the audio clip.
 */

using UnityEngine;

public class PlaySoundLoop : MonoBehaviour
{
    // Reference to the AudioSource component which contains the sound clip
    public AudioSource audioSource;

    void Start()
    {
        // Schedule the PlayAudio method to be called after 5 seconds,
        // then repeatedly every time the clip's length elapses
        InvokeRepeating("PlayAudio", 5f, audioSource.clip.length);
    }

    // Method that plays the audio clip
    void PlayAudio()
    {
        audioSource.Play();
    }
}