using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelUnlock : MonoBehaviour
{
    //[SerializeField] private GameObject[] Levels; //Uncomment if implemented
    [SerializeField] private TMP_Text TotalStars; //Uncomment if implemented

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
        UnlockedLevels(_obtainedStars);
        //Show Total Stars Obtained
        TotalStars.text = string.Format("{0:00}", _obtainedStars); //Uncomment if implemented
    }

    void UnlockedLevels(int totalStars)
    {
        int[] starRequirements = {3, 6, 12, 15, 20, 30, 37, 40, 49, 51, 63};
        Debug.Log(_obtainedStars);
        for (int level = 0; level < starRequirements.Length; level++)
        {
            if (totalStars >= starRequirements[level])
            {
                //Unlock Level
                Debug.Log("Level " + (level + 1) + " unlocked");
            }
        }
    }
}
