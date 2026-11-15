using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Android;

public class MicGenerator : MonoBehaviour
{
    [Header("Referenzen")]
    public Transform messpunkt;
    public GameObject monoPrefab; 
    public GameObject stereoPrefab;

    public GameObject parent;

    [Header("Daten")]
    public List<MicData> allMicData; 

    // Audio management
    private List<AudioSource> audioSources = new List<AudioSource>();
    private List<bool> isMuted = new List<bool>();
    private float bassVolume = 1.0f;

    private int counter = 0;
    
    private int currentTrackIndex = -1;

    // Singleton for UI access
    public static MicGenerator Instance;

    void Awake() 
    { 
        Instance = this; 
    }

    void Start()
    {
        SpawnAllMics();
        if (isMuted.Count > 0)
        {
            isMuted[0] = false;
            Debug.Log("Start: Mikrofon 0 wurde automatisch aktiviert.");
        }
        UpdateVolumes();
    }

    void SpawnAllMics()
    {
        foreach (var data in allMicData)
        {
           //Decision: is it mono or stereo?
            GameObject prefabToUse = data.isStereo ? stereoPrefab : monoPrefab;

            //Calculation of position
            Vector3 worldPos = messpunkt.TransformPoint(data.position);
            Quaternion worldRot = Quaternion.Euler(data.rotation);

            //Spawn
            GameObject newMic = Instantiate(prefabToUse, worldPos, worldRot);
            newMic.transform.SetParent(parent.transform); 

            //Transmitting data
            StaticMic micScript = newMic.GetComponent<StaticMic>();
            if (micScript != null)
            {
                micScript.Setup(data, counter, messpunkt);
            }
            else
            {
                Debug.LogError("Fehler: Auf dem Prefab fehlt das 'StaticMic' Skript!");
            }

            //Creating audio source
            CreateAudioSourceForMic(data);
            counter++;
        }
        Debug.Log("I created so much!" + counter);
        Debug.Log("audioSources.Count(spawn): " + audioSources.Count);
    }

    void CreateAudioSourceForMic(MicData data)
    {
        GameObject audioObj = new GameObject("Audio_" + data.micName);
        audioObj.transform.SetParent(parent.transform);
        
        AudioSource source = audioObj.AddComponent<AudioSource>();
        source.playOnAwake = false;
        source.loop = true;

        // 0.0f = 2D Sound (same volume everywhere), 1.0f = 3D Sound (quieter when far away)
        // set to 2D here to have it sound the same everywhere
        source.spatialBlend = 0f; 
        
        audioSources.Add(source);
        Debug.Log("I Added!" + source);
        isMuted.Add(true); // start muted
    }

    // audio navigation

    public void PlayTrack(int trackID)
    {
        //toggle logic for playing/stopping a track
        if (currentTrackIndex == trackID)
        {
            StopAllAudio();
            currentTrackIndex = -1; // Reset
            Debug.Log("Track gestoppt (Toggle).");
            return;
        }

        StopAllAudio();

        Debug.Log("Trying to Play!" + trackID);
        Debug.Log("allMicData.Count: " + allMicData.Count);
        Debug.Log("audioSources.Count:(play) " + audioSources.Count);

        // sync playback time if any track was already running (seamless transition)
        float syncTime = (audioSources.Count > 0 && audioSources[0].isPlaying) ? audioSources[0].time : 0;

        for (int i = 0; i < allMicData.Count; i++)
        {
            AudioClip clip = null;
            if (trackID == 1) clip = allMicData[i].track1;
            if (trackID == 2) clip = allMicData[i].track2;
            if (trackID == 3) clip = allMicData[i].track3;

            if (clip != null)
            {
                //Debug.Log("Mistake should be here!" + audioSources[0]);
                audioSources[i].clip = clip;
                audioSources[i].time = syncTime;
                audioSources[i].Play();
            }
        }
        UpdateVolumes();
        currentTrackIndex = trackID;
        Debug.Log("Playing track!" + trackID);
    }

    public void StopAllAudio()
    {
        foreach (var source in audioSources)
        {
            if (source != null) source.Stop();
        }
    }

    public bool IsTrackPlaying(int trackID)
    {
        return currentTrackIndex == trackID;
    }

    //toggle logic for muting/activating specific microphone on launchpad
    public void ToggleMic(int index)
    {
        isMuted[index] = !isMuted[index];
        UpdateVolumes();
        Debug.Log("ToggleMic toggled!" + index);
        Debug.Log(index + " is now " + isMuted[index]);
    }

    public void SetBassVolume(float val)
    {
        bassVolume = val;
        UpdateVolumes();
    }

    public bool IsMicActive(int index) 
    {
        //Debug.Log("i am testing if its muted on  " + index);
        //foreach (bool b in isMuted)
        //{ Debug.Log(b); }
        //Debug.Log(isMuted);

        // used by StaticMic.cs to update visuals (billboard icons)
        return !isMuted[index];
    }

    public void SetBassState(float volume)
    {
        bassVolume = volume;

        for (int i = 0; i < allMicData.Count; i++)
        {
            if (allMicData[i].micName.ToLower().Contains("bass") || allMicData[i].micName.Contains("D112"))
            {
                
                bool shouldBeMuted = (volume <= 0.01f);
                
                isMuted[i] = shouldBeMuted;
            }
        }
        UpdateVolumes();
    }


    //apply calculated volume to all AudioSources based on mute state
    void UpdateVolumes()
    {
        for (int i = 0; i < audioSources.Count; i++)
        {
            if (isMuted[i]) audioSources[i].volume = 0;
            else
            {
                // Bass check 
                if (allMicData[i].micName.ToLower().Contains("bass") || allMicData[i].micName.Contains("D112"))
                    audioSources[i].volume = bassVolume;
                else
                    audioSources[i].volume = 1.0f;
            }
        }
    }
}