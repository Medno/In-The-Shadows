using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum soundType {
    global, music
};

public class AdaptSoundVolume : MonoBehaviour
{
    public soundType type;
    private PlayerSettings settings;
    private AudioSource audioSource;
    void Start()
    {
        settings = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<PlayerSettings>();
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (settings.muteAudio && audioSource.isPlaying)
        {
            audioSource.Stop();
            Debug.Log("Stopping sound");
        }
        if (type == soundType.global)
            audioSource.volume = settings.globalVolume;
        else if (type == soundType.music)
            audioSource.volume = settings.musicVolume * settings.globalVolume;
    }
}
