using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager GMInstance;

    [SerializeField] private PauseButton PauseButton;
    [SerializeField] private Settings Settings;
    [SerializeField] private TMP_Text StartLabel;

    //This came from the Main Repo Branch
    [Header("Game Level Mode")]
    public string g_GameLevelID;
    public int _musicID;
    public bool g_normalMode;
    public int g_maxScore;
    public float g_timeLimit;
    public float g_musicVolume;

    [Header("Game State")]
    public bool g_isGameStarted;
    public bool g_isGameEnded;

    [Header ("Music Calculations")]
    [SerializeField] private float _BPM;
    [SerializeField] private float _BPS;
    [SerializeField] private float _musicOffset;

    [HideInInspector] public float g_BPS;

    public float g_BPM_Multiplier;
    public float g_timerPlattform;

    //For the Jump Notification
    [HideInInspector] public string g_jumpNotif;

    //constant Varible/s
    private const float _BPMConstant = 60f;
    // Start is called before the first frame update

    private void Awake()
    {
        if(GMInstance == null)
        {
            GMInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        AudioManager.instance.PauseorStopMusic(true);
    }

    void Start()
    {
        //BPS Calculation
        _BPS = _BPMConstant / _BPM;
        g_BPS = _BPS;

        //BPM Multiplier Calculation 60 BPM is standard since it's 1 Beat per Second
        //This is for the Physics Calculation for the Player.
        g_BPM_Multiplier = _BPM / _BPMConstant;

        //Reset AudioManager Pause Variable
        AudioManager.instance.GameRestarted();

        CheckMusicAudio();
    }

    // Update is called once per frame
    private bool _isMusicAlreadyPlayed;
    private void Update()
    {
        if(!_isMusicAlreadyPlayed){
            if(g_isGameStarted){
                StartLabel.enabled = false;
                AudioManager.instance.PlayLevelMusic(_musicID, _musicOffset, g_musicVolume);
                _isMusicAlreadyPlayed = true;
                if (g_GameLevelID == "0"){
                    AudioManager.instance.MusicSource.loop = true;
                }else{
                    AudioManager.instance.MusicSource.loop = false;
                }
            }
        }
    }

    public void CheckMusicAudio() //Checks if Volume of Master or Music is 0
    {
        if(AudioManager.instance.g_valMasterSet == 0.0001f || AudioManager.instance.g_valMusicSet == 0.0001f)
        {
            //Pause the Whole Game
            PauseButton.Pause();
            Time.timeScale = 0f;
            AlertBox.Instance.ShowAlertBox("Music / Master Volume has been set to 0\nPlease set the Master / Music Volume Above 0",
                () => 
                {
                    if (!PauseButton.IsScreenPaused())
                    {
                        PauseButton.Pause();
                    }

                    if (!Settings.IsinSettings())
                    {
                        Settings.ShowHideSettings(true);
                    }
                });
        }
    }

    public void PlaySFX(int sfxValue){
        AudioManager.instance.PlaySFX(sfxValue);
    }
}
