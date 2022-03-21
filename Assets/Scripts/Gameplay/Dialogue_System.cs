using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue_System : MonoBehaviour
{
    public static Dialogue_System Instance;
    [SerializeField] Canvas _dialogueBox;
    [SerializeField] GameObject _nextButton;
    [SerializeField] GameObject _jumpButton;
    [SerializeField] TMP_Text _StartLabel;
    [SerializeField] TMP_Text _textDisplay;
    [SerializeField] string[] _sentences;
    [SerializeField] float _typingspeed;
    private int index; 
    private bool _isDialogueFinished;
    // Start is called before the first frame update

    void Awake(){
        if(Instance == null){
            Instance = this;
        }else{
            Destroy(gameObject);
        }
    }
    void Start()
    {
        _nextButton.SetActive(false);
    }

    void Update(){
        if (_textDisplay.text == _sentences[index]){
            _nextButton.SetActive(true);
        }
    }

    IEnumerator TypeDialogue(){
        foreach (char letter in _sentences[index].ToCharArray())
        {
            _textDisplay.text += letter;
            yield return new WaitForSeconds(_typingspeed);
        }
    }

    public void NextSentence(){

        _nextButton.SetActive(false);

        if (index < _sentences.Length -1){
            index++;
            _textDisplay.text = "";
            StartCoroutine(TypeDialogue());
        }else{ // Dialouge is Finished
            _textDisplay.text = "";
            _isDialogueFinished = true;
            _nextButton.SetActive(false);
            _dialogueBox.enabled = false;
            _jumpButton.SetActive(true);
            _StartLabel.enabled = true;
        }
    }

    public void DisplayDialogue(string[] _dialogueLines){
        //Hides Jump Function Stopping from working until reactivated
        if(_jumpButton.activeSelf){
            _jumpButton.SetActive(false);
        }
        _isDialogueFinished = false;
        _sentences = _dialogueLines;
        index = 0;
        _dialogueBox.enabled = true;
        _textDisplay.text = "";
        StartCoroutine(TypeDialogue());
    }

    public bool IsDialogueFinished(){
        return _isDialogueFinished;
    }

    public void SkipDialogue(){
        StopCoroutine(TypeDialogue());
        _textDisplay.text = "";
        _isDialogueFinished = true;
        _nextButton.SetActive(false);
        _dialogueBox.enabled = false;
        _jumpButton.SetActive(true);
        _StartLabel.enabled = true;
    }
}
