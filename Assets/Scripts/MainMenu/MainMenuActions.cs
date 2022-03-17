using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuActions : MonoBehaviour
{
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
}
