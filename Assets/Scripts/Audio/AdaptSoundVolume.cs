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
    private GameManager gm;
    private AudioSource audioSource;
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (gm.muteAudio && audioSource.isPlaying)
        {
            audioSource.Stop();
            Debug.Log("Stopping sound");
        }
        if (type == soundType.global)
            audioSource.volume = gm.globalVolume;
        else if (type == soundType.music)
            audioSource.volume = gm.musicVolume * gm.globalVolume;
    }
}
