using UnityEngine;

// This Script was made to test User Interactions with Buttons and Audio Output

public class SoundButton : MonoBehaviour, Interactable
{
    // References to play sound
    public AudioClip audioClip;
    public AudioSource audioSource;

    // Implementation of Interactable Interface
    public void Interact()
    {
        Debug.Log("Success Menu!");
        PlaySound();
    }

    public void PlaySound()
    {
        if (audioSource != null && audioClip != null)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();         // if Track is now playing, stopping it
                return;
            }
            audioSource.Stop();             // Stopping the last track to avoid multiple Tracks at the same time
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }
}

