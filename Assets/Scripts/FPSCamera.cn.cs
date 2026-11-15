using UnityEngine;

public class FPSCamera : MonoBehaviour
{
    public float mouseSensitivity = 150f;
    public Transform playerBody;  // der Player (Capsule)

    float xRotation = 0f;
    //float yRotation = 30f;

    void Start()
    {
        // Cursor sperren
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Vertikale Rotation (Kamera)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Horizontale Rotation (dreht Player)
        playerBody.Rotate(Vector3.up * mouseX);
        //yRotation -= mouseX;
        //yRotation = Mathf.Clamp(yRotation, -60f, 120f);

        //transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
    }
}

