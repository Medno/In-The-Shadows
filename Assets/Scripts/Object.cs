using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    public Vector3  expectedRotation;
    public Vector3  expectedPosition;
    private float    validationTimer = 1.0f;
    private float    currentTimer;
    private int offsetValidation = 5;
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
    }
    bool CheckMatchOffset(float input, float expected) {
        return (expected - offsetValidation < input && input < expected + offsetValidation);
    }
    void CheckValidation() {
        bool validation = false;
        validation = CheckMatchOffset(transform.rotation.eulerAngles.y, expectedRotation.y);
        if (level.currentDifficulty > Level.difficulty.ONE)
            validation = validation && CheckMatchOffset(transform.rotation.eulerAngles.x, expectedRotation.x);
        if (level.currentDifficulty > Level.difficulty.TWO)
            validation = validation && CheckMatchOffset(transform.position.y, expectedPosition.y);
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
