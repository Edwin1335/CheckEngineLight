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

    //Cache currently used scripts to access as components
    private void Awake(){
        _movement = GetComponent<PlayerMovement>();
        _jumping = GetComponent<PlayerJump>();
        _dash = GetComponent<PlayerDash>();
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

        //Jumping
        //Only true when jump key is pressed, which is then sent to jump script
        _jumping.Jump(_jumpKeyState, false, _dirInput);
        
        //Movement
        //Sends single float value -1, 0, or 1 to movement script
        _movement.Move(_dirInput);

        //Dash
        //Only triggers when LeftShift is pressed, which then sends current directional input to dash script
        if (Input.GetKeyDown(KeyCode.LeftShift)){
            _dash.Dash(_dirInput);
        }

    }
}
