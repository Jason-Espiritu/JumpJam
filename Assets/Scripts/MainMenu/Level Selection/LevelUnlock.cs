using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class LevelUnlock : MonoBehaviour
{
    [SerializeField] private bool _UnlockAll;
    [SerializeField] private TMP_Text TotalStars; //Uncomment if implemented
    [SerializeField] private Button[] Levels; //Uncomment if implemented
    //public int g_starsobtained; // comment this line if done testing

    private int _obtainedStars;
    // Start is called before the first frame update
    void Awake()
    {
        _obtainedStars = PlayerPrefs.GetInt("TotalStars"); //Uncomment if done testing
        //_obtainedStars = g_starsobtained; //Comment this line if done testing.
    }

    private void Start()
    {   
        //Loadsif it Unlock All was enabled.
        _UnlockAll = Convert.ToBoolean(PlayerPrefs.GetInt("UnlockAll?"));
        if(!_UnlockAll){
            UnlockedLevels(_obtainedStars);
        }else{
            UnlockAll();
        }
        //Show Total Stars Obtained
        TotalStars.text = string.Format("{0:00}", _obtainedStars); //Uncomment if implemented
    }

    private void UnlockAll()
    {
        for (int i = 0; i < Levels.Length; i++)
        {
            Levels[i].interactable = true;
        }
    }

    void UnlockedLevels(int totalStars)
    {
        int[] starRequirements = {3, 6, 12, 15, 20, 28, 31, 34, 40, 51};
        Debug.Log(_obtainedStars);
        for (int level = 0; level < starRequirements.Length; level++)
        {
            if (totalStars >= starRequirements[level])
            {
                //Unlock Level
                Debug.Log("Level " + (level + 1) + " unlocked");
                Levels[level].interactable = true;
            }
        }
    }
}
