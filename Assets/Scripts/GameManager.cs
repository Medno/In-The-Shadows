using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool testMode = false;
    public Level[] levels;
    public int score = 0;
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Game Manager");
        if (objs.Length > 1)
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }
    public SavedData CreateSavedData()
    {
        SavedData data = new SavedData();

        data.score = score;
        foreach(Level level in levels)
        {
            LevelData levelData = new LevelData();
            levelData.levelName = level.levelName;
            if (level.firstLevel)
                levelData.status = status.Available;
            else
                levelData.status = status.Locked;
            levelData.timeScore = 0;
            data.levels.Add(levelData);
        }
        return data;
    }
    public void SaveGame()
    {
        SavedData data = CreateSavedData();
        string json = JsonUtility.ToJson(data);
    }
    void Update()
    {

    }
}
