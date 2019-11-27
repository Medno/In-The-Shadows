using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    public Vector3  expectedRotation;
    public Vector3  expectedPosition;
    private float    validationTimer = 1.0f;
    private float    currentTimer;
    private int offsetValidation = 6;
    private int offsetPositionValidation = 2;
    [HideInInspector] public bool finished = false;
    [HideInInspector] public Level level;
    void Start()
    {
        currentTimer = validationTimer;
        level = GameObject.FindObjectOfType<Level>();
    }
    void ObjectDone()
    {
        finished = true;
        Debug.Log("Finished");
    }
    bool CheckMatchOffset() {
        Quaternion current = Quaternion.Euler (transform.rotation.eulerAngles);
        Quaternion expected = Quaternion.Euler (expectedRotation);

        float angle = Quaternion.Angle (current, expected);

        bool sameRotation = Mathf.Abs(angle) < offsetValidation;
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
