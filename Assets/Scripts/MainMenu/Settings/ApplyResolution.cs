using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApplyResolution : MonoBehaviour
{
    [SerializeField] private Button _interactableState;

    private void Start()
    {
        
    }

    public void ResolutionValueIsChanged()
    {
        _interactableState.interactable = true;
    }

    public void AppliedResolution()
    {
        _interactableState.interactable = false;
    }
}
