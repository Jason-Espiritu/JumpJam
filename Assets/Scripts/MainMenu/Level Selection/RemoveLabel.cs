
using UnityEngine;
using UnityEngine.UI;

public class RemoveLabel : MonoBehaviour
{
    [SerializeField] private GameObject StarReqLabel;
    [SerializeField] private Button LevelButton;
    bool hasChanged; 
    void Update()
    {
        if(LevelButton.interactable && !hasChanged){
            StarReqLabel.SetActive(false);
            hasChanged = true;
        }
    }
}
