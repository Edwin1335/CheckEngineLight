using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField]
    private LayerMask _groundLayer;
    private bool _grounded;

    private float _jumpForce;
    private bool _jumpKeyHeld;
    private bool _isJumping;
    [SerializeField]
    private Vector2 _counterJumpForce;
    [SerializeField]
    private float _jumpStrength;

    [SerializeField]
    private float _raycastFeet;
    private bool _wallSlide;
    private float _dirInput;
    private bool slideLooped;

    private Rigidbody2D _rigidBody;
    private Collider2D _collider;
    private PlayerWallJump _slideJump;

    // Wakes up required external scripts
    private void Awake(){
        _rigidBody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _slideJump = GetComponent<PlayerWallJump>();

        _jumpForce = calculateJumpForce(Physics2D.gravity.magnitude, _jumpStrength);
    }

    private void FixedUpdate(){
        if (_jumpKeyHeld == true){
            _slideJump.SlideJump(_jumpKeyHeld, _dirInput);

            if (_grounded == true && _wallSlide != true && _isJumping == false){
                _isJumping = true;
                _rigidBody.AddForce(Vector2.up * _jumpForce * _rigidBody.mass, ForceMode2D.Impulse);
            }
            else if (_grounded != true && _wallSlide == true && _isJumping == false){
                //Debug.Log("Walljump True");
                Vector2 _jumpVector = new Vector2(-_dirInput, 1);
                _isJumping = true;
                _rigidBody.AddForce(_jumpVector * _jumpForce * _rigidBody.mass, ForceMode2D.Impulse);
            }
        }

        RaycastHit2D _feet = Physics2D.Raycast(transform.position, Vector2.down, _raycastFeet, _groundLayer);
        if (_feet.collider != null && _wallSlide == false){
            _grounded = true;
            _isJumping = false;
        }
        else if (_feet.collider == null && _wallSlide != false){
            _grounded = false;
            _isJumping = false;
        }
        else{
            _grounded = false;
            _isJumping = true;
        }
        slideLooped = false;
    }

    private static float calculateJumpForce(float gravityStrength, float jumpHeight){
        return Mathf.Sqrt(2 * gravityStrength * jumpHeight);
    }

    public void Jump(bool _jumpKeyState, bool _isWallSlide, float _input){
        _jumpKeyHeld = _jumpKeyState;
        _wallSlide = _isWallSlide;
        _dirInput = _input;
    }
}
