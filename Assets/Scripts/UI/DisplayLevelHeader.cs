using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayLevelHeader : MonoBehaviour
{
    private TextMeshProUGUI title;
    public LevelLoader levelLoader;
    private Level level;
    void Start()
    {
        title = GetComponent<TextMeshProUGUI>();
        level = levelLoader.level;
    }
    void Update()
    {
        if (level)
        {
            if (levelLoader.finished)
                title.text = level.levelName;
            else
                title.text = level.levelNameHint;
        }
    }
}
