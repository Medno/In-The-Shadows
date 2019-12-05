using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitGame : MonoBehaviour
{
    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(QuitGame);
    }
    void QuitGame()
    {
        Debug.Log("Exiting game...");
        Application.Quit();
    }
}
