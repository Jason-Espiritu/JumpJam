using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Point_System : MonoBehaviour
{
    public static Point_System PSinstance;

    [SerializeField] private TMP_Text ScoreText;
    [SerializeField] private TMP_Text CollectiblePointText;

    [SerializeField] private int _multiplier;
    [SerializeField] private int _collectiblePoints;
    [SerializeField] private int _greatTimingPoints;

    [HideInInspector] public int g_score; 
    private void Awake()
    {
        if (PSinstance == null)
        {
            PSinstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ShowScore();
    }

    public void AddCollectiblePoint()
    {
        _collectiblePoints += _multiplier * 1;
        ShowCollectibleScore();
        //PlaySFX(); //Not being used due to no appropriate sfx
    }

    public void AddBeatValue(bool isRightBeat)
    {
        if (isRightBeat)
        {
            _greatTimingPoints += 1;
        }
        else
        {
            if(_greatTimingPoints != 0)
            {
                _greatTimingPoints -= 1;
            }
        }
        ShowScore();
    }


    private void ShowScore()
    {
        ScoreText.text = string.Format("Score\n{0:00}", _greatTimingPoints);
    }
    private void ShowCollectibleScore()
    {
        CollectiblePointText.text = string.Format("x {0:00}", _collectiblePoints);
    }

    //Save Score When Game Ended
    public int GetCollectibleScore()
    {
        return _collectiblePoints;
    }
    public int GetTimingScore()
    {
        return _greatTimingPoints;
    }
}
