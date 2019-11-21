using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public enum status {
        Locked = 1 << 1,
        Available = 1 << 2,
        Done = 1 << 3
    };
    public enum level {
        ONE, TWO, THREE
    };
    private string levelName;
    public MeshRenderer mesh;
    public level difficulty;
    public status levelStatus;
    public Vector3  finalRotation;

    void Start()
    {

    }

    void CheckRotation() {
        if ()
    }
    void Update()
    {
        CheckRotation();
    }
}
