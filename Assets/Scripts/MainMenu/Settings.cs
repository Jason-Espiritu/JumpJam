using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class Settings : MonoBehaviour
{
    [SerializeField] TMP_Dropdown _resolution;
    [SerializeField] Toggle _isFullScreenMode;
    [SerializeField] AudioMixer _audioMixer;
    [SerializeField] Slider _masterAudio;
    [SerializeField] Slider _musicAudio;
    [SerializeField] Slider _sfxAudio;
    [SerializeField] Button _applyResolution;

    Canvas settingScreen;

    //Resolution Setting
    private int _resolutionIndx;
    private int _fullScreenMode;

    //Audio Settings
    const string MASTER_AUDIO = "MasterVolume";
    const string MUSIC_AUDIO = "MusicVolume";
    const string SFX_AUDIO = "SFXVolume";
    private void Awake()
    {

        //When Slider Change Change Audio Settings
        _masterAudio.onValueChanged.AddListener(SetMasterVolume);
        _musicAudio.onValueChanged.AddListener(SetMusicVolume);
        _sfxAudio.onValueChanged.AddListener(SetSFXVolume);
        
    }
    void Start()
    {
        settingScreen = GetComponent<Canvas>();
        //Load Resolution Settings
        LoadResolutionSettings();
        //Load Volume Settings
        LoadAudioSettings();
    }

    //Display Settings
    public void ChangeResolution()
    {
        int resolutionIndex = _resolution.value;
        Screen.SetResolution(SettingNames._resolutionWidth[resolutionIndex], SettingNames._resolutionHeight[resolutionIndex], _isFullScreenMode.isOn);
        Debug.Log("Resolution Changed: Resolution Width: " + SettingNames._resolutionWidth[resolutionIndex] + " Resolution Height: " + SettingNames._resolutionHeight[resolutionIndex] + " Windowed Mode: " + _isFullScreenMode.isOn.ToString());
        
        //Save Resolution Settings
        PlayerPrefs.SetInt(SettingNames.resolutionIndex, _resolution.value);
        PlayerPrefs.SetInt(SettingNames.fullScreenMode, Convert.ToInt32(_isFullScreenMode.isOn));
        //Reload Res Settings
        LoadResolutionSettings();
    }
    void LoadResolutionSettings(){
        //Get Values From Player Prefs
        _resolutionIndx = PlayerPrefs.GetInt(SettingNames.resolutionIndex);
        _fullScreenMode = PlayerPrefs.GetInt(SettingNames.fullScreenMode);

        _resolution.value = _resolutionIndx;
        _isFullScreenMode.isOn = Convert.ToBoolean(_fullScreenMode);

        _applyResolution.interactable = false;
    }

    // Audio Settings
    void SetMasterVolume(float value){
        _audioMixer.SetFloat(MASTER_AUDIO, Mathf.Log10(value) * 20);
        //Save Master Volume
        PlayerPrefs.SetFloat(SettingNames.masterVolume, _masterAudio.value);
        //Save Master Volume in AudioManager
        AudioManager.instance.g_valMasterSet = value;
    }
    void SetMusicVolume(float value){
        _audioMixer.SetFloat(MUSIC_AUDIO, Mathf.Log10(value) * 20);
        //Save Music Volume
        PlayerPrefs.SetFloat(SettingNames.musicVolume, _musicAudio.value);
        AudioManager.instance.g_valMusicSet = value;
    }
    void SetSFXVolume(float value){
        _audioMixer.SetFloat(SFX_AUDIO, Mathf.Log10(value) * 20);
        //Save SFX Volume
        PlayerPrefs.SetFloat(SettingNames.sfxVolume, _sfxAudio.value);
    }

    void LoadAudioSettings(){
        _masterAudio.value = PlayerPrefs.GetFloat(SettingNames.masterVolume, 0.5f);
        _musicAudio.value = PlayerPrefs.GetFloat(SettingNames.musicVolume, 1f);
        _sfxAudio.value = PlayerPrefs.GetFloat(SettingNames.sfxVolume, 1f);

        //Save Slider Values to AudioManager
        AudioManager.instance.g_valMasterSet = _masterAudio.value;
        AudioManager.instance.g_valMusicSet = _musicAudio.value;
    }

    //Save and Quit Settings Screen
    public void SaveSettings()
    {
        if(_resolution.value != _resolutionIndx || _isFullScreenMode.isOn != Convert.ToBoolean(_fullScreenMode)){
            //ConfirmationBox.Instance.ResetDelagates();
            ConfirmationBox.Instance.ShowConfirmBox("Apply Settings?", () => 
                {
                    ChangeResolution(); 
                    settingScreen.enabled = false;
                }, 
                () => 
                {
                    LoadResolutionSettings(); // Reload Defaults on the Screen
                    settingScreen.enabled = false;
                });
        }else{
            settingScreen.enabled = false;
        }
    }
    public void ShowHideSettings(bool value){
        settingScreen.enabled = value;
    }

    public bool IsinSettings()
    {
        return settingScreen.enabled;
    }

    public void PlaySoundFX(int sfxID){
        AudioManager.instance.PlaySFX(sfxID);
    }
}
