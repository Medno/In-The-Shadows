using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ModeSelection : MonoBehaviour
{
    public Button playMode;
    public Button testMode;
    void LaunchLevelSelection()
    {
        SceneManager.LoadScene("Level Selection");
    }
    void LaunchPlayMode()
    {
        LevelManager.testMode = false;
        LaunchLevelSelection();
    }
    void LaunchTestMode()
    {
        LevelManager.testMode = true;
        LaunchLevelSelection();

    }
    void Start()
    {
        playMode.onClick.AddListener(LaunchPlayMode);
        testMode.onClick.AddListener(LaunchTestMode);
    }

    void Update()
    {
        
    }
}
