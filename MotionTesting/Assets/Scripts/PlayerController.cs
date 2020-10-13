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
    private PlayerAttack _attack;

    private float _dirInput;
    private bool _jumpKeyState;
    private bool _dashKeyState;
    private bool _atkKeyState;

    //Cache currently used scripts to access as components
    private void Awake(){
        _movement = GetComponent<PlayerMovement>();
        _jumping = GetComponent<PlayerJump>();
        _dash = GetComponent<PlayerDash>();
        _attack = GetComponent<PlayerAttack>();
    }

    private void Start(){
        //Sets maximum framerate (This fixed the janky looking jumping somehow)
        Application.targetFrameRate = 300;
    }

    private void Update()
    {
        //Collect keyboard inputs at the start of each update loop
        _dirInput = Input.GetAxisRaw("Horizontal"); //Left/Right
        _jumpKeyState = Input.GetKey(KeyCode.Z); //Jump
        _dashKeyState = Input.GetKey(KeyCode.LeftShift); //Dash
        _atkKeyState = Input.GetKey(KeyCode.X); //Attack

        //Jumping
        //Only true when jump key is pressed, which is then sent to PlayerJump script
        _jumping.Jump(_jumpKeyState, false, _dirInput);
        
        //Movement
        //Sends single float value -1, 0, or 1 to PlayerMovement script
        _movement.Move(_dirInput);

        //Dash
        //Only triggers when LeftShift is pressed, which then sends current directional input to PlayerDash script
        _dash.Dash(_dashKeyState, _dirInput, _atkKeyState);

        //Attack
        _attack.Attack(_atkKeyState, _dashKeyState);

    }
}
