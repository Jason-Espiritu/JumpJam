using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    [SerializeField] Canvas ScreenPaused;
    bool _isGamePaused;
    public void Pause()
    {
        if (!GameManager.GMInstance.g_isGameEnded)
        {
            if (!_isGamePaused)
            {
                Time.timeScale = 0f;
                _isGamePaused = true;
                //Reveal Pause Screen
                ScreenPaused.enabled = true;

            }
            else
            {
                ScreenPaused.enabled = false;
                _isGamePaused = false;
                Time.timeScale = 1f;
            }
        }
    }
}
