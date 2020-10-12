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
        if (Input.GetKeyDown(KeyCode.LeftShift)){
            _dash.Dash(_dirInput);
        }

    }
}
