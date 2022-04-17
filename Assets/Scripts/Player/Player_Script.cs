using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour
{

    [SerializeField] private float _inputRange;
    [SerializeField] private GameManager GM;
    [SerializeField] private Animator PlayerAnimator;
    [SerializeField] private float _speedMultiplier;
    [SerializeField] private float _jumpForce = 22f;
    [SerializeField] private float _fallMultiplier = 5f;
    [SerializeField] private float _peakjump = 5f;
    [SerializeField] private Transform _feetPos;
    [SerializeField] private float _checkRadius;
    [SerializeField] private LayerMask _GroundLayer;
    [SerializeField] private bool _isJumping;
    
    [Header("Player Notification")]
    [SerializeField] private GameObject _parentGameObject;
    [SerializeField] private GameObject _jumpNotif;
    
    private Rigidbody2D _playerRigidBody;
    
    //constant variables
    private const float _constJF = 00.5f;

    // Start is called before the first frame update
    void Start()
    {
        _playerRigidBody = GetComponent<Rigidbody2D>();
        _speedMultiplier = GameManager.GMInstance.g_BPM_Multiplier;

        _playerRigidBody.gravityScale *= _speedMultiplier;
        _jumpForce *= (_constJF * _speedMultiplier) + _constJF;
        _fallMultiplier *= _speedMultiplier;
        _peakjump *= _speedMultiplier;

    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Space)){
            try
            {
                if(Dialogue_System.Instance.IsDialogueFinished() && !GM.g_isGameEnded){
                    RightBeat();
                    //Jump();
                }
            }
            catch (System.Exception)
            {
                if(!GM.g_isGameEnded){
                    RightBeat();
                    //Jump();
                }
            }
        }
    }
    private void FixedUpdate()
    {
        FallMultiplier(_isJumping);
    }

    public void RightBeat()
    {
        if (!GM.g_isGameEnded)
        {
            if (GroundCheck())
            {
                float TimeofInput = Timer_Global.Instance.g_timer;
                string jumpNotif = "";
                if(TimeofInput <= _inputRange || TimeofInput >= GameManager.GMInstance.g_BPS - _inputRange)
                {
                        
                    Jump(true);
                    Debug.Log("GREAT" + TimeofInput + " : " + GameManager.GMInstance.g_BPS);
                    jumpNotif = "Great";
                    AudioManager.instance.PlaySFX(4);

                    if (!GM.g_isGameStarted) { GM.g_isGameStarted = true; Timer_Global.Instance.g_timer = 0f; } // Checks if 1st (Start) Jump
                    
                    //Score
                    if (GM.g_isGameStarted)
                    {
                        Point_System.PSinstance.AddBeatValue(true);
                        jumpNotif += " + 1"; //Add Added Value
                    }
                }
                else
                {
                    Jump(false);
                    AudioManager.instance.PlaySFX(5);
                    
                    //Checks if it's too early or too late.
                    float _middleBPS = GameManager.GMInstance.g_BPS / 2;
                    if(TimeofInput >= _middleBPS){
                        jumpNotif = "Too Early";
                    }else
                    {
                        jumpNotif = "Too Late";
                    }
                    Debug.Log("BAD " + TimeofInput + " : " + _middleBPS);
                    
                    //Score
                    if (GM.g_isGameStarted)
                    {
                        Point_System.PSinstance.AddBeatValue(false);
                        jumpNotif += " - 1"; //Add subtracted Value
                    }


                }
                //Sends string to GameManager
                GM.g_jumpNotif = jumpNotif;
                SpawnNotification();
            }
        }
    }

    public void Jump(bool isGreatJump)
    {
        if (!GM.g_isGameEnded) // If Game is Ended... It can't jump anymore
        {
            if (GroundCheck())
            {
                if (isGreatJump)
                {
                    _playerRigidBody.velocity = Vector2.up * _jumpForce;
                }
                else
                {
                    _playerRigidBody.velocity = Vector2.up * (_jumpForce / 2); //Short Jump
                }

                _isJumping = true;

                //PlaySFX(); // Plays Jump sfx *Not being used due to no good sfx
                PlayerAnimator.SetBool("Jumping", true); // Execute Jump Anim
            }
        }
    }
    private bool GroundCheck()
    {
        Collider2D groundCheck = Physics2D.OverlapCircle(_feetPos.position, _checkRadius, _GroundLayer);

        if (groundCheck != null && _playerRigidBody.velocity.y == 0f)
        {
            return true;
        }

        return false;
    }
    private void FallMultiplier(bool fromJump)
    {
        if (!GroundCheck() && _playerRigidBody.velocity.y < _peakjump)
        {
            if (fromJump)
            {
                _playerRigidBody.velocity += Vector2.up * Physics2D.gravity.y * _fallMultiplier * Time.fixedDeltaTime;
            }
        }

        if (_playerRigidBody.velocity.y == 0f)
        {
            _isJumping = false;
        }

        //Animation Executions
        if (GroundCheck())
        {
            PlayerAnimator.SetBool("Falling", false);
        }

        if(_playerRigidBody.velocity.y < 0)
        {
            PlayerAnimator.SetBool("Falling", true); //Play Falling Animation
            PlayerAnimator.SetBool("Jumping", false);
        }
    }

    public void SpawnNotification()//Spawns the Notification
    {
        Instantiate(_jumpNotif, _parentGameObject.transform);
    }
}
