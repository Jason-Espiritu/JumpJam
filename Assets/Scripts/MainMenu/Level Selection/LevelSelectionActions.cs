using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectionActions : MonoBehaviour
{   
    [SerializeField] private int _musicTrack;
    [SerializeField] private Settings Settings;

    void Start(){
        PlayLevelAudio(_musicTrack);
        CheckMusicAudio();
    }
    private void PlayLevelAudio(int musicIndex){
        AudioManager.instance.PlayMenuMusic(musicIndex);
    }
    public void ChangeScene(string nameOfScene){
        SceneManager.LoadScene(nameOfScene);
    }

    public void PlaySoundFX(int sfxID){
        AudioManager.instance.PlaySFX(sfxID);
    }

    public void ShowDifficultyBox(int LevelID){
        DifficultySelection.Instance.ShowDiffBox(
            () => {NormalModeCheck(LevelID);},
            () => {HardModeCheck(LevelID);},
            () => {/* Do Nothing */}
        );
    }

    void HardModeCheck(int LevelID){
        int isHardTutorPlayed = PlayerPrefs.GetInt("HardTutor?");
        switch (isHardTutorPlayed)
        {
            case 1:
                SceneManager.LoadScene("Level "+ LevelID +" - H");
                break;
            default:
                ConfirmationBox.Instance.ShowConfirmBox("Tutorial for Hard Mode is Needed\nPlay Hard Mode Tutorial?",
                () => {SceneManager.LoadScene("Level 0 - H");},
                () => {/*Do Nothing*/}
                );
                break;
        }
    }
    void NormalModeCheck(int LevelID)
    {
        int isHardTutorPlayed = PlayerPrefs.GetInt("NormalTutor?");
        switch (isHardTutorPlayed)
        {
            case 1:
                SceneManager.LoadScene("Level " + LevelID + " - N");
                break;
            default:
                ConfirmationBox.Instance.ShowConfirmBox("Tutorial for Normal Mode is Needed\nPlay Normal Mode Tutorial?",
                () => { SceneManager.LoadScene("Level 0 - N"); },
                () => {/*Do Nothing*/}
                );
                break;
        }
    }

    public void CheckMusicAudio() //Checks if Volume of Master or Music is 0
    {
        if (AudioManager.instance.g_valMasterSet == 0.0001f || AudioManager.instance.g_valMusicSet == 0.0001f)
        {
            AlertBox.Instance.ShowAlertBox("Music / Master Volume has been set to 0\nPlease set the Master / Music Volume Above 0",
                () =>
                {
                    Settings.ShowHideSettings(true);
                });
        }
    }
}
