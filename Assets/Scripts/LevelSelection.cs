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
	private Animator animator;
	public bool	firstLevel;
	private string levelName;
	private int status;
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
		else if (status == 1)
			levelStatusText.text = "?";
		else
			levelStatusText.text = "X";
	}
	void Start () {
		animator = GetComponent<Animator>();
		levelDetails = level.GetComponent<Level>();
		GetLevelStatus();
	}
	IEnumerator LaunchLevel()
	{
        LevelManager.selectedLevel = level;
		animator.SetTrigger("Clicked");
		Debug.Log("You pressed the button!");
		yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene("Level");
	}
	void OnMouseDown()
    {
		if (status > 0 || LevelManager.testMode)
			StartCoroutine(LaunchLevel());
	}
}
