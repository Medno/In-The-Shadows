using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelSelection : MonoBehaviour
{
	public GameObject  level;
	public Level  levelDetails;
	public TextMesh levelStatusText;
	public bool	firstLevel;
	private string levelName;
	private int status;
	private AnimateClicked button;
	private TextMeshProUGUI levelText;
	void GetLevelStatus() {
		levelName = level.GetComponent<Level>().levelName;
		if (LevelManager.testMode)
		{
			status = 1;
			levelStatusText.text = "?";
		}
		else
		{
			if (firstLevel && !PlayerPrefs.HasKey(levelName + "_status"))
			{
				PlayerPrefs.SetInt(levelName + "_status", 1);
				PlayerPrefs.Save();
			}
			status = PlayerPrefs.GetInt(levelName + "_status");
			if (status == 2)
			{
				levelStatusText.text = "✓";
				Debug.Log(LevelManager.selectedLevel);
				if (LevelManager.selectedLevel && LevelManager.selectedLevel.name == transform.name)
				{
					GetComponent<UnlockCube>().LaunchUnlock();
					LevelManager.selectedLevel = null;
				}
				else
					GetComponent<UnlockCube>().firstCube.GetComponent<LoadNextCube>().SetScale();
			}
			else if (status == 1)
				levelStatusText.text = "?";
			else
				levelStatusText.text = "X";
		}
	}
	void Start () {
		levelText = GameObject.FindGameObjectWithTag("Level Selector Header").GetComponent<TextMeshProUGUI>();
		levelText.text = "";
		levelDetails = level.GetComponent<Level>();
		button = GetComponent<AnimateClicked>();
		GetLevelStatus();
		GetComponent<AnimateClicked>().functionToExecute.AddListener(TransitionManager.instance.TriggerTransition);
	}
	void LaunchLevel()
	{
    	SceneManager.LoadScene("Level");
	}
	IEnumerator SelectLevel()
	{
		LevelManager.selectedLevel = level;
		TransitionManager.instance.levelToLoad = "Level";
		yield return StartCoroutine(button.Animate());
	}
	void OnMouseDown()
    {
		if (status > 0 || LevelManager.testMode)
			StartCoroutine(SelectLevel());
	}
	void OnMouseOver()
	{
		levelText.text = status == 2 ? levelName : level.GetComponent<Level>().levelNameHint;
	}
	void OnMouseExit()
    {
        levelText.text = "";
    }
}
