using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(PlayerMovement), typeof(PlayerJump), typeof(PlayerDash))]
[RequireComponent (typeof(PlayerWallJump), typeof(PlayerAttack), typeof(PlayerGroundPound))]
[RequireComponent (typeof(PlayerPaintBomb))]

public class PlayerController : MonoBehaviour
{
    private PlayerMovement _movement;
    private PlayerJump _jumping;
    private PlayerDash _dash;
    private PlayerWallJump _slideJump;
    private PlayerAttack _attack;
    private PlayerGroundPound _groundPound;
    private PlayerPaintBomb _paintBomb;

    private float _dirInputX;
    private float _dirInputY;
    private bool _jumpKeyState;
    private bool _dashKeyState;
    private bool _atkKeyState;
    private bool _specialKeyState;

    //Cache currently used scripts to access as components
    private void Awake(){
        _movement = GetComponent<PlayerMovement>();
        _jumping = GetComponent<PlayerJump>();
        _slideJump = GetComponent<PlayerWallJump>();
        _dash = GetComponent<PlayerDash>();
        _attack = GetComponent<PlayerAttack>();
        _groundPound = GetComponent<PlayerGroundPound>();
        _paintBomb = GetComponent<PlayerPaintBomb>();
    }

    private void Start(){
        //Sets maximum framerate (This fixed the janky looking jumping somehow)
        Application.targetFrameRate = 300;
    }

    private void Update()
    {
        //Collect keyboard inputs at the start of each update loop
        _dirInputX = Input.GetAxisRaw("Horizontal"); //Left/Right
        _dirInputY = Input.GetAxisRaw("Vertical"); //Up/Down
        _jumpKeyState = (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.Space)); //Jump
        _dashKeyState = Input.GetKey(KeyCode.LeftShift); //Dash
        _atkKeyState = Input.GetKey(KeyCode.X); //Attack
        _specialKeyState = Input.GetKey(KeyCode.C); //Special (Bounce Bomb)

        //Jumping
        //Only true when jump key is pressed, which is then sent to PlayerJump script
        _jumping.Jump(_jumpKeyState, false, _dirInputX);
        _slideJump.SlideJump(_jumpKeyState, _dirInputX);
        
        //Movement
        //Sends single float value -1, 0, or 1 to PlayerMovement script
        _movement.Move(_dirInputX);

        //Dash
        //Only triggers when LeftShift is pressed, which then sends current directional input to PlayerDash script
        _dash.Dash(_dashKeyState, _dirInputX, _atkKeyState);

        if (_dirInputY != -1){
            //Attack
            _attack.Attack(_atkKeyState, _dashKeyState);
        }

        //Ground Pound
        _groundPound.GroundPound(_atkKeyState, _dirInputY);

        //PaintBomb
        _paintBomb.ThrowBomb(_specialKeyState, _dirInputY);
    }
}