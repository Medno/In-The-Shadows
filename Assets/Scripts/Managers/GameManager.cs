using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public bool testMode = false;
    public Level[] levels;
    public int score = 0;
    private string savedDataFilename = "saved.json";
    public Level selectedLevel;
    public Vector3 selectedLevelPosition;
    public bool animatingNext = false;
    public float globalVolume {get; set;}
    public float musicVolume {get; set;}
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
        PlayerPrefs.DeleteAll();
        LoadVolume();
        SaveGame();
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
        string saveStatePath = Path.Combine(Application.persistentDataPath, savedDataFilename);
        Debug.Log("Writing: \n" + json);
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
        globalVolume = LoadSoundVolume("GlobalVolume");
        musicVolume = LoadSoundVolume("MusicVolume");
        Debug.Log("Global volume set to : " + globalVolume);
        Debug.Log("Music volume set to : " + musicVolume);
    }
    void SaveVolume()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("GlobalVolume", globalVolume);
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
        LoadVolume();
    }
    public void EditLevelData(Level level)
    {
        Level levelGM = null;
        foreach(Level lvl in levels)
            if (lvl.levelName == level.levelName)
            {
                levelGM = lvl;
                break;
            }
        if (levelGM)
        {
            if (levelGM.levelStatus != level.levelStatus)
                levelGM.levelStatus = level.levelStatus;
            if (levelGM.score < level.score)
                levelGM.score = level.score;
        }
    }
}
