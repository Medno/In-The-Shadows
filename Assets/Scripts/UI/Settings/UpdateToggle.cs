using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateToggle : MonoBehaviour
{
    private GameManager gm;
    private Toggle toggle;
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        toggle = GetComponent<Toggle>();
        toggle.isOn = gm.muteAudio;
        toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged();
        });
    }
    void ToggleValueChanged()
    {
        gm.muteAudio = toggle.isOn;
    }
}
