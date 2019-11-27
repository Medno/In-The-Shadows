using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayLevelHeader : MonoBehaviour
{
    private TextMeshProUGUI title;
    public GameObject levelLoader;
    private Level level;

    void Start()
    {
        title = GetComponent<TextMeshProUGUI>();
        Debug.Log(levelLoader.GetComponent<LevelLoader>());
        level = levelLoader.GetComponent<LevelLoader>().level.GetComponent<Level>();
    }

    void Update()
    {
        if (level)
        {
            if (level.levelStatus == Level.status.Done)
                title.text = level.levelName;
            else
                title.text = level.levelNameHint;
        }
    }
}
