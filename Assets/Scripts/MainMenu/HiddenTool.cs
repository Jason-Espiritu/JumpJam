using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenTool : MonoBehaviour
{
    [SerializeField] private GameObject ToolBox;

    // NOTE THIS IS FOR DEBUG ONLY DONT USE UNLESS YOU KNOW WHAT YOU ARE DOING

    public void DeleteData()
    {
        ConfirmationBox.Instance.ShowConfirmBox("Are you sure you want to Delete all of Data?\nThe game will Close doing this action.",
        () => {
            PlayerPrefs.DeleteAll();
            AlertBox.Instance.ShowAlertBox("Data has Been Reset.\nClosing the Application to take effect.",
            () => {Application.Quit();}
            );
        },
        () => {/*Do Nothing*/}
        );
    }
    public void UnlockAll()
    {
        //PlayerPrefs.
        ConfirmationBox.Instance.ShowConfirmBox("Are you sure you want to Unlock all Levels?",
        () => {
            PlayerPrefs.SetInt("UnlockAll?", 1);
            AlertBox.Instance.ShowAlertBox("All Levels are now Unlocked", () => {});
        },
        () => {/*Do Nothing*/}
        );
    }

    public void OpenHiddenTools(){
        if(ToolBox.activeSelf){
            ToolBox.SetActive(false);
        }else{
            ToolBox.SetActive(true);
        } 
    }
}
