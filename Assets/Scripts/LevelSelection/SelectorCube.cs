﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectorCube : MonoBehaviour
{
    private GameManager gameManager;
	[HideInInspector] public Level level;
	public TextMesh levelStatusText;
	public Clicked cube;
	public SelectorCube next;
	public bool willBeUnlock = false;
	void GetTextLevelStatus()
	{
		if (gameManager.testMode)
		{
			levelStatusText.text = "?";
			return;
		}
		if (level.levelStatus == Level.status.Locked || willBeUnlock)
			levelStatusText.text = "X";
		else if (level.levelStatus == Level.status.Available)
			levelStatusText.text = "?";
		else if (level.levelStatus == Level.status.Done)
			levelStatusText.text = "✓";
	}
	void GetLevelStatus() {
		if (!gameManager.testMode)
		{
			if (level.levelStatus == Level.status.Done)
			{
				if (gameManager.animatingNext && gameManager.selectedLevel.levelName == level.levelName)
				{
					UnlockAnimation();
					gameManager.selectedLevel = null;
					next.willBeUnlock = true;
					gameManager.animatingNext = false;
				}
				else if (level.nextLevel)
					GetComponent<UnlockCube>().firstCube.GetComponent<LoadNextCube>().SetScale();
			}
		}
		GetTextLevelStatus();
	}
	void UnlockAnimation()
	{
		GetComponent<UnlockCube>().LaunchUnlock();
	}
	public void UpsizeCubeAnimation()
	{
		Debug.Log("Upsizing..." + transform.name);
		cube.GetComponent<Animator>().SetTrigger("Upsize");
	}
	void Start () {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
		Level[] levels = gameManager.GetComponent<GameManager>().levels;
		foreach (Level lvl in levels)
			if (lvl.levelName == name)
			{
				level = lvl;
				break;
			}
		GetLevelStatus();
		cube.functionToExecute.AddListener(TransitionManager.instance.TriggerTransition);
	}
	void LaunchLevel()
	{
    	SceneManager.LoadScene("Level");
	}
}
