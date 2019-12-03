using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timer;
    private LevelLoader loader;

    void Start()
    {
        loader = GetComponent<LevelLoader>();
    }

    void Update()
    {
        if (!loader.finished)
        {
            int minutes = (int)Time.timeSinceLevelLoad / 60;
            int seconds = (int)Time.timeSinceLevelLoad % 60;
            string minutesString = minutes < 10 ? "0" + minutes : minutes.ToString();
            string secondsString = seconds < 10 ? "0" + seconds : seconds.ToString();
            timer.text = minutesString + ":" + secondsString;
        }
    }
}
