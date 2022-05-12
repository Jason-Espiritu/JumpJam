using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndGame : MonoBehaviour
{
    [SerializeField] Canvas ResultsScreen;
    [SerializeField] TMP_Text LabelTimingScore;
    [SerializeField] TMP_Text LabelCollectibleScore;
    [SerializeField] TMP_Text LabelTotalScore;
    [SerializeField] TMP_Text LabelStatus;
    [SerializeField] Image[] Stars;

    [SerializeField] float DelayInSeconds;
    private GameManager GameMngr;
    private Point_System Points;


    private bool _GameEndProcessed;

    private string _gameLevelID;

    //Saved HighScore and Lowest time from
    private int _savedHighScore;
    private int _savedStarReward;
    
    //Current
    private int _currentTotalScore;
    private int _currentCollectedCollectables;
    private int _currentScore;
    private int _starReward;

    //This to create a standard name for the Level.
    private string _highScoreLevelName;
    private string _starRewardLevelName;

    // Start is called before the first frame update
    void Start()
    {
        GameMngr = GetComponent<GameManager>();
        Points = GetComponent<Point_System>();


        _gameLevelID = GameMngr.g_GameLevelID;

        //Recompile Full Name of Level
        _highScoreLevelName = "Level_" + _gameLevelID + "_HighScore";
        _starRewardLevelName = "Level_" + _gameLevelID + "_Star";

        //Load HighScore
        if (IsThereSavedScore())
        {
            _savedHighScore = PlayerPrefs.GetInt(_highScoreLevelName);
            _savedStarReward = PlayerPrefs.GetInt(_starRewardLevelName);
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
        //Debug.Log("Game Finished");

        // Get Current Score
        _currentCollectedCollectables = Points.GetCollectibleScore();
        _currentScore = Points.GetTimingScore();
        _currentTotalScore = _currentCollectedCollectables + _currentScore;

        //Reveal Scores
        ResultsScreen.enabled = true;

        _starReward = StarRewards();
        //Check if Score is Perfect
        switch (_starReward)
        {
            case 1:
                LabelStatus.text = "Nice";
                if (IsNewHighScore())
                {
                    LabelStatus.text += " and Best Score!";
                    SaveNewScores();
                }
                
                break;

            case 2:
                LabelStatus.text = "Great Job";
                if (IsNewHighScore())
                {
                    LabelStatus.text += " and Best Score!";
                    SaveNewScores();
                }

                break;

            case 3:
                LabelStatus.text = "Perfect!";
                if (IsNewHighScore())
                {
                    LabelStatus.text += " and Best Score!";
                    SaveNewScores();
                }

                break;
            default:
                LabelStatus.text = "Sorry Try Again";
                break;
        }

        //Prints Result Values
        PrintResult(_currentScore, _currentCollectedCollectables, _currentTotalScore);

        //Show Star Rewards
        for (int grant = 0; grant < _starReward; grant++)
        {
            Stars[grant].enabled = true;
        }

        //Slowly Stop the BGM
        StartCoroutine(AudioManager.instance.FadeOut(10f));
    }

    void PrintResult(int timingScore, int collectibleScore, int totalScore)
    {
        LabelTimingScore.text = string.Format("Score x {0:00}", timingScore);
        LabelCollectibleScore.text = string.Format("Collectibles x {0:00}", collectibleScore);
        LabelTotalScore.text = string.Format("{0:00}", totalScore);
    }

    void SaveNewScores()
    {
        //Save Highscore
        PlayerPrefs.SetInt(_highScoreLevelName, _currentTotalScore);

        //Recalculate and Save in total stars.
        CalculateTotalStars();
        //Save Star Accomplishment
        PlayerPrefs.SetInt(_starRewardLevelName, _starReward);
        //Debug.Log("Scores Saved Total Stars = " + PlayerPrefs.GetInt("TotalStars"));
    }

    bool IsNewHighScore()
    {
        if (IsThereSavedScore())
        {
            if (_currentTotalScore > _savedHighScore)
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
        float _pointsPercentage = (float)_currentTotalScore / ((float)GameMngr.g_maxScore * 2);
        //Debug.Log(_pointsPercentage.ToString());
        if (_pointsPercentage > 0.0f && _pointsPercentage <= 0.5f) // 1 Star = 1% - 50% of Total Score
        {
            return 1;
        }else if (_pointsPercentage > 0.5f && _pointsPercentage < 1f) // 2 Star = 51 - 99% of Total Score
        {
            return 2;
        }else if (_pointsPercentage == 1f) // 3 Star = 100% of Total Score // Perfect Score
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
            //Debug.Log("Saved");
        }
    }



    //Unused Functions //Need to be removed after checking all Code
    bool IsPerfect()
    {
        //Check if Result is Perfect
        int PerfectScore = GameMngr.g_maxScore * 2;

        if (_currentTotalScore == PerfectScore)
        {
            return true;
        }
        return false;
    }
}
