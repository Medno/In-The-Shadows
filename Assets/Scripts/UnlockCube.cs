using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockCube : MonoBehaviour
{
    public GameObject firstCube;
    public void LaunchUnlock()
    {
        firstCube.GetComponent<Animator>().SetTrigger("Initialization");
    }

}
