using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField] float DelayInSeconds;
    private GameManager GameMngr;
    private Point_System Points;
    private Timer_Global TimeLimit;

    private bool _GameEndProcessed;

    private string _gameLevelID;

    //Saved HighScore and Lowest time from
    private int _savedHighScore;
    private int _savedTimeLeft;
    
    //Current
    private int _currentHighScore;
    private int _currentTimeLeft;

    //Final Score
    private int _finalHighScore;
    private int _finalTimeLeft;

    //This is to know if it is a HighScore or not.
    private bool _newHighScore;
    private bool _newTimeLeft;

    // Start is called before the first frame update
    void Start()
    {
        GameMngr = GetComponent<GameManager>();
        Points = GetComponent<Point_System>();
        TimeLimit = GetComponent<Timer_Global>();

        _gameLevelID = GameMngr.g_GameLevelID;

        //Load HighScore
        if (IsThereSavedScore())
        {
            _savedHighScore = PlayerPrefs.GetInt("highscore" + _gameLevelID);
            _savedTimeLeft = PlayerPrefs.GetInt("lowesttime" + _gameLevelID);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameMngr.g_isGameEnded && !_GameEndProcessed)
        {
            StartCoroutine(CalculateScores(1f));
            _GameEndProcessed = true;
        }
    }

    IEnumerator CalculateScores(float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        GameMngr.g_isGameStarted = false;
        Debug.Log("Game Finished");

        // Get Current Score
        
        // Compare Scores

        // Reveal Scores

    }
    void ScoreComparison()
    {
        // Get Current Score
        _currentHighScore = Points.g_score;
        _currentTimeLeft = Mathf.FloorToInt(TimeLimit.g_timeLeft % 60f);

        if (IsThereSavedScore())
        {
            if (_currentHighScore > _savedHighScore)
            {
                //Set New High Score
                _newHighScore = true;
                _finalHighScore = _currentHighScore;
            }

            if (_newHighScore)
            {
                if (_currentTimeLeft > _savedTimeLeft)
                {

                }
            }
        }
    }
    bool IsThereSavedScore()
    {
        if (PlayerPrefs.GetInt("HighScore" + _gameLevelID) > 0)
        {
            return true;
        }
        return false;
    }
}
