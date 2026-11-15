using UnityEngine;

// LEGACY Room Achoring -> new Concept now
public class AnchoringSpawn : MonoBehaviour
{
    [Header("References")]
    public Transform rightControllerTransform;
    public Transform centerEyeAnchor; 

    [Header("Prefab & Settings")]
    public GameObject spawnPrefab;
    public Vector3 localPositionOffset = new Vector3(0f, 0.1f, 0.1f);
    public Vector3 spawnScale = Vector3.one;

    private GameObject spawnedObject;
    private bool buttonWasPressed = false;
    private bool isVisible = false;

    void Update()
    {
        bool buttonPressed = OVRInput.Get(
            OVRInput.Button.One,
            OVRInput.Controller.RTouch
        );

        if (buttonPressed && !buttonWasPressed)
        {
            ToggleObject();
        }

        buttonWasPressed = buttonPressed;
    }

    void ToggleObject()
    {
        if (spawnedObject == null)
        {
            spawnedObject = Instantiate(spawnPrefab);
            spawnedObject.transform.localScale = spawnScale;
        }

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


