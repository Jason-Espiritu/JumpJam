using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Point_System : MonoBehaviour
{
    public static Point_System PSinsttance;

    [SerializeField] private TMP_Text ScoreText;

    [SerializeField] private int _multiplier;
    [SerializeField] private int _overallPoints;

    private void Awake()
    {
        if (PSinsttance == null)
        {
            PSinsttance = this;
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

    public void AddPoint()
    {
        _overallPoints += _multiplier * 1;
        ShowScore();
    }

    private void ShowScore()
    {
        ScoreText.text = "Score\n" + _overallPoints.ToString();
    }

    //Save Score
}
