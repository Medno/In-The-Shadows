using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenPlaylist : MonoBehaviour
{
    public AudioClip[]  clips;
    private int indexClip;
    private GameManager gm;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gm = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        indexClip = Random.Range(0, clips.Length);
    }
    public void PlayIfUnmute()
    {
        if (!audioSource.isPlaying && !gm.muteAudio)
        {
            audioSource.clip = clips[indexClip % clips.Length];
            indexClip++;
            audioSource.Play();
        }
    }
    void Update()
    {
        PlayIfUnmute();
    }
}
