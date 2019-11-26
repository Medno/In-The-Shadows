using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [HideInInspector] public GameObject level;
    void Awake()
    {
        if (LevelManager.selectedLevel)
            level = Instantiate(LevelManager.selectedLevel, LevelManager.selectedLevel.transform.position, Quaternion.identity);
        else
            Debug.Log("There is no level to load");
    }
}
