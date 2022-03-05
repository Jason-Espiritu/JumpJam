using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    public void LevelSelect()
    {
        if (gameManager.g_isGameEnded)
        {
            ConfirmLevelSelect();
        }
        else
        {
            ConfirmationBox.Instance.ShowConfirmBox(
                "Go Back to Level Selection?",
                () => { ConfirmLevelSelect(); },

                () => {
                    //Do Nothing
                });
        }
    }
    private void ConfirmLevelSelect()
    {
        //Checks if the Whole Game is still Paused
        if(Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
        //Go Back to Level Selection
        SceneManager.LoadScene("Level Selection");
    }
    public void Restart()
    {
        ConfirmationBox.Instance.ShowConfirmBox(
            "Restart the Level?",
            () => { ConfirmRestart(); },

            () => {
                //Do Nothing
            });
    }
    void ConfirmRestart()
    {
        //Checks if the Whole Game is still Paused
        if(Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
        //Restart the Stage
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowSettings()
    {
        //Show Settings

    }
}
