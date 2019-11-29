using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockCube : MonoBehaviour
{
    public GameObject firstCube;
    public LoadNextCube lastCube;
    private bool animationDone = false;
    public void LaunchUnlock()
    {
        firstCube.GetComponent<Animator>().SetTrigger("Initialization");
    }
    void Update()
    {
        if (!animationDone && lastCube.rescaled)
        {
            GetComponent<SelectorCube>().next.UpsizeCubeAnimation();
            animationDone = true;
        }
    }
}
