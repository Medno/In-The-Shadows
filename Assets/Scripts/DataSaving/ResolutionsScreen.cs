using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResolutionsScreen : MonoBehaviour
{
    private Resolution[] resolutions;
    private TMP_Dropdown dropdown;
    private int width;
    private int height;
    public Toggle toggle;
    private bool fullScreen;
    void Save()
    {
        PlayerPrefs.SetInt("Width", width);
        PlayerPrefs.SetInt("Height", height);
        PlayerPrefs.SetInt("FullScreen", fullScreen == true ? 1 : 0);
    }
    void LoadResolution()
    {
        if (PlayerPrefs.HasKey("FullScreen"))
            fullScreen = PlayerPrefs.GetInt("FullScreen") == 1 ? true : false;
        else
            fullScreen = true;
        if (PlayerPrefs.HasKey("Width") && PlayerPrefs.HasKey("Height"))
        {
            width = PlayerPrefs.GetInt("Width");
            height = PlayerPrefs.GetInt("Height");
        }
        else if (resolutions.Length > 0)
        {
            width = resolutions[resolutions.Length - 1].width;
            height = resolutions[resolutions.Length - 1].height;
        }
        Save();
        Debug.Log("Width : " + width + ", height : " + height);
        Screen.SetResolution(width, height, fullScreen);
    }
    void ToggleValueChanged()
    {
        fullScreen = toggle.isOn;
        Screen.SetResolution(width, height, fullScreen);
        PlayerPrefs.SetInt("FullScreen", fullScreen == true ? 1 : 0);
    }
    void DropdownValueChanged(TMP_Dropdown drop)
    {
        Debug.Log("Selected: " + drop.value);
        width = resolutions[drop.value].width;
        height = resolutions[drop.value].height;
        Screen.SetResolution(width, height, fullScreen);
        Save();
    }
    void SetupFullScreenToggle()
    {
        toggle.isOn = fullScreen;
        toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged();
        });
    }
    void SetupDropdown()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        dropdown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(dropdown);
        });
        List<string> opts = new List<string>();
        int matchedResolutionIndex = 0;
        for(int i = 0; i < resolutions.Length; i++)
        {
            if (width == resolutions[i].width && height == resolutions[i].height)
                matchedResolutionIndex = i;
            opts.Add(resolutions[i].width + "x" + resolutions[i].height);
        }
        dropdown.AddOptions(opts);
        dropdown.value = matchedResolutionIndex;
    }
    void Start()
    {
        resolutions = Screen.resolutions;
        LoadResolution();
        SetupFullScreenToggle();
        SetupDropdown();
    }
}
