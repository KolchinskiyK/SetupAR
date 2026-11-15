using Meta.XR.ImmersiveDebugger.UserInterface.Generic;
using UnityEngine;

// This Script is a transition between AR environment and technical backgrounds
// It allows to interact with Microfon-Tiles of MusicMenu
public class MusikMenuTiles : MonoBehaviour, Interactable
{
    public PlayUI p;                    // Reference to the same object to use other technical script
    public AudioClip audioClip;         // LEGACY
    public AudioSource audioSource;     // LEGACY

    // "Interactable" Interface
    public void Interact()
    {
        //Debug.Log("Success Musik Tiles!"); //works good
        //audioSource.PlayOneShot(testClip);
        ToggleSound();
    }

    // Using another script
    public void ToggleSound()
    {
        Debug.Log("Success Musik Toggle!" + p);
        p.OnClick_Toggle();
        //PlaySoundShort(); // test
    }

    // LEGACY for test
    public void PlaySoundShort()    
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
            return;
        }
        StartCoroutine(PlayForSeconds(1f));
    }

    // LEGACY for test
    private System.Collections.IEnumerator PlayForSeconds(float duration)
    {
        audioSource.clip = audioClip;
        audioSource.Play();

        yield return new WaitForSeconds(duration);

        audioSource.Stop();
    }
}