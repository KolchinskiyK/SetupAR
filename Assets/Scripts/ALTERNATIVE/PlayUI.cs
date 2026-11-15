using UnityEngine;
using UnityEngine.UI;


//handles UI interactions for buttons (Mute/Unmute, Play Track) and Sliders.
//updates the visual icon based on its state

public class PlayUI : MonoBehaviour
{
    public enum ButtonType { MicControl, TrackControl }
    [Header("Settings")]
    public ButtonType myType;
    public int myIndex; //ID used to identify which mic or track this button controls
    
    [Header("Normal Buttons (Button)")]
    public Image iconImage;       
    public Sprite spriteActive;   
    public Sprite spriteMuted;    

    [Header("Bass only (Slider)")]
    public bool isBassSlider;

    [Header("MicGen")]
    public MicGenerator gen; //reference to the main logic manager

    void Start()
    {
        if (iconImage != null) iconImage.color = Color.white;
        UpdateVisuals();
    }
    void Update()
    {
        // polls the state every frame to keep UI in sync with the internal logic
        UpdateVisuals();
    }


    //called when a regular button is clicked
    //distinguishes bewteen toggling a mic or switching music track
    public void OnClick_Toggle()
    {
        if (isBassSlider) return;
        if (gen == null) return;

        Debug.Log("Toggle Clicked: Index " + myIndex + " Type: " + myType);

        if (myType == ButtonType.MicControl)
        {
            gen.ToggleMic(myIndex);
        }
        else if (myType == ButtonType.TrackControl)
        {
            gen.PlayTrack(myIndex);
        }

        UpdateVisuals();
    }

    //this function would have been used for the slider
    public void OnSlider_Changed(float value)
    {
        if (!isBassSlider) return;
        if (gen == null) return;

        gen.SetBassVolume(value);
        bool shouldBeOn = (value > 0.01f);
        
        bool isCurrentlyActive = gen.IsMicActive(myIndex);
        
        if (shouldBeOn != isCurrentlyActive)
        {
            gen.ToggleMic(myIndex);
        }
    }


    //checks the current state in MicGenerator and swaps the sprite (Active vs Muted/Stopped).
    void UpdateVisuals()
    {
        if (gen == null || iconImage == null) return;

        bool isActive = false;

        if (myType == ButtonType.MicControl)
        {
            //Debug.Log("i want IMA with Index " + myIndex);

            isActive = gen.IsMicActive(myIndex);
            //Debug.Log("it was good with " + myIndex);
        }
        else if (myType == ButtonType.TrackControl)
        {
            isActive = gen.IsTrackPlaying(myIndex);
            //Debug.Log("it was good with " + myIndex);
        }

        if (isActive)
        {
            if (spriteActive != null) iconImage.sprite = spriteActive;
        }
        else
        {
            if (spriteMuted != null) iconImage.sprite = spriteMuted;
        }
    }
}