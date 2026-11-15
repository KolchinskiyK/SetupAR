using UnityEngine;

// This Script enables leaving the AR App

public class CloseApp : MonoBehaviour, Interactable
{
    // Interactable Interface 
    public void Interact()
    {
        Debug.Log("EXIT Button gedrückt");
        ExitApplication(); 
    }

    // Debug feature showing, that the App can be leaved
    private void OnEnable()
    {
        Debug.Log("Exit ENABLED");
    }

    // Leave - Code
    private void ExitApplication()
    {
        
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        
        
        #else
            Application.Quit();
        #endif
    }
}