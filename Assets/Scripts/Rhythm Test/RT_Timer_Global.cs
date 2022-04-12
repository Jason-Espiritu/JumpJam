using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RT_Timer_Global : MonoBehaviour
{
    //Class Instance
    public static RT_Timer_Global instance;

    //private variables
    private RT_GameManager RTGM;
    
    [SerializeField] private TMP_Text CountDown;
    [SerializeField] private float _countdownTimer;

    [SerializeField] private float _countdownBeforeStart;

    //This is for Timing
    public float g_timing;

    //Constant
    private const float _constCountdownTimer = 20f;

    // Start is called before the first frame update
    void Start()
    {
        RTGM = GetComponent<RT_GameManager>();
        ResetTimer();

    }

    // Update is called once per frame
    void Update()
    {
        if (RTGM.g_isGameStarted)
        {
            if (!RTGM.g_isGameEnded)
            {
                CountDownTimer();
                StartTiming();
            }
        }
    }


    void StartTiming()
    {
        g_timing += Time.deltaTime;
        if (g_timing >= RTGM.g_BPS)
        {
            g_timing = g_timing - RTGM.g_BPS;

            AudioManager.instance.PlaySFX2(3); //TEMP
        }
        else
        {
            //Do Nothing
        }
    }

    void CountdownToStart()
    {
        
    }
    

    void CountDownTimer()
    {
        if (_countdownTimer > 0f)
        {
            _countdownTimer -= Time.deltaTime;

            //Display the Countdown to UI
            DisplayTimer(_countdownTimer);
        }
        else
        {
            //Execute End Function
            CountDown.text = "GameEnded"; //Comment or Remove when Done
            RTGM.g_isGameEnded = true;
            RTGM.g_isGameStarted = false;

            _countdownTimer = 0;
            DisplayTimer(_countdownTimer);

            //Call Results Script


        }
    }

    public void ResetTimer()
    {
        _countdownTimer = _constCountdownTimer;
        g_timing = 0f;
    }

    public int GetTimeLeft()
    {
        return Mathf.FloorToInt(_countdownTimer);
    }

    public int GetConstCountDown()
    {
        return (int)_constCountdownTimer;
    }

    private void DisplayTimer(float deltaTime)
    {
        float seconds = Mathf.FloorToInt(deltaTime);
        CountDown.text = string.Format("Time\n{0:00}", seconds);
    }
}
