using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[]  clips;
    private AudioSource audioSource;
    private GameManager gm;
    private int indexClip;
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");
        if (objs.Length > 1)
            Destroy(this.gameObject);
        indexClip = Random.Range(0, clips.Length);
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gm = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
    }
    void Update()
    {
        if (!audioSource.isPlaying && !gm.muteAudio)
        {
            Debug.Log("Play");
            audioSource.clip = clips[indexClip % clips.Length];
            indexClip++;
            audioSource.Play();
        }
    }
}
