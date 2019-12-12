using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuLight : MonoBehaviour
{
    void Start()
    {
        GetComponent<Animator>().SetBool("Move 0", true);
    }
}
