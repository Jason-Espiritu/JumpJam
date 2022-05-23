using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuActions : MonoBehaviour
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
    public void ExitGame(){
        ConfirmationBox.Instance.ShowConfirmBox(
            "Are you sure you want to Quit?",
            () => 
            {
                Application.Quit(); //Quit Application
            },
            () => {
                // DO Nothing
            }
        );
    }

    public void MoveToLevelSelection(){
        SceneManager.LoadScene("Level Selection");
    }
    public void PlaySoundFX(int sfxID){
        AudioManager.instance.PlaySFX(sfxID);
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
