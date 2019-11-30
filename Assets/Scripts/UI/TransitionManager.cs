using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
	public static TransitionManager instance {get; private set;}
	private Animator animator;
	public Canvas canvas;
    public string levelToLoad;
    public bool quit {get; set;}

	void Awake () {
		instance = this;
	}
    void Start()
    {
        animator = GetComponent<Animator>();
        quit = false;
    }
    public void DisableCanvas()
    {
        canvas.enabled = false;
    }
    public void TriggerTransition()
    {
        canvas.enabled = true;
        animator.SetTrigger("Fade");
    }
    public void FadeScene()
    {
        if (quit)
            Application.Quit();
        SceneManager.LoadScene(levelToLoad);
    }
    public void SetLevelToLoad(string value)
    {
        instance.levelToLoad = value;
    }
}
