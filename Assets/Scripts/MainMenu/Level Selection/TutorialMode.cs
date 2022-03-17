using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialMode : MonoBehaviour
{
    [SerializeField] private int LevelID;
    [SerializeField] private Image[] Stars;
    private int _starsObtainedPerLevel;
    // Start is called before the first frame update
    void Awake()
    {
        //Get total Stars of a Level
        _starsObtainedPerLevel = PlayerPrefs.GetInt("Level_" + LevelID + "_Star");
        Debug.Log(_starsObtainedPerLevel);
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
    public void ShowDifficultyBox(){
        DifficultySelection.Instance.ShowDiffBox(
            () => {SceneManager.LoadScene("Level "+ LevelID + " - N");},
            () => {SceneManager.LoadScene("Level "+ LevelID +" - H");},
            () => {/* Do Nothing */}
        );
    }
}
