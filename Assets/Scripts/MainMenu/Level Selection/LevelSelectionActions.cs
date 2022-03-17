using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectionActions : MonoBehaviour
{
    public void ChangeScene(string nameOfScene){
        SceneManager.LoadScene(nameOfScene);
    }

    public void PlaySoundFX(int sfxID){
        AudioManager.instance.PlaySFX(sfxID);
    }

    public void ShowDifficultyBox(int LevelID){
        DifficultySelection.Instance.ShowDiffBox(
            () => {SceneManager.LoadScene("Level "+ LevelID + " - N");},
            () => {SceneManager.LoadScene("Level "+ LevelID +" - H");},
            () => {/* Do Nothing */}
        );
    }
}
