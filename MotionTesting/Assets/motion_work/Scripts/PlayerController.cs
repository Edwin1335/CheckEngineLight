using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public RigidbodyInterpolation Interpolation;

    private PlayerMovement _movement;
    private PlayerJump _jumping;
    private PlayerDash _dash;
    //private PlayerWallJump _wallJump;

    private float _dirInput;
    private bool _jumpKeyState;

    private void Awake(){
        _movement = GetComponent<PlayerMovement>();
        _jumping = GetComponent<PlayerJump>();
        _dash = GetComponent<PlayerDash>();
        //_wallJump = GetComponent<PlayerWallJump>();
    }

    private void Start(){
        Application.targetFrameRate = 300;
    }

    private void Update()
    {
        _dirInput = Input.GetAxisRaw("Horizontal");
        _jumpKeyState = Input.GetKey(KeyCode.Z);

        //Jumping
        _jumping.Jump(_jumpKeyState, false, _dirInput);
        
        //Movement
        _movement.Move(_dirInput);

        //Dash
        //float _horiz = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.LeftShift)){
            //Debug.Log(_horiz + ", Shift");
            _dash.Dash(_dirInput);
        }

        //WallJump
        //float _sliding = Input.GetAxisRaw("Horizontal");
        //_wallJump.SlideJump(_dirInput, _jumpKeyState);

    }
}
