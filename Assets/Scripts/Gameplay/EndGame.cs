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

        //Check if Score is Perfect
        if (IsPerfect())
        {
            //Show Perfect Score
            Debug.Log("Perfect Score");
        }

        // Check if Score is a HighScore
        if (IsNewHighScore())
        {
            //Show Best Record
            Debug.Log("New High Score " + _currentHighScore + " Time Left: " + _currentTimeLeft);
            
            _saveIt = true; // Let the Game Save the Score and Star Reward
        }
        else
        {
            Debug.Log("Score " + _currentHighScore + " Time Left: " + _currentTimeLeft);
        }

        //Process Star Rewards
        _starReward = StarRewards();
        switch (_starReward)
        {
            case 1:  //Show 1 Stars
                Debug.Log("1 Star");
                break;
            case 2:  //Show 2 Stars
                Debug.Log("2 Star");
                break;
            case 3:  //Show 3 Stars / Perfect
                Debug.Log("3 Star");
                break;
            default: //Show No Star
                Debug.Log("0 Star");
                break;
        }
        //Save Scores
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
            Debug.Log("Scores Saved");
            _saveIt = false;
        }
    }

    bool IsPerfect()
    {
        //Check if Result is Perfect
        int PerfectScore = GameMngr.g_maxScore;
        int PerfectTimeLeft = Mathf.FloorToInt((GameMngr.g_timeLimit - (GameMngr.g_maxScore * GameMngr.g_BPS)) % 60f);
        if (_currentHighScore == PerfectScore && _currentTimeLeft == PerfectTimeLeft)
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
            return 1;
        }else if (_pointsPercentage > 0.5f && _pointsPercentage <= 1f && !IsPerfect())
        {
            return 2;
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
            //PlayerPrefs.SetInt("TotalStars", totalstars); //Uncomment if done
        }
    }
}
