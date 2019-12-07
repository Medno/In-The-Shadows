﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public bool testMode = false;
    public Level[] levels;
    public int score = 0;
    private string DataFilename = "saved.json";
    public Level selectedLevel;
    public Vector3 selectedLevelPosition = Vector3.zero;
    public bool animatingNext = false;
    public bool muteAudio {get; set;}
    public float globalVolume {get; set;}
    public float musicVolume {get; set;}
    public Resolution resolution;
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Game Manager");
        if (objs.Length > 1)
            Destroy(this.gameObject);
        DontDestroyOnLoad(objs[0]);
    }
    void Start()
    {
        LoadGame();
    }
    public void ResetData()
    {
        score = 0;
        foreach (Level lvl in levels)
        {
            if (lvl.firstLevel)
                lvl.levelStatus = Level.status.Available;
            else
                lvl.levelStatus = Level.status.Locked;
            lvl.score = 0;
        }
        SaveGame();
    }
    public Data CreateData()
    {
        Data data = new Data();

        data.score = score;
        foreach(Level level in levels)
        {
            LevelData levelData = new LevelData();
            levelData.levelName = level.levelName;
            levelData.status = level.levelStatus;
            levelData.timeScore = level.score;
            data.levels.Add(levelData);
        }
        return data;
    }
    public void SaveGame()
    {
        Debug.Log("GameManager saving data...");
        Data data = CreateData();
        string json = JsonUtility.ToJson(data, true);
        string saveStatePath = Path.Combine(Application.persistentDataPath, DataFilename);
        Debug.Log("Writing: \n" + json);
        File.WriteAllText(saveStatePath, json);
    }
    void LoadFromData(Data data)
    {
        score = data.score;
        foreach(Level level in levels)
        {
            LevelData levelData = data.levels.Find(lvlSaved => lvlSaved.levelName == level.levelName);
            if (levelData == null)
            {
                Debug.Log("Saved data corrupted, exiting...");
                ResetData();
                return ;
            }
            level.levelStatus = levelData.status;
            level.score = levelData.timeScore;
        }
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
    public void LoadGame()
    {
        Debug.Log(Application.persistentDataPath + "/" + DataFilename);
        LoadVolume();
        if (File.Exists(Path.Combine(Application.persistentDataPath, DataFilename)))
        {
            Debug.Log("Data file founded, loading data...");
            string saveFileString = File.ReadAllText(Application.persistentDataPath + "/" + DataFilename);
            Data saved = JsonUtility.FromJson<Data>(saveFileString);
            LoadFromData(saved);
        }
        else
        {
            Debug.Log("Data file not founded, creating file...");
            SaveGame();
        }
    }
    void ComputeScore(Level levelGM, Level.status newStatus)
    {
        if (levelGM.levelStatus != Level.status.Done)
            score += 1000;
        if (levelGM.levelStatus != newStatus)
            levelGM.levelStatus = newStatus;
        int time = (int)Time.timeSinceLevelLoad;
        if (time < levelGM.score || levelGM.score == 0)
        {
            score += (120 - (time - levelGM.score) * (1 + (int)(0.5 * (int)levelGM.currentDifficulty)));
            levelGM.score = time;
        }
    }
    void UnlockNext(Level level)
    {
        level.levelStatus = Level.status.Available;
        animatingNext = true;
    }
    public void EditLevelData(Level level, Level.status newStatus)
    {
        if (level && newStatus == Level.status.Done)
            ComputeScore(level, newStatus);
        else if (level.levelStatus < Level.status.Available && newStatus == Level.status.Available)
            UnlockNext(level);
    }
}
