using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    void Start()
    {
        Instantiate(LevelManager.selectedLevel, LevelManager.selectedLevel.transform.position, Quaternion.identity);
    }
}
