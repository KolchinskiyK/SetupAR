using UnityEngine;

//this script is used to store microphone data as a separate object with it's own attributes

[CreateAssetMenu(fileName = "New MicData", menuName = "Final System/Mic Data")]
public class MicData : ScriptableObject
{
    public string micName;
    public bool isStereo;

    [Header("Audio")]
    public AudioClip track1;
    public AudioClip track2;
    public AudioClip track3;

    [Header("Position")]
    public Vector3 position;
    public Vector3 rotation;

    [Header("UI")]
    public GameObject infoMenuPrefab; 
    [Header("UI Bilder")]
    public Sprite infoImage;
    [Header ("Custom Mic Icons")]
    public Sprite customIconActive;
    public Sprite customIconMuted;

}