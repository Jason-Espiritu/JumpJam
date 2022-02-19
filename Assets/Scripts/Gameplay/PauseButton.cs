using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    [SerializeField] Canvas ScreenPaused;
    bool _isGamePaused;
    public void Pause()
    {
        if (!_isGamePaused)
        {
            Time.timeScale = 0f;
            _isGamePaused = true;
            //Reveal Pause Screen
            ScreenPaused.gameObject.SetActive(true);

        }
        else
        {
            ScreenPaused.gameObject.SetActive(false);
            _isGamePaused = false;
            Time.timeScale = 1f;
        }
    }
}
