﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HandleSliderValue : MonoBehaviour
{
    public soundType type;
    private GameManager gm;
    private Slider slider;
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        slider = GetComponent<Slider>();
        SetSliderValues();
        slider.onValueChanged.AddListener(delegate {ValueChangeCheck(); });
    }
    void SetSliderValues()
    {
        if (type == soundType.global)
            slider.value = gm.globalVolume;
        else if (type == soundType.music)
            slider.value = gm.musicVolume;
    }
    void ValueChangeCheck() {
        if (type == soundType.global)
            gm.globalVolume = slider.value;
        else if (type == soundType.music)
            gm.musicVolume = slider.value;
    }
}