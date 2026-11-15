using UnityEngine;

// LEGACY script -> was designed to do all the same as ObjectMenu, but special for microfones, as there were issues with loading
public class ObjMenSpecial : MonoBehaviour, Interactable
{
    [Header("References")]
    [SerializeField] private Transform target; // where should the menu appear
    [SerializeField] private Transform cameraTransform; // user
    [SerializeField] private GameObject menu;

    [Header("Offsets")]
    [SerializeField] private Vector3 localOffset = new Vector3(0.5f, 1f, 0f);

    private bool updated = false;

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

    void Start()
    {
        if (menu)
            menu.SetActive(false);
    }
    void Update()
    {
        Debug.Log("updated status" + updated);
        Debug.Log("menu" +menu + !menu.activeSelf);
        if (!menu || !menu.activeSelf)
            return;

        if (!updated && target && cameraTransform)
        {
            Debug.Log("updating Position and rotation");
            PositionAndRotateMenu();
            updated = true;
        }
    }


    public void Interact()
    {
        Debug.Log("Object interacted");
        ToggleMenu();
    }

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
            Vector3.up * localOffset.y;

        menu.transform.position = target.position + offset;

        Vector3 lookDir = menu.transform.position - cameraTransform.position;
        lookDir.y = 0f;

        if (lookDir.sqrMagnitude > 0.001f) menu.transform.rotation = Quaternion.LookRotation(lookDir);
    }


    private void OnDisable()
    {
        updated = false;
    }

    private void OnEnable()
    {
        updated = false;
    }
}
