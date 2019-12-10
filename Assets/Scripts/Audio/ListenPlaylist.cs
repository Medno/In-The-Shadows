using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenPlaylist : MonoBehaviour
{
    public AudioClip[]  clips;
    private int indexClip;
    private PlayerSettings settings;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        settings = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<PlayerSettings>();
        indexClip = Random.Range(0, clips.Length);
    }
    public void PlayIfUnmute()
    {
        if (!audioSource.isPlaying && !settings.muteAudio)
        {
            audioSource.clip = clips[indexClip % clips.Length];
            indexClip++;
            audioSource.Play();
        }
    }
    void Update()
    {
        if (tag != "Music")
            PlayIfUnmute();
    }
}
