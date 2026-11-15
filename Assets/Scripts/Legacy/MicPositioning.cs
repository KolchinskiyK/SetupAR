using UnityEngine;
using System.Collections.Generic;

public class MicPositioning : MonoBehaviour
{

    //Declaration and instantiation of variables
    public GameObject microphonePrefab;
    public float minWallDistance = 1.0f;
    public LayerMask wallLayer; 
       
    public Texture[] micIcons;
    private KeyCode placementKey = KeyCode.F;
    private KeyCode deletionKey = KeyCode.L;
    private List<GameObject> generatedMicrophones = new List<GameObject>();
    private List<Vector3> relativeMicPositions = new List<Vector3>()
    {
        new Vector3(2f, 0f, 1f), //0f instead of 1.5f to avoid mics positioned too high
        new Vector3(-4f, 0f, 2f),
        new Vector3(-5f, 0f, -5f),
        new Vector3(0f, 0f, 1f),
        //new Vector3(0f, 2f, 0f)
    };

    [Header("Audio Data")]
    public PositionAudioData[] allAudioData;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Not important for now, since methods are called by events
    void Start()
    {
        //PlaceMicrophones();
    }

    // Update is called once per frame
    // Update is now looking for pressed key for events
    void Update()
    {
        if(Input.GetKeyDown(placementKey))
        {
            PlaceMicrophones();
        }

        if(Input.GetKeyDown(deletionKey))
        {
            DestroyMicrophones();
        }
        
    }

    //Function to create and position mics with the trhee example vectors
    // Function that goes through every example vector and creates a mic at that position with correct rotation towards the piano
    // Mic gets added to List of generated Mics in the scene
    // At the start of function, already existing mics will get deleted to avoid complications
    void PlaceMicrophones()
    {
        DestroyMicrophones();

        for (int i = 0; i < relativeMicPositions.Count; i++)
        {
            Vector3 relativePosition = relativeMicPositions[i];
            Vector3 desiredWorldPosition = transform.TransformPoint(relativePosition);
            Vector3 finalPosition = desiredWorldPosition;
            
            if (Physics.CheckSphere(desiredWorldPosition, minWallDistance, wallLayer))
            {
                RaycastHit hit;
                Vector3 directionToPiano = (desiredWorldPosition - transform.position).normalized;

                if (Physics.Raycast(desiredWorldPosition, directionToPiano, out hit, 10f, wallLayer))
                    finalPosition = hit.point + hit.normal * minWallDistance;
            }

            GameObject newMic = Instantiate(microphonePrefab, finalPosition, Quaternion.identity);
            newMic.transform.LookAt(transform.position);

            Mikro mikroScript = newMic.GetComponent<Mikro>();
            if (mikroScript != null && allAudioData != null && allAudioData.Length > i)
            {
                mikroScript.InitAudio(allAudioData[i].audioClips);
            }

            Transform floorMarker = newMic.transform.Find("FloorMarker");
            
            if (floorMarker != null)
            {
                Renderer markerRenderer = floorMarker.GetComponent<Renderer>();
                
                if (markerRenderer != null)
                {
                    markerRenderer.material.color = Color.white;
                    if (micIcons.Length > 0)
                    {
                        markerRenderer.material.mainTexture = micIcons[i % micIcons.Length]; 
                    }
                }
            }

            //Adding lights above mics for better visuals
            // GameObject lightForMic = new GameObject("Spotlight_for_"+ newMic.name);
            // lightForMic.transform.SetParent(newMic.transform);
            // float verticalOffset = 1.5f;
            // Vector3 lightLocalPosition = new Vector3(0f, verticalOffset, 0f);
            // lightForMic.transform.localPosition = lightLocalPosition;
            // Light spotLight = lightForMic.AddComponent<Light>();
            // spotLight.type = LightType.Spot;
            // spotLight.color = Color.blue;
            // spotLight.intensity = 200f;
            // spotLight.spotAngle = 30f;
            // lightForMic.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);

            generatedMicrophones.Add(newMic);
            Debug.Log($"Microphone places at world coordinated {finalPosition}.");
        }
    }

    // Function that first deactivates the mics from the scene and then removes from List
    void DestroyMicrophones()
    {
        for (int i = generatedMicrophones.Count-1; i >= 0; i--)
        {
            if (generatedMicrophones[i] != null)
            {
                Destroy(generatedMicrophones[i]);
            }
            generatedMicrophones.RemoveAt(i);
        }
        Debug.Log("All mics deleted.");
    }

    private void OnDrawGizmos()
    {
        foreach (Vector3 relativePosition in relativeMicPositions)
        {
            Vector3 desiredWorldPosition = transform.TransformPoint(relativePosition);
            
            if (Physics.CheckSphere(desiredWorldPosition, minWallDistance, wallLayer))
            {
                Gizmos.color = Color.red; 
            }
            else
            {
                Gizmos.color = Color.green; 
            }
            //Gizmos.DrawWireSphere(desiredWorldPosition, minWallDistance);
        }
    }
}

[System.Serializable]
public class PositionAudioData
{
    public string name; // "Position 1 - Klavier"
    
    // 12 clips per position in following order: 
    // Model 1 (Low, Mid, High), 
    // Model 2 (Low, Mid, High), ...
    public AudioClip[] audioClips; 
}