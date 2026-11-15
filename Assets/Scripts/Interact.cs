using Oculus.Interaction;
using UnityEngine;
using UnityEngine.UI;

// This Script is an Implementation of "Interactable" Interface
// It sets how the User interacts with objects and what catalyses interactions

public interface Interactable
{
    void Interact();
}

// LEGACY -> new Concept now
public interface SliderInteractable 
{
    void SetValueFromHit(RaycastHit hit);
}

public class Interact : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform leftController;      // Position of Left Controller
    [SerializeField] private Transform rightController;     // Position of Right Controller

    [Header("Interaction")]
    [SerializeField] private float interactDistance = 5f;   // Distance of Raycast
    [SerializeField] private LayerMask interactLayer;       // Possibility to border the intraction area

    //[SerializeField] private LineRenderer lineRenderer;

    // Look each "frame", if user tries to interact
    void Update()
    {
        // Left Controller + Primary Button (A) 
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch))
        {
            TryInteractL();
        }

        // Right Controller + Primary Button (X)
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            TryInteractR();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteractPC();

        }
    }

    // Left Controller tries to interact with something, Raycast is sent and it triggers an "Interact" action,
    // if gameObject has the "Interactable" interface implemented
    void TryInteractL()
    {
        if (leftController == null)
        {
            Debug.LogWarning("no L Controller!");
            return;
        }

        Ray ray = new Ray(leftController.position, leftController.forward);
        //Debug.DrawRay(ray.origin, ray.direction * interactDistance, Color.green, 1f);

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactLayer))
        {
            Debug.Log($"Hit: {hit.collider.name}");

            if (hit.collider.TryGetComponent<Interactable>(out var interactable))
            {
                interactable.Interact();
            }
            if (hit.collider.TryGetComponent<SliderInteractable>(out var sliderinteractable))   // Legacy
            {
                sliderinteractable.SetValueFromHit(hit);
            }
        }
        //lineRenderer.SetPosition(0, leftController.position);
        //lineRenderer.SetPosition(1, leftController.position + leftController.forward * interactDistance);
    }

    // Right Controller tries to interact with something, Raycast is sent and it triggers an "Interact" action,
    // if gameObject has the "Interactable" interface implemented
    void TryInteractR()
    {
        if (rightController == null)
        {
            Debug.LogWarning("no R Controller!");
            return;
        }

        Ray ray = new Ray(rightController.position, rightController.forward);
        //Debug.DrawRay(ray.origin, ray.direction * interactDistance, Color.green, 1f);

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactLayer))
        {
            Debug.Log($"Hit: {hit.collider.name}");

            if (hit.collider.TryGetComponent<Interactable>(out var interactable))
            {
                interactable.Interact();
            }
            if (hit.collider.TryGetComponent<SliderInteractable>(out var sliderinteractable))   // Legacy
            {
                sliderinteractable.SetValueFromHit(hit);
            }
        }
        //lineRenderer.SetPosition(0, rightController.position);
        //lineRenderer.SetPosition(1, rightController.position + rightController.forward * interactDistance);
    }

    // PC Version
    void TryInteractPC()
    {
        if (Camera.main == null) return;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        PerformRaycast(ray);

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactLayer))
        {
            Debug.Log($"Hit: {hit.collider.name}");

            if (hit.collider.TryGetComponent<SliderInteractable>(out var sliderinteractable))
            {
                sliderinteractable.SetValueFromHit(hit);
                Debug.LogWarning("We tried to move slider");
            }
        }
    }

    // PC Version
    void PerformRaycast(Ray ray)
    {
        Debug.DrawRay(ray.origin, ray.direction * interactDistance, Color.green, 2f);

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactLayer))
        {
            if (hit.collider.TryGetComponent<Interactable>(out var interactable))
            {
                interactable.Interact();
                return; 
            }

            if (hit.collider.TryGetComponent<PlayUI>(out var tile))
            {
                tile.OnClick_Toggle();
                return;
            }

            if (hit.collider.TryGetComponent<Button>(out var btn))
            {
                btn.onClick.Invoke();
                return;
            }

            if (hit.collider.TryGetComponent<Slider>(out var slider))
            {
               // slider.value += 0.1f;  // KLIM DISABLED DUE BUG
            }

        }
    }
}
