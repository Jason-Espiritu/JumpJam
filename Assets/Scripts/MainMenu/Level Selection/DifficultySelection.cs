using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DifficultySelection : MonoBehaviour
{
    public static DifficultySelection Instance {get; private set;}

    [Header("Tutorial Mode?")]
    [SerializeField] private bool _isTutorialMode;
    [Header("UI Objects")]
    [SerializeField] private int LevelID;
    [SerializeField] private int perfectScore;
    [SerializeField] private TMP_Text highScore_N;
    [SerializeField] private TMP_Text highScore_H;
    [SerializeField] private Canvas dialogueBox;
    [SerializeField] private Button normalBtn;
    [SerializeField] private Button hardBtn;
    [SerializeField] private Button backBtn;
    [SerializeField] private Image[] stars;

    Action _normalMode;
    Action _hardMode;
    Action _back;

    private void Awake()
    {
        Instance = this;
        
        hardBtn.interactable = false; // set hard mode to false
    }
    public void ShowDiffBox(Action normalModeAction, Action hardModeAction, Action backAction)
    {
        RevealStars();
        dialogueBox.enabled = true;
        _normalMode = normalModeAction;
        _hardMode = hardModeAction;
        _back = backAction;
        normalBtn.onClick.AddListener(NormalClicked);
        hardBtn.onClick.AddListener(HardClicked);
        backBtn.onClick.AddListener(BackClicked);
    }

    void NormalClicked(){
        dialogueBox.enabled = false;
        _normalMode();
        RemoveBtnListeners();
    }

    void HardClicked(){
        dialogueBox.enabled = false;
        _hardMode();
        RemoveBtnListeners();
    }
    void BackClicked(){
        dialogueBox.enabled = false;
        _back();
        RemoveBtnListeners();
    }

    void RemoveBtnListeners(){
        normalBtn.onClick.RemoveListener(NormalClicked);
        hardBtn.onClick.RemoveListener(HardClicked);
        backBtn.onClick.RemoveListener(BackClicked);
    }

    public void RevealStars(){
        if(_isTutorialMode){
           int starsonTutorial = PlayerPrefs.GetInt("Level_" + LevelID + "_Star");
           for (int i = 0; i < starsonTutorial; i++)
           {
               stars[i].enabled = true;
           }
            //Unlocks Hard Mode when Normal tutorial is Finished
            if (PlayerPrefs.GetInt("NormalTutor?") == 1)
            {
                hardBtn.interactable = true;
            }
        }else{ //Rest of the Levels
            int starsOnNormal = PlayerPrefs.GetInt("Level_" + LevelID + "-1_Star");
            int highScoreN = PlayerPrefs.GetInt("Level_" + LevelID + "-1_HighScore");
            int highScoreH = PlayerPrefs.GetInt("Level_" + LevelID + "-2_HighScore");
            for (int i = 0; i < starsOnNormal; i++)
           {
               stars[i].enabled = true;
           }
            highScore_N.text = string.Format("{0:00}/{1:00}", highScoreN, perfectScore);
            highScore_H.text = string.Format("{0:00}/{1:00}", highScoreH, perfectScore);
           //Unlocks Hard Mode if Normal has Stars
           if (starsOnNormal > 0){
               hardBtn.interactable = true;
                int starsOnHard = PlayerPrefs.GetInt("Level_" + LevelID + "-2_Star");
                for (int i = 0; i < starsOnHard; i++)
                {
                    stars[i + 3].enabled = true;
                }
            }
        }
    }
    public void PlaySoundFX(int sfxID){
        AudioManager.instance.PlaySFX(sfxID);
    }

}

