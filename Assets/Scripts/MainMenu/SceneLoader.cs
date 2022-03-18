using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] VideoPlayer Intro;

    void Awake(){
        Intro.Play();
        Intro.loopPointReached += LoadScene;
    }

    void LoadScene(UnityEngine.Video.VideoPlayer vp)
    {
        LoadSceneImmediately();
    }

    public void LoadSceneImmediately(){
        SceneManager.LoadScene("Lobby");
    } 
}
