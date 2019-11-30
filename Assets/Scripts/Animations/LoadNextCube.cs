using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNextCube : MonoBehaviour
{
    public GameObject nextCube;
    public bool rescaled = false;

    void SetRescaled()
    {
        rescaled = true;
    }
    void CreateNext()
    {
        if (nextCube)
            nextCube.GetComponent<Animator>().SetTrigger("Initialization");
    }
    public void SetScale()
    {
        GetComponent<Animator>().SetTrigger("Initialized");
        if (nextCube)
            nextCube.GetComponent<LoadNextCube>().SetScale();
    }
}
