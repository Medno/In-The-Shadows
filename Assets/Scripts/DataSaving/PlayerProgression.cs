using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerProgression : MonoBehaviour
{
    private const string DataFilename = "saved.json";
    private GameManager gm;
    void Start()
    {
        gm = GetComponent<GameManager>();
        LoadProgression();
    }
    public void ResetProgression()
    {
        gm.score = 0;
        foreach (Level lvl in gm.levels)
        {
            if (lvl.firstLevel)
                lvl.levelStatus = Level.status.Available;
            else
                lvl.levelStatus = Level.status.Locked;
            lvl.score = 0;
        }
        SaveProgression();
    }
    public Data CreateProgressionData()
    {
        Data data = new Data();

        data.score = gm.score;
        foreach(Level level in gm.levels)
        {
            LevelData levelData = new LevelData();
            levelData.levelName = level.levelName;
            levelData.status = level.levelStatus;
            levelData.timeScore = level.score;
            data.levels.Add(levelData);
        }
        return data;
    }
    void LoadFromData(Data data)
    {
        gm.score = data.score;
        foreach(Level level in gm.levels)
        {
            LevelData levelData = data.levels.Find(lvlSaved => lvlSaved.levelName == level.levelName);
            if (levelData == null)
            {
                Debug.Log("Saved data corrupted, exiting...");
                ResetProgression();
                return ;
            }
            level.levelStatus = levelData.status;
            level.score = levelData.timeScore;
        }
    }
    public void SaveProgression()
    {
        Debug.Log("GameManager saving data...");
        Data data = CreateProgressionData();
        string json = JsonUtility.ToJson(data, true);
        string saveStatePath = Path.Combine(Application.persistentDataPath, DataFilename);
        Debug.Log("Writing: \n" + json);
        File.WriteAllText(saveStatePath, json);
    }
    public void LoadProgression()
    {
        Debug.Log(Application.persistentDataPath + "/" + DataFilename);
        // LoadVolume();
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
            SaveProgression();
        }
    }

    void Update()
    {
        
    }
}
