using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

// LEGACY Script to scan the room and place objects for other algorithms
public class WallDetectorBB : MonoBehaviour
{
    public GameObject wallPrefab;
    private ARPlaneManager planeManager;

    void Start()
    {
        planeManager = FindObjectOfType<ARPlaneManager>();
        if (planeManager != null)
            planeManager.planesChanged += OnPlanesChanged;
    }

    void OnDestroy()
    {
        if (planeManager != null)
            planeManager.planesChanged -= OnPlanesChanged;
    }

    void OnPlanesChanged(ARPlanesChangedEventArgs args)
    {
        foreach (var plane in args.added)
        {
            if (plane.alignment == PlaneAlignment.Vertical) 
            {
                GameObject wall = Instantiate(wallPrefab);
                wall.transform.position = plane.transform.position;
                wall.transform.rotation = plane.transform.rotation;
                wall.transform.localScale = new Vector3(plane.size.x, plane.size.y, 0.05f);
            }
        }
    }
}






