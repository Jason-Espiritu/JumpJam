using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCaller : MonoBehaviour
{
    [SerializeField] private int _dialogueSetIndex;
    [SerializeField] private GameObject _jumpButton;
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
        //Debug.Log("Dialogue Playing");
    }
}
