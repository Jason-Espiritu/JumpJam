using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RT_Player_Input : MonoBehaviour
{
    private RT_Timer_Global Timer_Global;
    private RT_GameManager RTGM;

    [Header("Input/s")]
    [SerializeField] private KeyCode Jump;
    [SerializeField] private int _greatInputs;
    [SerializeField] private int _badInputs;
    [SerializeField] private float _inputRange;
    [SerializeField] private float _inputCooldown;

    private bool _isBadInput;

    [Header("Accuracy")]
    [SerializeField] private float _accuracy;

    // Start is called before the first frame update
    void Start()
    {
        Timer_Global = GetComponent<RT_Timer_Global>();
        RTGM = GetComponent<RT_GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (RTGM.g_isGameStarted && !RTGM.g_isGameEnded)
        {
            if (Input.GetKeyDown(Jump))
            {
                //Run Timing Check
                TimingCheck();
                CheckAccuracy();
            }
        }
    }

    void TimingCheck()
    {
        //Get Input Time
        float timeOfInput = Timer_Global.g_timing;

        if (timeOfInput <= _inputRange || timeOfInput >= RTGM.g_BPS - _inputRange)
        {
            //Great Input
            _greatInputs += 1;
            Debug.Log("Great");
        }
        else
        {
            //Bad Input
            _badInputs += 1;
            Debug.Log("Bad");
        }


    }

    void CheckAccuracy()
    {
        //From Input
        float totalInputs = _greatInputs + _badInputs;
        _accuracy = (_greatInputs / totalInputs) * 100;
        Debug.Log(string.Format("{0:00.00} %", _accuracy));
    }

}
