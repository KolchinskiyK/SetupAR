using UnityEngine;
using UnityEngine.UI;

public class Bass : MonoBehaviour, Interactable
{
    public PlayUI p;
    public AudioClip audioClip;
    public AudioSource audioSource;

    public float maxSliderOffset = 1.0f; 
    public float bassMin = 0.0f;
    public float bassMax = 1.0f;
    [Header("Visuals")]
    public SpriteRenderer sliderRenderer; // Hier den SpriteRenderer des Sliders reinziehen
    public Image image;
    public Sprite[] stepSprites;          // Größe 6: Bilder für 0, 20, 40, 60, 80, 100

    [Header("Settings")]
    private int currentIndex = 1; // Startwert (z.B. Index 1 = 20%)

    [Header("MicGen")]
    public MicGenerator gen;

    public int x;

    void Start()
    {
        UpdateVisuals();
        UpdateAudio();
    }

    public void ChangeStep(int direction)
    {
        currentIndex += direction;

        currentIndex = Mathf.Clamp(currentIndex, 0, stepSprites.Length - 1);

        Debug.Log("ChangeStep-ed");

        UpdateVisuals();
        UpdateAudio();
    }
    void UpdateVisuals()
    {
        // Tauscht das Sprite aus
        if (sliderRenderer != null && stepSprites.Length > 0)
        {
            sliderRenderer.sprite = stepSprites[currentIndex];
            //Debug.Log("new sprite there");
        }
        if (image != null && stepSprites.Length > 0)
        {
            image.sprite = stepSprites[currentIndex];
            //Debug.Log("new image there");
        }
    }
    void UpdateAudio()
    {
        float volume = (float)currentIndex / (float)(stepSprites.Length - 1);

        if (gen != null)
        {
            gen.SetBassState(volume);
            Debug.Log("new bass volume");
        }
    }
    public void Interact()
    {
        //Debug.Log("Success Musik Tiles!"); //works good
        //audioSource.PlayOneShot(testClip);
        ChangeBass();
    }
    public void ChangeBass()
    {
        {
            // Relative H�he zum Startwert
            //float relativeHeight = transform.position.y - startPosition.y; //DISABLED TO CON

            // Clamp, damit wir im erlaubten Bereich bleiben
            //relativeHeight = Mathf.Clamp(relativeHeight, 0f, maxSliderOffset); //DISABLED TO CON

            // Normalisieren (0�1)
            //float normalizedValue = relativeHeight / maxSliderOffset; //DISABLED TO CON

            // Auf Bassbereich mappen
            //float bassValue = Mathf.Lerp(bassMin, bassMax, normalizedValue); //DISABLED TO CON

            //Debug.Log("Success Musik Bass Slider! Bass: " + bassValue); //DISABLED TO CON

            // Bass setzen
            // p.OnSlider_Changed(bassValue); //DISABLED TO CON
        }
    }
}