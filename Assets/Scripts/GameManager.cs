﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public bool testMode = false;
    public Level[] levels;
    public int score = 0;
    private string savedDataFilename = "saved.json";
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Game Manager");
        if (objs.Length > 1)
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        LoadGame();
    }
    public SavedData CreateSavedData()
    {
        SavedData data = new SavedData();

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
        SavedData data = CreateSavedData();
        string json = JsonUtility.ToJson(data, true);
        Debug.Log(Path.Combine(Application.persistentDataPath.Replace(" ", "\\ "), "/" + savedDataFilename));
        string saveStatePath = Path.Combine(Application.persistentDataPath, savedDataFilename);
        File.WriteAllText(saveStatePath, json);
    }
    void LoadFromSavedData(SavedData data)
    {
        score = data.score;
        foreach(Level level in levels)
        {
            LevelData levelData = data.levels.Find(lvlSaved => lvlSaved.levelName == level.levelName);
            if (levelData == null)
            {
                Debug.Log("Saved data corrupted, exiting...");
                Application.Quit();
            }
            level.levelStatus = levelData.status;
            level.score = levelData.timeScore;
        }
    }
    public void LoadGame()
    {
        Debug.Log(Application.persistentDataPath + "/" + savedDataFilename);
        if (File.Exists(Path.Combine(Application.persistentDataPath, savedDataFilename)))
        {
            Debug.Log("SavedData file founded, loading data...");
            string saveFileString = File.ReadAllText(Application.persistentDataPath + "/" + savedDataFilename);
            SavedData saved = JsonUtility.FromJson<SavedData>(saveFileString);
            LoadFromSavedData(saved);
        }
        else
        {
            Debug.Log("SavedData file not founded, creating file...");
            SaveGame();
        }
    }
    void Update()
    {

    }
}
