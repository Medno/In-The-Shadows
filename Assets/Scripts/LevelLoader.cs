using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    void Start()
    {
        if (LevelManager.selectedLevel)
            Instantiate(LevelManager.selectedLevel, LevelManager.selectedLevel.transform.position, Quaternion.identity);
        else
            Debug.Log("There is no level to load");
    }
}
