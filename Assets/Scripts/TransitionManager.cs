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

	void Awake () {
		instance = this;
	}
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void DisableCanvas()
    {
        canvas.enabled = false;
    }
    public void TriggerTransition()
    {
        Debug.Log("Transition triggered");
        canvas.enabled = true;
        animator.SetTrigger("Fade");
    }
    public void FadeScene()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
