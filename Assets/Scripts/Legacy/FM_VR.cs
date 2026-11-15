using UnityEngine;
using UnityEngine.InputSystem;

// LEGACY followMenu for AR -> fixed otherways
public class FM_VR : MonoBehaviour
{
    [Header("Input Action")]
    public InputActionReference openMenuAction;

    [Header("Menu")]
    public GameObject menuCanvas;

    private bool isMenuOpen;

    private void OnEnable()
    {
        openMenuAction.action.performed += ToggleMenu;
        openMenuAction.action.Enable();
    }

    private void OnDisable()
    {
        openMenuAction.action.performed -= ToggleMenu;
        openMenuAction.action.Disable();
    }

    private void ToggleMenu(InputAction.CallbackContext context)
    {
        isMenuOpen = !isMenuOpen;
        menuCanvas.SetActive(isMenuOpen);
    }
}

