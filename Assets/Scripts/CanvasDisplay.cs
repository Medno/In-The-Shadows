using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasDisplay : MonoBehaviour
{
    public KeyCode  key;
    private Canvas canvas;
    public bool persistent = false;

    void Start() {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
    }

    void Update()
    {
        if (!canvas.enabled && Input.GetKeyDown(key))
            canvas.enabled = true;
        else if (Input.GetKeyDown(key) && !persistent)
            canvas.enabled = false;
    }
}
