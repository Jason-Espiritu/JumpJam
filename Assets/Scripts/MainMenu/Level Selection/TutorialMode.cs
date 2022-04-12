using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialMode : MonoBehaviour
{
    [SerializeField] private int LevelID;
    
    public void ShowDifficultyBox(){
        DifficultySelection.Instance.ShowDiffBox(
            () => {SceneManager.LoadScene("Level "+ LevelID + " - N");},
            () => {SceneManager.LoadScene("Level "+ LevelID +" - H");},
            () => {/* Do Nothing */}
        );
    }
}
