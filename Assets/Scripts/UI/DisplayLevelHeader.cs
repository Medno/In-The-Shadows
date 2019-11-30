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
        Debug.Log(levelLoader);
        level = levelLoader.level;
        Debug.Log(levelLoader);
        Debug.Log(level);
        //Display obj name at the end
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
