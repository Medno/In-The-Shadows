using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
    public bool muteAudio {get; set;}
    public float globalVolume {get; set;}
    public float musicVolume {get; set;}
    public Resolution resolution;
    void Start()
    {
        LoadVolume();
    }
    float LoadSoundVolume(string key)
    {
        float volume = 0.5f;

        if (PlayerPrefs.HasKey(key))
            volume = PlayerPrefs.GetFloat(key);
        else
            PlayerPrefs.SetFloat(key, volume);
        return volume;
    }

    void LoadVolume()
    {
        Debug.Log("Mute is saved: " + PlayerPrefs.HasKey("Mute"));
        if (PlayerPrefs.HasKey("Mute"))
            muteAudio = PlayerPrefs.GetInt("Mute") == 1 ? true : false;
        else
        {
            muteAudio = false;
            PlayerPrefs.SetInt("Mute", 0);
        }
        globalVolume = LoadSoundVolume("GlobalVolume");
        musicVolume = LoadSoundVolume("MusicVolume");
        Debug.Log(muteAudio);
    }
    public void SaveVolume()
    {
        int muteValue = muteAudio ? 1 : 0;
        PlayerPrefs.SetInt("Mute", muteValue);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("GlobalVolume", globalVolume);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
