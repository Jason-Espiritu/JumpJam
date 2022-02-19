using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GMInstance;

    [Header("Game Level Mode")]
    public bool g_normalMode;
    [SerializeField] private int _maxScore;
    [SerializeField] private int _timeLimit;

    [Header("Game State")]
    public bool g_isGameStarted;
    public bool g_isGameEnded;

    [Header ("Music Calculations")]
    [SerializeField] private float _BPM;
    [SerializeField] private float _BPS;
    
    [Range(0f, 1f)]
    [SerializeField] private float _EarlyOffsetToBeat = 0f;
    [Range(0f, 1f)]
    [SerializeField] private float _LateOffsetToBeat = 0f;

    //Beat Variables
    //[HideInInspector] public float g_beatEarly;
    //[HideInInspector] public float g_beatLate;
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
        //BPS Calculation
        _BPS = _BPMConstant / _BPM;
        g_BPS = _BPS;

        //BPM Multiplier Calculation 60 BPM is standard since it's 1 Beat per Second
        g_BPM_Multiplier = _BPM / _BPMConstant;

        //Offset Calculation
        //g_beatEarly = _BPS - _EarlyOffsetToBeat;
        //g_beatLate = _BPS + _LateOffsetToBeat;
        
        //Debug.Log(_EarlyOffsetToBeat + " <- " + _BPS + " -> " + _LateOffsetToBeat);

    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
