using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndOfLevelMenu : MonoBehaviour
{
    private TextMeshProUGUI title;
    private Level level;
    void Start()
    {
        title = GetComponent<TextMeshProUGUI>();
        level = LevelManager.selectedLevel.GetComponent<Level>();
    }

    void Update()
    {
        if (level.levelStatus == Level.status.Done)
            title.text = level.levelName;
        else
            title.text = level.levelNameHint;
    }
}
