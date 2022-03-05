using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    [SerializeField] TMP_Dropdown resolution;
    [SerializeField] Toggle isFullScreenMode;
    [SerializeField] Slider masterAudio;
    [SerializeField] Slider musicAudio;
    [SerializeField] Slider sfxAudio;
    Canvas settingScreen;

    //Resolution Width and Height
    static readonly int[] _resolutionWidth =  { 1280, 1366, 1600, 1920 };
    static readonly int[] _resolutionHeight = { 720, 768, 900, 1080 };
    private int _resolutionIndx;
    private int fullScreenMode;

    private void Awake()
    {
        _resolutionIndx = PlayerPrefs.GetInt("ResolutionIndex");
        fullScreenMode = PlayerPrefs.GetInt("WindowedMode");
    }
    void Start()
    {
        //Load Resolution Settings
        settingScreen = GetComponent<Canvas>();
        resolution.value = _resolutionIndx;
        if (fullScreenMode == 0)
        {
            isFullScreenMode.isOn = true;
        }
        else
        {
            isFullScreenMode.isOn = false;
        }
        //Load Volume Settings
        
    }

    public void Back()
    {
        settingScreen.enabled = false;
    }

    public void ChangeResolution()
    {
        int resolutionIndex = resolution.value;
        Screen.SetResolution(_resolutionWidth[resolutionIndex], _resolutionHeight[resolutionIndex], isFullScreenMode.isOn);
        Debug.Log("Resolution Changed: Resolution Width: " + _resolutionWidth[resolutionIndex] + " Resolution Height: " + _resolutionHeight[resolutionIndex] + " Windowed Mode: " + isFullScreenMode.isOn.ToString());
    }

    public void SaveSettings()
    {
        
    }
}
