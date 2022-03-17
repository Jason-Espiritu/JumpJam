using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GMInstance;

    private Timer_Global TimeLeft;
    private Point_System Points;

    //This came from the Main Repo Branch
    [Header("Game Level Mode")]
    public string g_GameLevelID;
    public int _musicID;
    public bool g_normalMode;
    public int g_maxScore;
    public float g_timeLimit;

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
    }

    void Start()
    {
        TimeLeft = GetComponent<Timer_Global>();
        Points = GetComponent<Point_System>();

        //BPS Calculation
        _BPS = _BPMConstant / _BPM;
        g_BPS = _BPS;

        //BPM Multiplier Calculation 60 BPM is standard since it's 1 Beat per Second
        //This is for the Physics Calculation for the Player.
        g_BPM_Multiplier = _BPM / _BPMConstant;

        //Reset AudioManager Pause Variable
        AudioManager.instance.GameRestarted();
    }

    // Update is called once per frame
    private bool _isMusicAlreadyPlayed;
    private void Update()
    {
        if(!_isMusicAlreadyPlayed){
            if(g_isGameStarted){
                AudioManager.instance.PlayLevelMusic(_musicID, _musicOffset);
                _isMusicAlreadyPlayed = true;
            }
        }
    }

    public void PlaySFX(int sfxValue){
        AudioManager.instance.PlaySFX(sfxValue);
    }
}
