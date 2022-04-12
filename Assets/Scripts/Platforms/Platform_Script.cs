using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Platform_Script : MonoBehaviour
{
    //Debug Values
    //public Text PrintDebugger;
    public bool Player;

    [SerializeField] private Transform _platformCollisionBox;
    [SerializeField] private Platform_Identity _platIdentity;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private bool _isPlatformRemainClosed;
    [SerializeField] private float _xAxis;
    [SerializeField] private float _xAxisBase;
    [SerializeField] private bool _moveRight;
    [SerializeField] private LeanTweenType Ease;

    private Vector2 _cornerRect1;
    private Vector2 _cornerRect2;

    private bool _isNormalMode;

    private bool _isPlatformOpen;
    
    //[SerializeField]private float _openPlatformTrigger;
    //private float _closePlatformTrigger;
    //private float _Beat;

    //private Vector2 _currentPosition;
    //private Vector2 _temporaryVector2;

    private void Start()
    {
        _xAxisBase = gameObject.transform.position.x; //Gets X Axis Base Location
        _isNormalMode = GameManager.GMInstance.g_normalMode;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlayerOnPlatform(_isNormalMode) || IsPlayerOnPlatform(_platIdentity.g_isFinalPlatform))
        {
            _isPlatformRemainClosed = true; //Permanently Colse the Platform if Game is in Normal Mode
        }

        if (!_isPlatformRemainClosed)
        {
            if (!Timer_Global.Instance.g_openPlatform)
            {
                //DebugF(false, _isPlatformOpen); // Close Platform
            }
            else if (Timer_Global.Instance.g_openPlatform)
            {
                //DebugF(true, _isPlatformOpen); //Open Platform
            }
        }
        //Player = PlayerCheck(_normalMode);
    }

    private void DebugF(bool open, bool isplatformOpen)
    {
        if (open && !isplatformOpen)
        {
            //Debug.Log("Open");

            _isPlatformOpen = true;
            if (_moveRight)
            {
                LeanTween.moveLocalX(gameObject, _xAxis, GameManager.GMInstance.g_BPS / 2f).setEase(Ease);
            }
            else
            {
                LeanTween.moveLocalX(gameObject, _xAxis * -1f, GameManager.GMInstance.g_BPS / 2f).setEase(Ease);
            }
        }
        else if (!open && isplatformOpen)
        {
            //Debug.Log("Close");
            _isPlatformOpen = false;
            LeanTween.moveLocalX(gameObject, _xAxisBase, GameManager.GMInstance.g_BPS / 2f).setEase(Ease);
        }
    }


    private bool IsPlayerOnPlatform(bool situation)
    {
        if (situation)
        {
            _cornerRect1 = new Vector2(_platformCollisionBox.position.x - 1f, _platformCollisionBox.position.y + 0.2f);
            _cornerRect2 = new Vector2(_platformCollisionBox.position.x + 1f, _platformCollisionBox.position.y + 1f);
            
            Collider2D playercheck = Physics2D.OverlapArea(_cornerRect1, _cornerRect2, _playerLayer);
            if (playercheck != null)
            {
                return true;
            }
        }
        return false;
    }


    //private void PlatformAction(bool direction, bool openPlatform)
    //{
    //    if (openPlatform && !_isPlatformOpen) //Open
    //    {
    //        switch (direction)
    //        {
    //            case true: // Move Platform to the Right
    //                //Debug.Log("Platform Moved Right");
    //                LeanTween.moveLocalX(gameObject, 4f, 0.5f).setEase(Ease);
    //                break;
    //            case false: // Move Platform to the Left
    //                //Debug.Log("Platform Moved Left");
    //                LeanTween.moveLocalX(gameObject, -4f, 0.5f).setEaseInOutBack();
    //                break;
    //        }
    //        _isPlatformOpen = true;  
    //    }
    //    else if (!openPlatform && _isPlatformOpen) //Close
    //    {
    //        LeanTween.moveLocalX(gameObject, 0f, 0.5f).setEase(Ease);

    //        _isPlatformOpen = false;
    //    }
    //}
}
