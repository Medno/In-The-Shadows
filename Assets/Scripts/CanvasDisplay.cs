using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasDisplay : MonoBehaviour
{
    public KeyCode  key;
    private Canvas canvas;
    public bool persistent = false;
    public GameObject panel;

    void Start() {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
    }

    IEnumerator DisableCanvasAnimation()
    {
        panel.GetComponent<Animator>().SetTrigger("ExitMenu");
        yield return new WaitForSeconds(1.0f);
        canvas.enabled = false;
    }
    public void DisableCanvas()
    {
        StartCoroutine(DisableCanvasAnimation());
    }
    public void EnableCanvas()
    {
        canvas.enabled = true;
        panel.GetComponent<Animator>().SetTrigger("StartMenu");
    }
    void Update()
    {
        if (!canvas.enabled && Input.GetKeyDown(key))
            EnableCanvas();
        else if (Input.GetKeyDown(key) && !persistent)
            DisableCanvas();
    }
}
