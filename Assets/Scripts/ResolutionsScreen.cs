using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionsScreen : MonoBehaviour
{
    private Resolution[] resolutions;
    void Start()
    {
        resolutions = Screen.resolutions;
        foreach(Resolution res in resolutions)
            Debug.Log(res);
    }

    void Update()
    {

    }
}
