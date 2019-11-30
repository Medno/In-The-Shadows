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
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        Debug.Log(gm);
    }
    void Update()
    {
        if (type == soundType.global)
            GetComponent<AudioSource>().volume = gm.globalVolume;
        else if (type == soundType.music)
            GetComponent<AudioSource>().volume = gm.musicVolume * gm.globalVolume;
    }
}
