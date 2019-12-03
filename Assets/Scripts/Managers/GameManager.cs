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
    public bool muteAudio {get; set;}
    public float globalVolume {get; set;}
    public float musicVolume {get; set;}
    private bool quit = false;
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
        if (PlayerPrefs.HasKey("Mute"))
            muteAudio = PlayerPrefs.GetInt("Mute") == 1 ? true : false;
        else
        {
            muteAudio = false;
            PlayerPrefs.SetInt("Mute", 0);
        }
        globalVolume = LoadSoundVolume("GlobalVolume");
        musicVolume = LoadSoundVolume("MusicVolume");
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
        Debug.Log(Application.persistentDataPath + "/" + savedDataFilename);
        LoadVolume();
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
    public void EditLevelData(Level level, Level.status newStatus)
    {
        Level levelGM = null;
        foreach(Level lvl in levels)
            if (lvl.levelName == level.levelName)
            {
                levelGM = lvl;
                break;
            }
        if (levelGM && newStatus == Level.status.Done)
        {
            if (levelGM.levelStatus != Level.status.Done)
                score += 1000;
            if (levelGM.levelStatus != newStatus)
                levelGM.levelStatus = newStatus;
            int time = (int)Time.timeSinceLevelLoad;
            if (time < levelGM.score || levelGM.score == 0)
                score += (120 - (time - levelGM.score) * (1 + (int)(0.5 * (int)levelGM.currentDifficulty)));
            levelGM.score = time;
        }
        else if (levelGM.levelStatus != Level.status.Available && newStatus == Level.status.Available)
        {
            levelGM.levelStatus = level.levelStatus;
            levelGM.levelStatus = Level.status.Available;
            animatingNext = true;
        }
    }
    public void QuitGame()
    {
        Debug.Log("Should quit");
        quit = true;
    }
    void Update()
    {
        if (quit)
            Application.Quit();
    }
}
