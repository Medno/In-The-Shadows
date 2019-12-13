using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressionBar : MonoBehaviour
{
    private Image progressBar;
    private List<Object> objs;
    private const float startProgressRotation = 40.0f;
    private const float startProgressPosition = 1.0f;
    void Start()
    {
        progressBar = GetComponent<Image>();
        objs = GameObject.FindGameObjectWithTag("Level Loader").GetComponent<LevelLoader>().objs;
    }
    float ComputeRotationProgression(Object obj)
    {
        return Mathf.Clamp((startProgressRotation + obj.offsetValidation - obj.currentAngle) / startProgressRotation, 0.0f, 1.0f);
    }
    float ComputePositionProgression(Object obj)
    {
        return Mathf.Clamp((startProgressPosition + obj.offsetPositionValidation) - Mathf.Abs(obj.expectedPosition.y - obj.transform.position.y) / startProgressPosition, 0.0f, 1.0f);
    }
    float ComputeObjProgression(Object obj)
    {
        float progression = 0.0f;

        progression += ComputeRotationProgression(obj);
        if (obj.level.currentDifficulty == Level.difficulty.THREE)
        {
            progression += ComputePositionProgression(obj);
            return progression / 2.0f;
        }
        return progression;
    }
    void UpdateProgressionBar()
    {
        float progression = 0.0f;

        foreach(Object obj in objs)
        {
            progression += ComputeObjProgression(obj);
        }
        progressBar.fillAmount = progression / objs.Count;
    }
    void Update()
    {
        UpdateProgressionBar();
    }
}
