using UnityEngine;
using UnityEngine.UI;

// This Script enables AR Interaction with Bass Micro values
public class plusminus : MonoBehaviour, Interactable
{
    public Scala s;                 // Legacy
    public bool plus = true;        // Legacy 
    public int value = 20;
    [Header("Reference")]
    public Bass bassController;     // Reference to Bass, to give new values
    
    [Header("Settings")]
    public bool isPlus = true;      // Does it represent a "+" or "-" Button?

    void Start()
    {
        //
    }
    void Update()
    {
        //
    }

    // Interactable Interface (AR)
    public void Interact()
    {
        // Debug.Log("Scala Added");
        // if (plus)
        // {
        //     s.Add(value);
        //     Debug.Log("Bass Value is " + s.sValue);
        // } 
        // else
        // {
        //     s.Sub(value);
        //     Debug.Log("Bass Value is " + s.sValue);
        // }

        if (bassController != null)
        {
            // In relation to button type it sets bass to +1 or -1
            int direction = isPlus ? 1 : -1;
            bassController.ChangeStep(direction);
            
            Debug.Log(isPlus ? "Bass +" : "Bass -");
        }
    }
}