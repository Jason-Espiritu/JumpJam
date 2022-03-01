using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowStars : MonoBehaviour
{
    [SerializeField] private int LevelID;
    [SerializeField] private GameObject[] Stars;
    private int _starsObtainedPerLevel;
    // Start is called before the first frame update
    void Awake()
    {
        _starsObtainedPerLevel = PlayerPrefs.GetInt("StarofLevel_" + LevelID);
    }
    
}
