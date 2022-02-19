using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour
{
    [SerializeField] private GameManager GM;
    [SerializeField] private float _speedMultiplier;
    [SerializeField] private float _jumpForce = 22f;
    [SerializeField] private float _fallMultiplier = 5f;
    [SerializeField] private float _peakjump = 5f;
    [SerializeField] private Transform _feetPos;
    [SerializeField] private float _checkRadius;
    [SerializeField] private LayerMask _GroundLayer;
    
    private Rigidbody2D rb;
    
    [SerializeField] private bool _isJumping;

    //constant variables
    private const float _constJF = 00.5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _speedMultiplier = GameManager.GMInstance.g_BPM_Multiplier;

        rb.gravityScale *= _speedMultiplier;
        _jumpForce *= (_constJF * _speedMultiplier) + _constJF;
        _fallMultiplier *= _speedMultiplier;
        _peakjump *= _speedMultiplier;

    }

    private void FixedUpdate()
    {
        FallMultiplier(_isJumping);
    }

    public void Jump()
    {
        if (GroundCheck())
        {
            rb.velocity = Vector2.up * _jumpForce;
            _isJumping = true;
        }
        if(!GM.g_isGameStarted && !AudioManager.instance.MusicSource.isPlaying)
        {
            AudioManager.instance.MusicSource.time = 0.7f;
            AudioManager.instance.MusicSource.Play();
            GM.g_isGameStarted = true;
        }
    }
    private bool GroundCheck()
    {
        Collider2D groundCheck = Physics2D.OverlapCircle(_feetPos.position, _checkRadius, _GroundLayer);

        if (groundCheck != null)
        {
            return true;
        }

        return false;
    }
    private void FallMultiplier(bool fromJump)
    {
        if (!GroundCheck() && rb.velocity.y < _peakjump)
        {
            if (fromJump)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * _fallMultiplier * Time.fixedDeltaTime;
            }
        }
        if (rb.velocity.y == 0f)
        {
            _isJumping = false;
        }
    }
}
