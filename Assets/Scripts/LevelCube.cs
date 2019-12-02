using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelCube : MonoBehaviour
{
    public SelectorCube levelLinked;
    private TextMeshProUGUI levelText;
    private GameManager gameManager;
    public FadePanel detailsUICanvas;
    public LevelUI detailsUI;
    public void UpdateLevelTextStatus()
    {
        levelLinked.willBeUnlock = false;
        levelLinked.levelStatusText.text = "?";
    }
    IEnumerator SelectLevel()
	{
		gameManager.selectedLevel = levelLinked.level;
		gameManager.selectedLevelPosition = transform.position;
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
	void OnMouseEnter()
	{
        if (levelLinked.level.levelStatus == Level.status.Done || gameManager.testMode)
        {
            detailsUICanvas.StartFading();
            detailsUI.score.text = levelLinked.level.score.ToString();
            levelText.text = levelLinked.level.levelName;
        }
        else if (levelLinked.level.levelStatus == Level.status.Available)
            levelText.text = levelLinked.level.levelNameHint;
	}
	void OnMouseExit()
    {
        levelText.text = "";
        if (detailsUI.score.text != "")
            detailsUICanvas.ExitFading();
    }
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        levelText = GameObject.FindGameObjectWithTag("Level Selector Header").GetComponent<TextMeshProUGUI>();
		levelText.text = "";
        detailsUI = detailsUICanvas.GetComponent<LevelUI>();
    }
}
