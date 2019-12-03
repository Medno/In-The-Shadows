using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitApplication : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(QuitGame);
    }

    void QuitGame()
    {
        Application.Quit();
    }
}
