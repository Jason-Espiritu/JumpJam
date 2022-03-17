using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndGameTutor : MonoBehaviour
{
    [SerializeField] Canvas ResultsScreen;
    [SerializeField] TMP_Text LabelScore;
    [SerializeField] TMP_Text LabelTimeLeft;
    [SerializeField] TMP_Text LabelStatus;
    [SerializeField] Image[] Stars;

    [SerializeField] float DelayInSeconds;
    private GameManager GameMngr;
    private Point_System Points;
    private Timer_Global TimeLimit;

    private bool _GameEndProcessed;

    private string _gameLevelID;

    //Saved HighScore and Lowest time from
    private int _savedHighScore;
    private int _savedTimeLeft;
    private int _savedStarReward;
    
    //Current
    private int _currentHighScore;
    private int _currentTimeLeft;
    private int _starReward;
    private bool _saveIt;
    //This to create a standard name for the Level.
    private string _highScoreLevelName;
    private string _timeLeftLevelName;
    private string _starRewardLevelName;

    // Start is called before the first frame update
    void Start()
    {
        GameMngr = GetComponent<GameManager>();
        Points = GetComponent<Point_System>();
        TimeLimit = GetComponent<Timer_Global>();

        _gameLevelID = GameMngr.g_GameLevelID;

        //Recompile Full Name of Level
        _highScoreLevelName = "Level_" + _gameLevelID + "_HighScore";
        _timeLeftLevelName = "Level_" + _gameLevelID + "_TimeLeft";
        _starRewardLevelName = "Level_" + _gameLevelID + "_Star";

        //Load HighScore
        if (IsThereSavedScore())
        {
            _savedHighScore = PlayerPrefs.GetInt(_highScoreLevelName);
            _savedTimeLeft = PlayerPrefs.GetInt(_timeLeftLevelName);
            _savedStarReward = PlayerPrefs.GetInt(_starRewardLevelName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameMngr.g_isGameEnded && !_GameEndProcessed)
        {
            // Get Current Score
            _currentHighScore = Points.GetScore();
            _currentTimeLeft = TimeLimit.GetTimeLeft();

            StartCoroutine(CalculateScores(1f));
            _GameEndProcessed = true;
        }
    }

    IEnumerator CalculateScores(float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        GameMngr.g_isGameStarted = false;
        Debug.Log("Game Finished");

        //Reveal Scores
        ResultsScreen.enabled = true;

        _starReward = StarRewards();
        //Check if Score is Perfect
        if (IsPerfect())
        {
            //Show Perfect Score
            Debug.Log("Perfect Score");
            LabelStatus.text = "Perfect!";
        }

        // Check if Score is a HighScore
        if (IsNewHighScore())
        {
            //Show Best Record
            Debug.Log("New High Score " + _currentHighScore + " Time Left: " + _currentTimeLeft);
            if (IsPerfect())
            {
                LabelStatus.text += " and Best Record!"; //Adds in the Perfect
            }
            else
            {
                LabelStatus.text = "Best Record!";
            }
            LabelScore.text = string.Format("{0:00}", _currentHighScore);
            LabelTimeLeft.text = string.Format("{0:00}", _currentTimeLeft);
            _saveIt = true; // Let the Game Save the Score and Star Reward
        }
        else
        {
            Debug.Log("Score " + _currentHighScore + " Time Left: " + _currentTimeLeft);
            LabelScore.text = string.Format("{0:00}", _currentHighScore);
            LabelTimeLeft.text = string.Format("{0:00}", _currentTimeLeft);
        }

        //Show Star Rewards
        for (int grant = 0; grant < _starReward; grant++)
        {
            Stars[grant].enabled = true;
        }

        //Save Scorest
        SaveNewScores();
    }

    void SaveNewScores()
    {
        if (_saveIt)
        {
            //Recalculate and Save in total stars.
            CalculateTotalStars();
            //Save Highscore
            PlayerPrefs.SetInt(_highScoreLevelName, _currentHighScore);
            //Save Time Left
            PlayerPrefs.SetInt(_timeLeftLevelName, _currentTimeLeft);
            //Save Star Accomplishment
            PlayerPrefs.SetInt(_starRewardLevelName, _starReward);
            //Debug.Log("Scores Saved Total Stars = " + PlayerPrefs.GetInt("TotalStars"));
            _saveIt = false;
        }
    }
    bool IsPerfect()
    {
        //Check if Result is Perfect
        int PerfectScore = GameMngr.g_maxScore;
        int PerfectTimeLeft = Mathf.FloorToInt((GameMngr.g_timeLimit - (GameMngr.g_maxScore * GameMngr.g_BPS)) % 60f);
        Debug.Log(PerfectTimeLeft);
        if (_currentHighScore == PerfectScore && _currentTimeLeft >= PerfectTimeLeft)
        {
            return true;
        }
        return false;
    }
    bool IsNewHighScore()
    {
        if (IsThereSavedScore())
        {
            if (_currentHighScore == _savedHighScore)
            {
                if(_currentTimeLeft > _savedTimeLeft)
                {
                   return true; // New High Score because Time is Better even if Score is Equal
                }
                else
                {
                    return false;
                }
            }
            else if (_currentHighScore > _savedHighScore)
            {
                return true; // New High Score because Score is Better
            }
            else
            {
                return false;
            }
        }
        return true; // New High Score because There is no Score saved
    }
    bool IsThereSavedScore()
    {
        if (PlayerPrefs.GetInt("Level_"  + _gameLevelID +  "_HighScore") > 0)
        {
            return true;
        }
        return false;
    }
    int StarRewards()
    {
        float _pointsPercentage = (float)_currentHighScore / (float)GameMngr.g_maxScore;
        Debug.Log(_pointsPercentage.ToString());
        if (_pointsPercentage > 0.0f && _pointsPercentage <= 0.5f) // 1 Star = 1% - 50% of Max Score
        {
            return 3;
        }else if (_pointsPercentage > 0.5f && _pointsPercentage <= 1f && !IsPerfect())
        {
            return 3;
        }else if (IsPerfect())
        {
            return 3;
        }

        return 0;
    }
    void CalculateTotalStars()
    {
        //Get Total Stars
        int totalstars = PlayerPrefs.GetInt("TotalStars");
        //Compare Current and Saved Star Rewards to see if there is a difference
        int starDifference = _starReward - _savedStarReward;
        if (starDifference > 0)
        {
            //Add Positive Difference to Total Stars
            totalstars += starDifference;
            PlayerPrefs.SetInt("TotalStars", totalstars); //Uncomment if done
            Debug.Log("Saved");
        }
    }

    // NOTE THIS IS FOR DEBUG ONLY DONT USE UNLESS YOU KNOW WHAT YOU ARE DOING
    [ContextMenu("Delete All Player Prefs")]
    void DeleteData()
    {
        PlayerPrefs.DeleteAll();
        Debug.LogError("ALL DATA DELETED");
    }
}
