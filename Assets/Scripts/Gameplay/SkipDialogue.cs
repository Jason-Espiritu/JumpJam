using UnityEngine;
using UnityEngine.UI;

public class SkipDialogue : MonoBehaviour
{
    [SerializeField] Button SkipButton;
    public void HideSkipButton(){
        SkipButton.enabled = false;
    }
}
