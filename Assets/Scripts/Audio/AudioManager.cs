using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] public AudioMixer _audioMixer;
    [SerializeField] public AudioSource MusicSource;
    [SerializeField] List<AudioClip> MusicClips = new List<AudioClip>(); 

    [SerializeField] public AudioSource SFXSource;
    [SerializeField] List<AudioClip> SFXClips = new List<AudioClip>();
    
    //Audio Settings
    const string MASTER_AUDIO = "MasterVolume";
    const string MUSIC_AUDIO = "MusicVolume";
    const string SFX_AUDIO = "SFXVolume";
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        //Check if it is First Launch of the Game
        LoadAudioSettings();
    }

    void LoadAudioSettings(){
        float master_Volume = PlayerPrefs.GetFloat(MASTER_AUDIO, 0.5f);
        float music_Volume = PlayerPrefs.GetFloat(MUSIC_AUDIO, 1f);
        float sfx_Volume = PlayerPrefs.GetFloat(SFX_AUDIO, 1f);

        _audioMixer.SetFloat(SettingNames.masterVolume, Mathf.Log10(master_Volume) * 20);
        _audioMixer.SetFloat(SettingNames.musicVolume, Mathf.Log10(music_Volume) * 20);
        _audioMixer.SetFloat(SettingNames.sfxVolume, Mathf.Log10(sfx_Volume) * 20);
    }

    // Play Functions
    public void PlayMenuMusic(int _musicIndex)
    {
        //Resets Music Volume
        MusicSource.volume = 1f;
        MusicSource.loop = true;

        if(MusicSource.isPlaying){
            //Check if it is the Same Song
            if(MusicSource.clip.name != MusicClips[_musicIndex].name){
                StartCoroutine(FadeOutToNextSong(_musicIndex, 1f));
            }
        }else{
            MusicSource.clip = MusicClips[_musicIndex];
            MusicSource.Play();
        }
    }


    public void PlayLevelMusic(int MusicID, float _musicOffset)
    {
        MusicSource.volume = 1f;
        MusicSource.loop = false;
        MusicSource.clip = MusicClips[MusicID];
        MusicSource.time = _musicOffset;
        MusicSource.Play();
    }

    public void PlaySFX(int sfxValue)
    {
        SFXSource.PlayOneShot(SFXClips[sfxValue]);
    }
    
    private bool _isPaused;
    public void PauseorStopMusic(bool stop)
    {
        switch (stop)
        {
            case true:
                MusicSource.Stop();
                break;
            default:
                if (!_isPaused){
                    MusicSource.Pause();
                    _isPaused = true;
                }else{
                    MusicSource.UnPause();
                    _isPaused = false;
                }
                break;
        }
    }

    public IEnumerator FadeOut(float FadeTime){
        float startVolume = MusicSource.volume;

        while (MusicSource.volume > 0){
            MusicSource.volume -= startVolume * Time.deltaTime / FadeTime;
            
            yield return null; 
        }
        MusicSource.Stop();
        MusicSource.volume = startVolume;
    }


    public void GameRestarted(){
        _isPaused = false;
    }
    IEnumerator FadeOutToNextSong(int MusicIndex, float FadeTime){
        float startVolume = MusicSource.volume;

        while (MusicSource.volume > 0){
            MusicSource.volume -= startVolume * Time.deltaTime / FadeTime;
            
            yield return null; 
        }
        MusicSource.Stop();
        MusicSource.volume = startVolume;
        MusicSource.clip = MusicClips[MusicIndex];
        MusicSource.Play();
    }
}
