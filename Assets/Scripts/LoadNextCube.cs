using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNextCube : MonoBehaviour
{
    public GameObject nextCube;

    void CreateNext()
    {
        if (nextCube)
            nextCube.GetComponent<Animator>().SetTrigger("Initialization");
    }
    public void SetScale()
    {
        Debug.Log("Trigger");
        GetComponent<Animator>().SetTrigger("Initialized");
        if (nextCube)
            nextCube.GetComponent<LoadNextCube>().SetScale();
    }
}
