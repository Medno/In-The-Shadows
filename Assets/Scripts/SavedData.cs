using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public string levelName = "";
    public Level.status status = Level.status.Locked;
    public int timeScore = 0;
};
[System.Serializable]
public class SavedData
{
    public int score = 0;
    public List<LevelData> levels = new List<LevelData>();
};
