using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Point_System : MonoBehaviour
{
    public static Point_System PSinstance;

    [SerializeField] private TMP_Text ScoreText;

    [SerializeField] private int _multiplier;
    [SerializeField] private int _overallPoints;

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

    public void AddPoint()
    {
        _overallPoints += _multiplier * 1;
        ShowScore();
        //PlaySFX(); //Not being used due to no appropriate sfx
    }

    private void ShowScore()
    {
        ScoreText.text = string.Format("Score\n{0:00}", _overallPoints);
    }

    void PlaySFX(){
        AudioManager.instance.PlaySFX(4);
    }

    //Save Score When Game Ended
    public int GetScore()
    {
        return _overallPoints;
    }
}
