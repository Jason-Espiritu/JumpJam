using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowStars : MonoBehaviour
{
    [SerializeField] private int LevelID;
    [SerializeField] private Image[] Stars;
    private int _starsObtainedPerLevel;
    // Start is called before the first frame update
    void Awake()
    {
        //Get total Stars of a Level
        _starsObtainedPerLevel = PlayerPrefs.GetInt("Level_" + LevelID + "-1_Star") + PlayerPrefs.GetInt("Level_" + LevelID + "-2_Star");
        //Debug.Log(_starsObtainedPerLevel);
        //Show the Stars
        RevealStars();
    }
    
    void RevealStars()
    {
        for (int Star = 0; Star < _starsObtainedPerLevel; Star++)
        {
            Stars[Star].enabled = true;
        }
    }
}
