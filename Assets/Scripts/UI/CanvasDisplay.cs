using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CanvasDisplay : MonoBehaviour
{
    public UnityEvent functionInClosing;
    public KeyCode  key;
    private Canvas canvas;
    void Start() {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
    }

    IEnumerator DisableCanvasAnimation()
    {
        FadePanel[] panels = GetComponents<FadePanel>();
        foreach (FadePanel panel in panels)
            panel.ExitFading();
        if (panels.Length > 0)
            yield return new WaitForSeconds(1.0f);
        canvas.enabled = false;
    }
    public void DisableCanvas()
    {
        StartCoroutine(DisableCanvasAnimation());
    }
    public void InvertCanvasMode()
    {
        if (canvas.enabled)
        {
            DisableCanvas();
            functionInClosing.Invoke();
        }
        else
            ActivePauseMenu();
    }
    public void EnableCanvas()
    {
        canvas.enabled = true;
    }
    void ActivePauseMenu()
    {
        EnableCanvas();
        FadePanel[] panels = GetComponents<FadePanel>();
        foreach (FadePanel panel in panels)
            panel.StartFading();

    }
    void Update()
    {
        if (!canvas.enabled && Input.GetKeyDown(key))
            ActivePauseMenu();
        else if (Input.GetKeyDown(key))
            DisableCanvas();
    }
}
