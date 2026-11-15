using UnityEngine;
using UnityEngine.UI;


// controls the visual representation of a single microphone object in the scene.
// handles billboarding (facing the camera), icon swapping (active/muted), 
// and distance-based scaling of floor markers.
public class StaticMic : MonoBehaviour, Interactable 
{
    private MicData myData;
    private int myIndex;
    private Transform floorReference;

    [Header("MicGen")]
    public MicGenerator gen;

    [Header("Visual Components")]
    public SpriteRenderer[] iconBillboards;  
    public SpriteRenderer[] floorRenderers;   
    public Image[] uiInfoImages;

    [Header("Fallback Status Icons")]

    //these were used as fallback icons, in case the custom icons could not be assigned correctly
    public Sprite iconActive; 
    public Sprite iconMuted;

    // internal storage for the final icons to be used (Custom vs Default)
    private Sprite finalIconActive;
    private Sprite finalIconMuted;
    
    [Header("Visual Settings")]
    public float scaleMinDist = 0.5f;
    public float scaleMaxDist = 3.0f;
    private Vector3[] originalScales;

    // initializes the microphone with data from the mic generator
    public void Setup(MicData data, int index,  Transform floorRef)
    {
        myData = data;
        myIndex = index;
        floorReference = floorRef;

        // logic: use custom icon if provided in data, otherwise use the default prefab icon
        if (data.customIconActive != null)
            finalIconActive = data.customIconActive; 
        else
            finalIconActive = iconActive;       

        if (data.customIconMuted != null)
            finalIconMuted = data.customIconMuted;
        else
            finalIconMuted = iconMuted;

        // Setup UI information images (if available)
        foreach (var img in uiInfoImages)
        {
            if (img != null)
            {
                if (data.infoImage != null)
                    {
                        img.sprite = data.infoImage; 
                        img.gameObject.SetActive(true);
                    }
                    else
                    {
                        img.gameObject.SetActive(false);
                        Debug.Log("No info image found.");
                    }
            
            }
        }
    }

    void Start()
    {
        // cache the original scale of floor markers so we can resize them relative to their design size later
        if (floorRenderers != null)
        {
            originalScales = new Vector3[floorRenderers.Length];
            for (int i = 0; i < floorRenderers.Length; i++)
            {
                if (floorRenderers[i] != null)
                    originalScales[i] = floorRenderers[i].transform.localScale;
            }
        } 
    }

    //this would have been used with the Legacy Scripts to change the icons of the four positions, that could have been configurated individually

    // public void SetIcon(Texture newIcon)
    // {
    //     foreach (var renderer in floorRenderers)
    //     {
    //         if (renderer != null)
    //         {
    //             renderer.material.mainTexture = newIcon;
    //         }
    //     }
    // }

    void LateUpdate()
    {
        if (Camera.main == null) return;
        bool isActive = false;
        
        //Check if mic is active
        if (MicGenerator.Instance != null)
        {
            //Debug.Log(MicGenerator.Instance + " exists");
            isActive = MicGenerator.Instance.IsMicActive(myIndex);
        }
        //else
            //Debug.Log("no MicGen in StaticMic!!");

        //determine which sprite to show based on state
        Sprite spriteToUse = isActive ? finalIconActive : finalIconMuted;
        
        //update billboard icons and make them face the user
        foreach (var billboard in iconBillboards)
        {
            if (billboard != null)
            {
                // make the object look at the camera, lock Y-axis
                Vector3 targetPos = Camera.main.transform.position;
                targetPos.y = billboard.transform.position.y; 
                billboard.transform.LookAt(targetPos);
                billboard.transform.Rotate(0, 180, 0); 

                if (spriteToUse != null)
                {
                    billboard.sprite = spriteToUse;
                    //billboard.material.color = Color.white; 
                }
            }
        }

        //animation logic for the growing/shrinking floormarkers based on distance
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        float scaleFactor = Mathf.InverseLerp(scaleMinDist, scaleMaxDist, distance);

        float targetY = (floorReference != null) ? floorReference.position.y : 0f;
        targetY += 0.01f;

        for (int i = 0; i < floorRenderers.Length; i++)
        {

            if (floorRenderers[i] != null && i < originalScales.Length)
            {
                Transform markerT = floorRenderers[i].transform;
                // interpolate scale: becomes smaller (0.5x) when close, larger (1.0x) when far
                markerT.localScale = originalScales[i] * Mathf.Lerp(0.5f, 1.0f, scaleFactor);
                //update position to stay on the floor
                Vector3 currentPos = markerT.position;
                    currentPos.y = targetY;
                    markerT.position = currentPos;
            }
        }
    }


    public void Interact()
    {
        Debug.Log("Interacted with Mic Index: " + myIndex);
        if (myData != null)
        {
            Debug.Log("Interacted with Static Mic");
            Debug.Log(myData != null);
            Debug.Log(myData.infoMenuPrefab != null);
            if (myData != null && myData.infoMenuPrefab != null)
            {
                Debug.Log("Interagiere mit: " + myData.micName);
                InfoDisplayManager.Instance.ShowInfo(myData.infoMenuPrefab, transform.position);
            }
        }    
    }
}
