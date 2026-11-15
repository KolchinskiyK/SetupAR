using UnityEngine;

public class Mikro : MonoBehaviour, Interactable
{
    [Header("Settings")]
    public AudioSource audioSource;   
    public AudioClip testClip;
    public GameObject menuUI; //Canvas UI in Prefab
    bool menuIsOpen = false;
    private Renderer floorRenderer;
    public bool shortcut;
    
    [Header("Visual Settings")]
    public float scaleMinDist = 0.5f;
    public float scaleMaxDist = 3.0f;
    private Vector3 originalScale;

    [Header("Microphone Type Icons")]
    public Texture[] typeIcons; 
    private Renderer iconBillboardRenderer;
    private Transform iconBillboardTransform;

    [Header("Internal Logic")]
    private AudioClip[] myAudioClips; // 12 clips for one position
    private int currentModelIndex = 0; // 0 to 3
    private int currentHeightIndex = 1; // 0=low, 1=standard, 2=high

    // heights in meter
    private float[] heightValues = new float[] { 1.0f, 1.5f, 2.0f };

    // initialisation
    public void InitAudio(AudioClip[] clips)
    {
        myAudioClips = clips;
    }
    
    // UI functions
    
    public void SetModelFromUI(int modelIndex)
    {
        currentModelIndex = modelIndex; //sets icon, like previous function
        SetTypeIcon(modelIndex); 
    }

    public void SetHeightFromUI(int heightIndex)
    {
        currentHeightIndex = heightIndex;
        UpdateVisualHeight();
    }

    public void PlayCurrentSound()
    {
        if (myAudioClips == null || audioSource == null) return;

        // calculating index
        // equation: (Model * 3) + height
        // Eg: model 0, height 0 -> index 0
        // Eg: model 0, height 1 -> index 1
        // Eg: model 1, höhe 0 -> index 3
        int clipIndex = (currentModelIndex * 3) + currentHeightIndex;

        if (clipIndex < myAudioClips.Length && myAudioClips[clipIndex] != null)
        {
            audioSource.Stop();
            audioSource.clip = myAudioClips[clipIndex];
            audioSource.Play();
            Debug.Log($"Sound: Model {currentModelIndex}, Height {currentHeightIndex}");
        }
        else
        {
            Debug.LogWarning("No sound for this combination found!");
        }
    }

    // Visual optimization
    void UpdateVisualHeight()
    {
        Transform billboard = transform.Find("IconBillboard");
        if (billboard != null)
        {
            Vector3 pos = billboard.localPosition;
            pos.y = heightValues[currentHeightIndex];
            billboard.localPosition = pos;
        }
    }

    void Start()
    {
        Transform floorMarker = transform.Find("FloorMarker");
        if (floorMarker != null) 
        {
            floorRenderer = floorMarker.GetComponent<Renderer>();
            originalScale = floorMarker.localScale;
            if(originalScale == Vector3.zero) 
            {
                originalScale = new Vector3(0.5f, 0.5f, 1f); // fallback
                Debug.LogWarning("Originalgröße war 0. Habe Fallback gesetzt.");
            }
        }
        else
        {
            Debug.LogError($"Floormarker not found in prefab of {name}");
        }

        Transform foundBillboard = transform.Find("IconBillboard");
        if (foundBillboard != null)
        {
            iconBillboardRenderer = foundBillboard.GetComponent<Renderer>();
            iconBillboardTransform = foundBillboard;

            // Setze initial das erste Icon (Basic), falls vorhanden
            SetTypeIcon(0);
        }
        else Debug.LogError($"IconBillboard not found in prefab of {name}. Hast du das Quad erstellt?");   

        if (menuUI != null)
        {
            MicMenuController menuCtrl = menuUI.GetComponent<MicMenuController>();
            // if script is not on the root, search through the children elements
            if (menuCtrl == null) menuCtrl = menuUI.GetComponentInChildren<MicMenuController>();
            
            if (menuCtrl != null)
            {
                menuCtrl.linkedMic = this;
            }
        }
    }

    public void SetTypeIcon(int index)
    {
        if (iconBillboardRenderer != null && typeIcons != null && index < typeIcons.Length)
        {
            iconBillboardRenderer.material.mainTexture = typeIcons[index];
            iconBillboardRenderer.material.color = Color.white;
        }
    }

    public void Interact()
    {
        Debug.Log("Success!");
        //audioSource.PlayOneShot(testClip);
        PlaySound(shortcut);
        Switch();
    }

    public void PlaySound(bool shortcut)
    {
        if (shortcut)
        {
            audioSource.Stop();
            audioSource.clip = testClip;
            audioSource.Play();
        }
    }

    void Switch()
    {
        menuIsOpen = !menuIsOpen;

        if (!menuIsOpen) menuUI.SetActive(false);
        else menuUI.SetActive(true);
    }
    public void SetIcon(Texture newIcon)
    {
        if (floorRenderer != null && newIcon != null)
        {
            floorRenderer.material.mainTexture = newIcon;
        }
    }
    void LateUpdate()
    {
        if (floorRenderer != null)
        {
        
            // Vector3 targetPos = transform.position;
            // targetPos.y = 0.02f; 
            // floorRenderer.transform.position = targetPos;
            // floorRenderer.transform.rotation = Quaternion.Euler(90f, 0f, 0f);

            if (Camera.main != null)
            {
                float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
                float scaleFactor = Mathf.InverseLerp(scaleMinDist, scaleMaxDist, distance);
                floorRenderer.transform.localScale = originalScale * scaleFactor;
            }
        }

        if (iconBillboardTransform != null && Camera.main != null)
        {
            Vector3 targetPosition = Camera.main.transform.position;
            targetPosition.y = iconBillboardTransform.position.y;
            iconBillboardTransform.LookAt(targetPosition);
            iconBillboardTransform.Rotate(0, 180, 0); 
        }
    }
}