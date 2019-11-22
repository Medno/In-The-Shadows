using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    public GameObject  level;
	void Start () {
		Button btn = GetComponent<Button>();
		btn.onClick.AddListener(LoadLevel);
	}

	void LoadLevel()
    {
        LevelManager.selectedLevel = level;
		Debug.Log ("You have clicked the button!");
        SceneManager.LoadScene("Level");
	}

}
