using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateToggle : MonoBehaviour
{
    private PlayerSettings settings;
    private Toggle toggle;
    void Start()
    {
        settings = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<PlayerSettings>();
        toggle = GetComponent<Toggle>();
        toggle.isOn = settings.muteAudio;
        toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged();
        });
    }
    void ToggleValueChanged()
    {
        settings.muteAudio = toggle.isOn;
    }
}
