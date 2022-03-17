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
    }


    // Play Functions
    public void PlayMenuMusic()
    {
        int randonNumber = Random.Range(0,9);
        MusicSource.clip = MusicClips[randonNumber];
        MusicSource.Play();
    }


    public void PlayLevelMusic(int MusicID, float _musicOffset)
    {
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

    void MusicFade(){

    }

    public void GameRestarted(){
        _isPaused = false;
    }
}
