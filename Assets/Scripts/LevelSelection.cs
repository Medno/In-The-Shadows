using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
	public GameObject  level;
	public Level  levelDetails;
	public TextMesh levelStatusText;
	public bool	firstLevel;
	public string levelName;
	public int status;
	public AnimateClicked	cube;
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
		levelDetails = level.GetComponent<Level>();
		GetLevelStatus();
		cube.functionToExecute.AddListener(TransitionManager.instance.TriggerTransition);
	}
	void LaunchLevel()
	{
    	SceneManager.LoadScene("Level");
	}
}
