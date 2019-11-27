using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DisplayPanel : MonoBehaviour
{
    public GameObject panel;
    private Button button;
    void PanelHandler()
    {
        panel.SetActive(!panel.activeSelf);
        EventSystem.current.SetSelectedGameObject(null);
    }
    void Start()
    {
        panel.SetActive(false);
        button = GetComponent<Button>();
        button.onClick.AddListener(PanelHandler);
    }
}
