using System;
using UnityEditor;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;


//This script is linked to the UI information tables of the microphones
public class InfoDisplayManager : MonoBehaviour
{
    public static InfoDisplayManager Instance;
    public Transform playerCamera; 
    private GameObject currentMenu;

    void Awake() { Instance = this; }

    void OnEnable()
    {
        Debug.Log($"Enabled: {name}\n{System.Environment.StackTrace}");
    }


    public void ShowInfo(GameObject prefab, Vector3 pos)
    {

        //Clean up
        if (currentMenu != null) Destroy(currentMenu);
        
        // Spawn Menu
        currentMenu = Instantiate(prefab, pos + Vector3.up * 0.8f, Quaternion.identity);

        //Vector3 newPos = currentMenu.transform.position;
        //newPos.y += 2f;
        //currentMenu.transform.position = newPos;

        //logic for positioning
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0f;
        cameraForward.Normalize();

        Vector3 cameraRight = Camera.main.transform.right;
        cameraRight.y = 0f;
        cameraRight.Normalize();

        //calculate offset
        Vector3 offset =
            cameraRight * 0f +
            Vector3.up * 2f;


        //apply calculated position
        currentMenu.transform.position = pos + offset;

        Vector3 lookDir = currentMenu.transform.position - Camera.main.transform.position;
        lookDir.y = 0f;

        //Apply rotation
        if (lookDir.sqrMagnitude > 0.001f) currentMenu.transform.rotation = Quaternion.LookRotation(lookDir);


        //

        //if (playerCamera != null)
           // currentMenu.transform.LookAt(currentMenu.transform.position + playerCamera.forward);
       // else
            //Debug.Log("No PlayerCamera");
    }
}