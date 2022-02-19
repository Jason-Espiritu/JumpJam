using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] public AudioSource MusicSource;
    [SerializeField] List<AudioClip> MusicClips = new List<AudioClip>(); 

    [SerializeField] public AudioSource SFXSource;
    [SerializeField] List<AudioClip> SFXClips = new List<AudioClip>();
    
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
    }


    public void PlayLevelMusic(bool fromPausedState)
    {
        switch (fromPausedState)
        {
            case true:
                MusicSource.Play();
                break;
            default:
                if (!MusicSource.isPlaying)
                {
                    MusicSource.clip = MusicClips[0];
                    MusicSource.Play();
                }
                break;
        }
    }

    public void PlaySFX(int sfxValue)
    {
        SFXSource.PlayOneShot(SFXClips[sfxValue]);
    }
    
    public void PauseorStopMusic(bool stop)
    {
        switch (stop)
        {
            case true:
                MusicSource.Stop();
                break;
            default:
                MusicSource.Pause();
                break;
        }
    }
}
