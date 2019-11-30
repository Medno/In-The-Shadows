using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeSelection : MonoBehaviour
{
    private GameManager gameManager;
    private Button buttonMode;
	private AnimateClicked animateButton;
    IEnumerator LaunchGame()
    {
        if (tag == "Test Mode")
            gameManager.testMode = true;
        else
            gameManager.testMode = false;
        yield return StartCoroutine(animateButton.Animate());
    }
    void AnimateLaunching()
    {
        StartCoroutine(LaunchGame());
    }
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        buttonMode = GetComponent<Button>();
        buttonMode.onClick.AddListener(AnimateLaunching);
		animateButton = GetComponent<AnimateClicked>();
    }
}
