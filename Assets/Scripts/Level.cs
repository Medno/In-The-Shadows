using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public enum status {
        Locked = 1 << 0,
        Available = 1 << 1,
        Done = 1 << 2
    };
    public enum difficulty {
        ONE, TWO, THREE
    };
    public string levelName;
    public string levelNameHint;
    public Level nextLevel;
    public difficulty currentDifficulty;
    public status levelStatus = status.Locked;
    public int score = 0;
    public GameObject[]  objects;
    public bool firstLevel = false;
}
