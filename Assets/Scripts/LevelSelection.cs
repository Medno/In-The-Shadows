using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
	public GameObject  level;
	public Level  levelDetails;
	public TextMesh levelStatusText;
	public bool	firstLevel;
	private string levelName;
	private int status;
	private AnimateClicked button;
	void GetLevelStatus() {
		levelName = level.GetComponent<Level>().levelName;
		if (firstLevel && !PlayerPrefs.HasKey(levelName + "_status"))
		{
			PlayerPrefs.SetInt(levelName + "_status", 1);
            PlayerPrefs.Save();
		}
		status = PlayerPrefs.GetInt(levelName + "_status");
		if (status == 2)
			levelStatusText.text = "✓";
		else if (status == 1 || LevelManager.testMode)
			levelStatusText.text = "?";
		else
			levelStatusText.text = "X";
	}
	void Start () {
		levelDetails = level.GetComponent<Level>();
		button = GetComponent<AnimateClicked>();
		GetLevelStatus();
	}
	void LaunchLevel()
	{
    SceneManager.LoadScene("Level");
	}
	IEnumerator SelectLevel()
	{
		LevelManager.selectedLevel = level;
		yield return StartCoroutine(button.Animate());
		Debug.Log("You pressed the button!");
		TransitionManager.instance.TriggerTransition();
	}
	void OnMouseDown()
    {
		if (status > 0 || LevelManager.testMode)
			StartCoroutine(SelectLevel());
	}
}
