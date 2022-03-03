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

    private void Awake()
    {
        Instance = this;
    }
    public void ShowConfirmBox(string confirmText, Action yesAction, Action noAction)
    {
        confirmBox.enabled = true;
        dialog.text = confirmText;
        yesBtn.onClick.AddListener(() => {
            confirmBox.enabled = false;
            yesAction();
        });
        noBtn.onClick.AddListener(() => {
            confirmBox.enabled = false;
            noAction();
        });
    }

}
