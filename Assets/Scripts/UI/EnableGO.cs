using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableGO : MonoBehaviour
{
    public GameObject go;
    void Start()
    {
        go.SetActive(false);
    }
    public void Enable()
    {
        go.SetActive(true);
    }
}
