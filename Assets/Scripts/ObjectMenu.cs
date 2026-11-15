using UnityEngine;

// This Script implements Opening a Canvas on interaction with a gameObject
// This Canvas rotates on Spawning to User^s position
public class ObjectMenu : MonoBehaviour, Interactable
{
    [Header("References")]
    [SerializeField] private Transform target;          // where should the menu appear (Gameobject)
    [SerializeField] private Transform cameraTransform; // user
    [SerializeField] private GameObject menu;           // Canvas

    [Header("Offsets")]
    [SerializeField] private Vector3 localOffset = new Vector3(0.5f, 1f, 0f);   // where should it spawn relative to Object

    private bool updated = false;

    // Finding CameraRig in AR, cause its not possible to give a Reference to it in PreFab
    void Awake()
    {
        if (menu)
            menu.SetActive(false);

        if (!cameraTransform)
        {
            Camera cam = Camera.main;
            if (cam)
            {
                cameraTransform = cam.transform;
                return;
            }

            var rig = FindFirstObjectByType<OVRCameraRig>();
            if (rig)
            {
                cameraTransform = rig.centerEyeAnchor;
            }
            else
            {
                Debug.LogWarning("No camera / OVRCameraRig found in scene.");
            }
        }
    }

    // Going sure, that in the beginning its off
    void Start()
    {
        if (menu)
            menu.SetActive(false);
    }

    // Checking, if it should be updated this "frame"
    void Update()
    {
        if (!menu || !menu.activeSelf)
            return;

        if (!updated && target && cameraTransform)
        {
            PositionAndRotateMenu();
            updated = true;
        }
    }

    // "Interactable" interface
    public void Interact()
    {
        Debug.Log("Object interacted");
        ToggleMenu();
    }

    // Switching Menu State
    private void ToggleMenu()
    {
        if (!menu) return;

        //menuCanvas.SetActive(true);
        //updated = false;
        if (menu.activeSelf)
        {
            menu.transform.SetParent(null, true);

            menu.SetActive(false);
            updated = false;
        }
        else
        {
            menu.SetActive(true);
            updated = false;
        }
    }

    // Update Position and Rotation after new activation
    private void PositionAndRotateMenu()
    {
        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0f;
        cameraForward.Normalize();

        Vector3 cameraRight = cameraTransform.right;
        cameraRight.y = 0f;
        cameraRight.Normalize();

        Vector3 offset =
            cameraRight * localOffset.x +
            Vector3.up * localOffset.y + Vector3.forward * localOffset.z;

        menu.transform.position = target.position + offset;

        Vector3 lookDir = menu.transform.position - cameraTransform.position;
        lookDir.y = 0f;

        if (lookDir.sqrMagnitude > 0.001f) menu.transform.rotation = Quaternion.LookRotation(lookDir);
    }

    // Resetting "update" state for rotation
    private void OnDisable()
    {
        updated = false;
    }

    private void OnEnable()
    {
        updated = false;
    }
}
