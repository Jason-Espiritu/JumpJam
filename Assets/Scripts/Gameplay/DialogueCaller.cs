using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueCaller : MonoBehaviour
{
    [SerializeField] private int _dialogueSetIndex;
    [SerializeField] private GameObject _jumpButton;
    [SerializeField] private Button _skipButton;
    [SerializeField] private Dialogue_System _dialogueSystem;
    [SerializeField] private Scriptable_Dialogue[] _dialogueSets;

    void Start(){
        _jumpButton.SetActive(false);
        PlayDialogue(_dialogueSetIndex);
    }
    void Update(){

    }

    public void PlayDialogue(int _dialogueSetIndex){
        _dialogueSystem.DisplayDialogue(_dialogueSets[_dialogueSetIndex]._sentences);
        _skipButton.enabled = true;
        //Debug.Log("Dialogue Playing");
    }
}
