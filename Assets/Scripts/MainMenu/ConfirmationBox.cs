using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConfirmationBox : MonoBehaviour
{
    public static ConfirmationBox Instance { get; private set; }

    [SerializeField] private Canvas confirmBox;
    [SerializeField] private TMP_Text dialog;
    [SerializeField] private Button yesBtn;
    [SerializeField] private Button noBtn;

    Action _yesAction;
    Action _noAction;
    private void Awake()
    {
        Instance = this;
    }
    public void ShowConfirmBox(string confirmText, Action yesAction, Action noAction)
    {
        confirmBox.enabled = true;
        dialog.text = confirmText;
        _yesAction = yesAction;
        _noAction = noAction;
        yesBtn.onClick.AddListener(YesClicked);
        noBtn.onClick.AddListener(NoClicked);
    }

    void YesClicked(){
        confirmBox.enabled = false;
        _yesAction();
        RemoveBtnListeners();
    }

    void NoClicked(){
        confirmBox.enabled = false;
        _noAction();
        RemoveBtnListeners();
    }

    void RemoveBtnListeners(){
        yesBtn.onClick.RemoveListener(YesClicked);
        noBtn.onClick.RemoveListener(NoClicked);
    }

    public void PlaySoundFX(int sfxID){
        AudioManager.instance.PlaySFX(sfxID);
    }
}
