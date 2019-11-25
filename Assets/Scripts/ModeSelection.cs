using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ModeSelection : MonoBehaviour
{
    private Button buttonMode;
	private AnimateClicked animateButton;
    IEnumerator LaunchGame()
    {
        if (tag == "Test Mode")
            LevelManager.testMode = true;
        else
            LevelManager.testMode = false;
        yield return StartCoroutine(animateButton.Animate());
        TransitionManager.instance.TriggerTransition();
    }
    void AnimateLaunching()
    {
        StartCoroutine(LaunchGame());
        LevelManager.testMode = true;
    }
    void Start()
    {
        buttonMode = GetComponent<Button>();
        buttonMode.onClick.AddListener(AnimateLaunching);
		animateButton = GetComponent<AnimateClicked>();
    }
}
