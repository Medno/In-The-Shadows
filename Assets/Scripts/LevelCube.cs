using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelCube : MonoBehaviour
{
    public LevelSelection levelLinked;
    private TextMeshProUGUI levelText;
    IEnumerator SelectLevel()
	{
		LevelManager.selectedLevel = levelLinked.level;
        Debug.Log(LevelManager.selectedLevel);
		TransitionManager.instance.levelToLoad = "Level";
		yield return StartCoroutine(GetComponent<AnimateClicked>().Animate());
	}
    void OnMouseDown()
    {
		if (levelLinked.status > 0 || LevelManager.testMode)
			StartCoroutine(SelectLevel());
	}
	void OnMouseOver()
	{
		Debug.Log("Over " + transform.name);
		levelText.text = levelLinked.status == 2
        ? levelLinked.levelName
        : levelLinked.level.GetComponent<Level>().levelNameHint;
	}
	void OnMouseExit()
    {
        levelText.text = "";
    }
    void Start()
    {
        levelText = GameObject.FindGameObjectWithTag("Level Selector Header").GetComponent<TextMeshProUGUI>();
		levelText.text = "";
    }
}
