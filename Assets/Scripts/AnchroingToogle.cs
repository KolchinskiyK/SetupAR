using UnityEngine;

// This Script moves piano to Users location
// For better placement lay the left controller on the floor, look forward and press 'Y'
public class AnchoringToogle : MonoBehaviour
{
    [Header("References")]
    public Transform rightControllerTransform; // Legacy
    public Transform centerEyeAnchor;          // User

    [Header("Prefab & Settings")]
    //public GameObject spawnPrefab;
    public Vector3 localPositionOffset = new Vector3(0f, 0.1f, 0.1f);   // Piano Spawn Offset
    public Vector3 spawnScale = Vector3.one; // Legacy

    public GameObject spawnedObject;         // what should be moved
    private bool buttonWasPressed = false;   // Variable for smooth input
    private bool isVisible = false;          // Legacy

    void Update()
    {
        /*
        bool buttonPressedL = OVRInput.Get(
            OVRInput.Button.PrimaryIndexTrigger,
            OVRInput.Controller.LTouch
        );
        bool buttonPressedR = OVRInput.Get(
            OVRInput.Button.PrimaryIndexTrigger,
            OVRInput.Controller.RTouch
        );

        bool buttonPressed = (buttonPressedL || buttonPressedR);
        */

        // Checking if 'Y' was pressed
        if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.LTouch) && !buttonWasPressed)
        {
            //ToggleObject();
            SetObjectToLookAtView();
        }

        buttonWasPressed = OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.LTouch);
    }

    void ToggleObject()
    {
        /*if (spawnedObject == null)
        {
            spawnedObject = Instantiate(spawnPrefab);
            spawnedObject.transform.localScale = spawnScale;
        }*/

        if (!isVisible)
        {
            SetObjectToLookAtView();
            spawnedObject.SetActive(true);
            isVisible = true;
        }
        else
        {
            spawnedObject.SetActive(false);
            isVisible = false;
        }
    }

    // Moving the Object to Users location and rotating with right side
    void SetObjectToLookAtView()
    {
        Vector3 forward = centerEyeAnchor.forward;
        forward.y = 0f;
        forward.Normalize();

        Vector3 right = Vector3.Cross(Vector3.up, forward);

        Vector3 spawnPos =
            centerEyeAnchor.position
            + forward * localPositionOffset.z
            + right * localPositionOffset.x
            + Vector3.up * localPositionOffset.y;

        spawnedObject.transform.position = spawnPos;

        float yRotation = Mathf.Atan2(forward.x, forward.z) * Mathf.Rad2Deg;
        spawnedObject.transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
        Debug.Log("Anchored!");
    }
}
