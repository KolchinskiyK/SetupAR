using UnityEngine;

// This Script was made to test User Interactions with Buttons and Audio Output (certain time amount)

public class SoundShortButton : MonoBehaviour, Interactable
{
    // References to play sound
    public AudioClip audioClip;
    public AudioSource audioSource;

    public void Interact()
    {
        Debug.Log("Success Menu!");
        //audioSource.PlayOneShot(testClip);
        PlaySoundShort();
    }

    // Implementation of Interactable Interface
    public void PlaySoundShort()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();                 // if Track is now playing, stopping it
            return;
        }
        StartCoroutine(PlayForSeconds(3f));
    }

    // Playing music for a set amount of time (3s)
    private System.Collections.IEnumerator PlayForSeconds(float duration)
    {
        audioSource.clip = audioClip;
        audioSource.Play();

        yield return new WaitForSeconds(duration);

        audioSource.Stop();
    }
}

