using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Timer_Global : MonoBehaviour
{
    public Text UI_TEXT; //Debug Comment / Remove if Finish

    //Class Instance
    public static Timer_Global Instance;
    
    //Privates
    [SerializeField] private GameManager GM;
    [SerializeField] private TMP_Text CountDown;
    [SerializeField] private float _countdownTimer;

    private float g_timer;
    
    //Public Variable/s
    public bool g_openPlatform;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        _countdownTimer = GM.g_timeLimit;
        DisplayTimer(_countdownTimer);
    }

    // Update is called once per frame
    void Update()
    {
        if (GM.g_isGameStarted && !GM.g_isGameEnded)
        {
            StartRunningPlatforms();
            CountDownTimer();
        }
    }

    void StartRunningPlatforms()
    {
        g_timer += Time.deltaTime;
        if (g_timer >= GM.g_BPS)
        {
            g_openPlatform = true; //Open
            g_timer = 0f;

        }
        else if (g_timer >= (GM.g_BPS / 2f))
        {
            g_openPlatform = false; //Close
        }

        UI_TEXT.text = Instance.g_timer.ToString();
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
        }
    }

    private void DisplayTimer(float deltaTime)
    {
        float seconds = Mathf.FloorToInt(deltaTime % 60f);
        CountDown.text = string.Format("Time\n{0:00}", seconds);
    }
}
