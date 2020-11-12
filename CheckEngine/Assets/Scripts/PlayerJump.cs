using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField]
    private LayerMask _groundLayer;
    private bool _grounded;
    [SerializeField]
    private Transform _feet;
    [SerializeField]
    private float checkRadius = 0.1f;

    private float _jumpForce;
    private bool _jumpKeyHeld;
    private bool _jumpKeyHeldPrev;
    private bool _isJumping;
    [SerializeField]
    private Vector2 _counterJumpForce = new Vector2(0, 5);
    [SerializeField]
    private float _jumpStrength = 50;
    [SerializeField]
    private int _jumpCount = 2;
    [SerializeField]
    private bool _extraJump = true;
    [SerializeField]
    private float _wallJumpForce;
    private bool _wallFlip;

    private bool _wallSlide;
    private float _dirInput;
    private bool slideLooped;
    private int _jumpsLeft;


    private Rigidbody2D _rigidBody;
    private Collider2D _collider;
    private PlayerWallJump _slideJump;
    private Animator animator;

    // Wakes up required external scripts
    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _slideJump = GetComponent<PlayerWallJump>();
        animator = GetComponent<Animator>();

        //Calculates and stores jump force heigh based on physics gravity and jump strength, which can be defined in Unity Inspector
        _jumpForce = calculateJumpForce(Physics2D.gravity.magnitude, _jumpStrength);

        _jumpsLeft = _jumpCount;
    }

    private void FixedUpdate()
    {

        if (_grounded == true && _extraJump == true)
        {
            //Debug.Log("Double Jump Reset: " + _jumpCount);
            _isJumping = false;
            _jumpsLeft = _jumpCount - 1;
            //Debug.Log("Button on jumps made: " + _jumpsLeft);
        }
        else if (_grounded == true && _extraJump == false)
        {
            //Debug.Log("No Double Jump Reset: " + _jumpCount);
            _isJumping = false;
            _jumpsLeft = 1;
            //Debug.Log("Button off jumps made: " + _jumpsLeft);
        }

        //Enters jump loop if jump key is pressed
        if (_jumpKeyHeld == true && _jumpsLeft != 0 && _jumpKeyHeldPrev == false)
        {

            //Passes jump button state and direction to PlayerWallJump script 
            //(Incomplete, but can slow vertical descent if player is on wall)
            //_slideJump.SlideJump(_jumpKeyHeld, _dirInput);

            if (_wallSlide == false)
            {
                _rigidBody.velocity = new Vector2(0, 0);
                //---------------------------------------------------------------------------
                //Jumping Occurs Here
                //---------------------------------------------------------------------------
                Debug.Log("Goes Here JUMP");
                animator.SetBool("isJumping", true);
                _rigidBody.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
                _jumpsLeft--;
                _isJumping = true;
            }

            //If the player is not on the ground but is sliding on a wall, they will jump away from the wall with an applied force
            //(Currently Incomplete)
            else if (_grounded != true && _wallSlide == true && _isJumping == false)
            {
                //Debug.Log("Walljump True");
                //Vector2 _jumpVector = new Vector2(-_dirInput, 0);
                _isJumping = true;
                //_rigidBody.velocity = new Vector2(_dirInput * -1, _rigidBody.velocity.y);
                _wallFlip = true;
                _rigidBody.velocity = new Vector2(_wallJumpForce * -_dirInput, _jumpForce);
                _dirInput = -_dirInput;
                //_rigidBody.AddForce(_jumpVector * _jumpForce * _wallJumpForce * _rigidBody.mass, ForceMode2D.Impulse);
                //Debug.Log(_jumpVector * _jumpForce * _wallJumpForce * _rigidBody.mass + ", " + _rigidBody.velocity);
                Debug.Log(_rigidBody.velocity);
            }
        }
        _jumpKeyHeldPrev = _jumpKeyHeld;

        //Performs hitbox detection, indicating if player is on the ground or sliding on a wall
        //Hitbox detection sensitivity can be changed in Unity Inspector with _raycastFeet
        //RaycastHit2D _feet = Physics2D.Raycast(transform.position, Vector2.down, _raycastFeet, _groundLayer);
        _grounded = Physics2D.OverlapCircle(_feet.position, checkRadius, _groundLayer);

        //Detects if player is on the ground and not sliding on a wall
        if (_grounded == true && _wallSlide == false)
        {
            _isJumping = false;
            animator.SetBool("isJumping", false);
        }
        //Detects if player is not on the ground and is sliding on a wall
        else if (_grounded == false && _wallSlide != false)
        {
            _isJumping = false;
        }
        //Default state is in the air, since the player is always on the ground otherwise.
        else
        {
            _grounded = false;
            _isJumping = true;
        }
        //slideLooped = false;

        if ((_grounded == true && _wallFlip == true) || (_wallSlide == true && _wallFlip == true))
        {
            //_rigidBody.velocity = new Vector2(_dirInput * -1, _rigidBody.velocity.y);
            _wallFlip = false;
        }
    }

    //Performs _jumpForce calculation
    private static float calculateJumpForce(float gravityStrength, float jumpHeight)
    {
        return Mathf.Sqrt(2 * gravityStrength * jumpHeight);
    }

    public void Jump(bool _jumpKeyState, bool _isWallSlide, float _input)
    {
        //Passes Jump Key state, wall sliding state, and directional input from other scripts to this script.
        _jumpKeyHeld = _jumpKeyState;
        _wallSlide = _isWallSlide;
        _dirInput = _input;
    }

    private void jumpAnimations()
    {
        if (!_grounded)
        {
            animator.SetBool("isJumping", true);

        }
        else
        {
            animator.SetBool("isJumping", false);
        }
    }
}