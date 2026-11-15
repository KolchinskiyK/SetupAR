using UnityEngine;

// This Script is a transition between AR environment and technical backgrounds
// It allows to interact with Track-Symbols of MusicMenu
public class MusikMenuTrack : MonoBehaviour, Interactable
{
    public MicGenerator mik;        // Reference to Piano Script that plays music
    public int TrackID = 1;         // Track^s Number

    // "Interactable" Interface
    public void Interact()
    {
        //Debug.Log("Success Musik Menu!"); //works good
        //audioSource.PlayOneShot(testClip);
        PlaySound();
    }

    // Using another script with right attributes
    public void PlaySound()
    {
        Debug.Log("Playing Sound");
        mik.PlayTrack(TrackID);
    }
}
