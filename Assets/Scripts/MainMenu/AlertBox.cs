using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AlertBox : MonoBehaviour
{
    public static AlertBox Instance { get; private set; }

    [SerializeField] private Canvas confirmBox;
    [SerializeField] private TMP_Text dialog;
    [SerializeField] private Button okBtn;

    Action _okAction;
    private void Awake()
    {
        Instance = this;
    }
    public void ShowAlertBox(string alertText, Action okAction)
    {
        confirmBox.enabled = true;
        dialog.text = alertText;
        _okAction = okAction;
        okBtn.onClick.AddListener(OkClicked);
    }

    void OkClicked(){
        confirmBox.enabled = false;
        _okAction();
        RemoveBtnListeners();
    }

    void RemoveBtnListeners(){
        okBtn.onClick.RemoveListener(OkClicked);
    }

    public void PlaySoundFX(int sfxID){
        AudioManager.instance.PlaySFX(sfxID);
    }
}
