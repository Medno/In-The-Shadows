using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HandleSliderValue : MonoBehaviour
{
    public soundType type;
    private PlayerSettings settings;
    private Slider slider;
    void Start()
    {
        settings = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<PlayerSettings>();
        slider = GetComponent<Slider>();
        SetSliderValues();
        slider.onValueChanged.AddListener(delegate {ValueChangeCheck(); });
    }
    void SetSliderValues()
    {
        if (type == soundType.global)
            slider.value = settings.globalVolume;
        else if (type == soundType.music)
            slider.value = settings.musicVolume;
    }
    void ValueChangeCheck() {
        if (type == soundType.global)
            settings.globalVolume = slider.value;
        else if (type == soundType.music)
            settings.musicVolume = slider.value;
    }
}
