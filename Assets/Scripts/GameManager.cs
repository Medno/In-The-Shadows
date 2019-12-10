using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool testMode = false;
    public Level[] levels;
    public int score = 0;
    public Level selectedLevel;
    public Vector3 selectedLevelPosition = Vector3.zero;
    public bool animatingNext = false;
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Game Manager");
        if (objs.Length > 1)
            Destroy(this.gameObject);
        DontDestroyOnLoad(objs[0]);
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
