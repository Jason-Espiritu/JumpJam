using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RT_GameManager : MonoBehaviour
{
    private RT_Player_Input Player_Input;
    private RT_Timer_Global Timer_Global;
    private RT_Results RT_Results;

    [Header("Game State")]
    [SerializeField] private int _trialNumber;
    public bool g_isGameStarted;
    public bool g_isGameEnded;

    [Header("Music Calculations")]
    [SerializeField] private float _tempBPM;
    [SerializeField] private float _BPM;
    [SerializeField] private float _BPS;
    //[SerializeField] private float _musicOffset;

    [HideInInspector] public float g_BPS;

    [Header("Other")]
    [SerializeField] private int _maxPoint;

    //constant Varible/s
    private const float _BPMConstant = 60f;



    // Start is called before the first frame update
    void Start()
    {
        Player_Input = GetComponent<RT_Player_Input>();
        Timer_Global = GetComponent<RT_Timer_Global>();
        RT_Results = GetComponent<RT_Results>();

        LoadTrial(1);
    }

    public void LoadTrial(int trialNumber)
    {
        //Reset Timer
        Timer_Global.ResetTimer();
        //Reset GameStates
        g_isGameStarted = false;
        g_isGameEnded = false;
        //Get BPM
        _BPM = _tempBPM;
        //Calculate BPS
        _BPS = _BPMConstant / _BPM;
        g_BPS = _BPS;
        //ReCalculate MaxPoint
        _maxPoint = Mathf.FloorToInt(_BPS * Timer_Global.GetConstCountDown());

        //Update Trial Number
        _trialNumber = trialNumber;
        //Play TrialNumber Music in Loop

        
    }
}
