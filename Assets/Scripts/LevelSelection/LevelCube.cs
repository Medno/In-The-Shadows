using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelCube : MonoBehaviour
{
    public SelectorCube levelLinked;
    private TextMeshProUGUI levelText;
    private GameManager gameManager;
    public GameObject detailsUICanvas;
    public LevelUI detailsUI;
    public FadePanel[] lockedUI;
    public FadePanel[] unlockedUI;
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        levelText = GameObject.FindGameObjectWithTag("Level Selector Header").GetComponent<TextMeshProUGUI>();
		levelText.text = "";
        detailsUI = detailsUICanvas.GetComponent<LevelUI>();

        detailsUI.time.text = GetTimeScore();
        detailsUI.difficulty.text = GetDifficulty();
    }
    string GetTimeScore()
    {
        string score = "";
        int minutes = (int)levelLinked.level.score / 60;
        int seconds = (int)levelLinked.level.score % 60;
        string minutesString = minutes < 10 ? "0" + minutes : minutes.ToString();
        string secondsString = seconds < 10 ? "0" + seconds : seconds.ToString();
        score = minutesString + ":" + secondsString;
        return score;
    }
    string GetDifficulty()
    {
        if (levelLinked.level.currentDifficulty == Level.difficulty.ONE)
            return "One";
        else if (levelLinked.level.currentDifficulty == Level.difficulty.TWO)
            return "Two";
        return "Three";
    }
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
		yield return StartCoroutine(GetComponent<Clicked>().Animate());
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
        foreach(FadePanel panel in unlockedUI)
            panel.StartFading();
        if (levelLinked.level.levelStatus == Level.status.Done || gameManager.testMode)
        {
            foreach(FadePanel panel in lockedUI)
                panel.StartFading();
            levelText.text = levelLinked.level.levelName;
        }
        else if (levelLinked.level.levelStatus == Level.status.Available)
            levelText.text = levelLinked.level.levelHint;
	}
	void OnMouseExit()
    {
        levelText.text = "";
        if (levelLinked.level.levelStatus == Level.status.Done || gameManager.testMode)
            foreach(FadePanel panel in lockedUI)
                panel.ExitFading();
        foreach(FadePanel panel in unlockedUI)
            panel.ExitFading();
    }
}
