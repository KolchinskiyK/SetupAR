using UnityEngine;
using UnityEngine.SceneManagement;

// This Script was made for out Demo to show simple Menu Interactions and Exiting function
public class MenuController : MonoBehaviour
{
    // Enabling Cursor to interact with it
    public class MenuCursor : MonoBehaviour
    {
        void OnEnable()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    // Opens the right scene with the most content
    public void OpenApp()
    {
        SceneManager.LoadScene("MenuTestScene"); 
    }

    // Abandoned feature
    public void OpenSettings()
    {
        // TO DO
        Debug.Log("Open Settings");
    }

    // Closing App (both in AR and Editor mode)
    public void ExitApp()
    {
        //UnityEditor.EditorApplication.isPlaying = false;

        //to avoid build errors
        #if UNITY_EDITOR
            // Wenn wir im Editor sind: Stoppe den Play-Modus
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // Wenn wir auf der Brille sind: Schließe die App richtig
            Application.Quit();
        #endif
        Application.Quit();
        Debug.Log("Game Quit");
    }

}