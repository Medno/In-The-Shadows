using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    private const float validationTimer = 1.0f;
    [HideInInspector] public float offsetValidation = 6.0f;
    [HideInInspector] public float offsetPositionValidation = 0.5f;
    private float    currentTimer;
    public Level level;
    public Vector3  expectedRotation;
    public Vector3  expectedPosition;
    public bool finished = false;
    public float currentAngle;
    void Start()
    {
        currentTimer = validationTimer;
        ComputeAngle();
    }
    void ObjectDone()
    {
        finished = true;
        Debug.Log("Finished");
    }
    void ComputeAngle()
    {
        Quaternion current = Quaternion.Euler(transform.rotation.eulerAngles);
        Quaternion expected = Quaternion.Euler(expectedRotation);

        float angle = Quaternion.Angle(current, expected);
        currentAngle = Mathf.Abs(angle);
    }
    bool CheckMatchOffset() {
        ComputeAngle();
        bool sameRotation = currentAngle < offsetValidation;
        if (level.currentDifficulty == Level.difficulty.THREE)
            sameRotation = sameRotation && expectedPosition.y - offsetPositionValidation < transform.position.y && transform.position.y < expectedPosition.y + offsetPositionValidation;
        return sameRotation;
    }
    void CheckValidation() {
        bool validation = CheckMatchOffset();

        if (validation && currentTimer > 0.0f)
            currentTimer -= Time.deltaTime;
        else if (validation)
            ObjectDone();
        else if (currentTimer != validationTimer)
            currentTimer = validationTimer;
    }
    void Update()
    {
        if (!finished)
            CheckValidation();
    }
}
