using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelCube : MonoBehaviour
{
    public SelectorCube levelLinked;
    private TextMeshProUGUI levelText;
    private GameManager gameManager;
    IEnumerator SelectLevel()
	{
		gameManager.selectedLevel = levelLinked.level;
		gameManager.selectedLevelPosition = transform.position;
        Debug.Log(gameManager.selectedLevel);
		TransitionManager.instance.levelToLoad = "Level";
		yield return StartCoroutine(GetComponent<AnimateClicked>().Animate());
	}
    void OnMouseDown()
    {
		if (levelLinked.level.levelStatus != Level.status.Locked || gameManager.testMode)
        {
			StartCoroutine(SelectLevel());
            GetComponent<AudioSource>().Play();
        }
	}
	void OnMouseOver()
	{
		levelText.text = levelLinked.level.levelStatus == Level.status.Done
        ? levelLinked.level.levelName
        : levelLinked.level.levelNameHint;
	}
	void OnMouseExit()
    {
        levelText.text = "";
    }
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        levelText = GameObject.FindGameObjectWithTag("Level Selector Header").GetComponent<TextMeshProUGUI>();
		levelText.text = "";
    }
}
