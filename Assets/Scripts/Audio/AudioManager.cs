using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    private ListenPlaylist playlist;
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");
        if (objs.Length > 1)
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playlist = GetComponent<ListenPlaylist>();
    }
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Level" && audioSource.isPlaying)
            audioSource.Stop();
        else if (SceneManager.GetActiveScene().name != "Level")
            playlist.PlayIfUnmute();
    }
}
