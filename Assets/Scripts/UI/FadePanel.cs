using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadePanel : MonoBehaviour
{
    public enum panelLinked
    {
        pauseMenu,
        objectName
    };
    public panelLinked linked;
    public GameObject panel;
    public void StartFading()
    {
        panel.GetComponent<Animator>().SetTrigger("StartMenu");
    }
    public void ExitFading()
    {
        panel.GetComponent<Animator>().SetTrigger("ExitMenu");
    }
}
