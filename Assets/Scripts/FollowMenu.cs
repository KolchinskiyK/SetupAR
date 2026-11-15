using UnityEngine;

// This Script implements Opening a Canvas that gives User way to interact with microfones
// This Canvas rotates on Spawning to User^s position and than follows (without rotation)
public class FollowMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform centerEyeAnchor;     // user
    [SerializeField] private GameObject menuVisual;         // canvas

    [Header("Settings")]
    [SerializeField] private float distance = 2f;           // follow distance to user
    [SerializeField] private bool rotateWithView = false;   // TEST FEATURE that worked in PC Scene, that allows rotation around User, dont recommended

    [Header("Rotation")]
    [SerializeField] private float xTilt = 90f; 

    private bool isOpen = false;
    private bool positionLocked = false;
    private Vector3 lockedOffset;

    void Update()
    {
        // Left A or Right X
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch) || OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch)
            || Input.GetKeyDown(KeyCode.Tab)) // || OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.RTouch) 
        {
            ToggleMenu();
        }
    }

    // Following User
    void LateUpdate()
    {
        if (!isOpen || centerEyeAnchor == null)
            return;

        if (rotateWithView)
        {
            lockedOffset = centerEyeAnchor.forward * distance;
        }
        else if (!positionLocked)
        {
            lockedOffset = centerEyeAnchor.forward * distance;
            positionLocked = true;
        }

        transform.position = centerEyeAnchor.position + Vector3.up * (-0.30f) + lockedOffset; // OFFSET

        Vector3 lookDir = transform.position - centerEyeAnchor.position;
        lookDir.y = 0f;
        Quaternion lookRotation = Quaternion.LookRotation(lookDir);
        Quaternion tiltRotation = Quaternion.Euler(xTilt, 0f, 0f);

        transform.rotation = lookRotation * tiltRotation;
    }

    // Switching Menu state
    void ToggleMenu()
    {
        isOpen = !isOpen;
        menuVisual.SetActive(isOpen); 

        positionLocked = false;
    }

    // LEGACY CODE

    //transform.position = target.position + target.TransformVector(offset);
    /*
    var cam = Camera.main;
    if (rotating)
    {
        transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward,
                         cam.transform.rotation * Vector3.up);
        transform.rotation = Quaternion.identity;
    }
    */



    /*
    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    */
}
/*
using UnityEngine;

public class FollowMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform centerEyeAnchor;
    [SerializeField] private GameObject menuVisual;

    [Header("Settings")]
    [SerializeField] private float distance = 2f;
    [SerializeField] private bool rotateWithView = false;

    [Header("Rotation")]
    [SerializeField, Range(-45f, 45f)]
    private float tiltAngle = 0f; 

    private bool isOpen = false;
    private bool positionLocked = false;
    private Vector3 lockedOffset;

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.LTouch)
            || Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleMenu();
        }
    }

    void LateUpdate()
    {
        if (!isOpen || centerEyeAnchor == null)
            return;

        if (rotateWithView)
        {
            lockedOffset = centerEyeAnchor.forward * distance;
        }
        else if (!positionLocked)
        {
            lockedOffset = centerEyeAnchor.forward * distance;
            positionLocked = true;
        }

        transform.position = centerEyeAnchor.position + lockedOffset;

        Vector3 lookDir = transform.position - centerEyeAnchor.position;
        lookDir.y = 0f;

        Quaternion yawRotation = Quaternion.LookRotation(lookDir);

        Quaternion tiltRotation = Quaternion.Euler(tiltAngle, 0f, 0f);

        transform.rotation = yawRotation * tiltRotation;
    }

    void ToggleMenu()
    {
        isOpen = !isOpen;
        menuVisual.SetActive(isOpen);

        positionLocked = false;
    }
}
*/