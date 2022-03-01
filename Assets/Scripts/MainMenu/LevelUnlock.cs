using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUnlock : MonoBehaviour
{
    //[SerializeField] private GameObject[] Levels;
    public int g_starsobtained;
    private int _obtainedStars;
    // Start is called before the first frame update
    void Awake()
    {
        //_obtainedStars = PlayerPrefs.GetInt("totalstars");
        _obtainedStars = g_starsobtained; //Comment this line if done testing.
    }

    private void Start()
    {
        UnlockedLevels(_obtainedStars);
    }

    void UnlockedLevels(int totalStars)
    {
        int[] starRequirements = {3, 6, 12, 15, 20, 30, 37, 40, 49, 51, 63};

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
