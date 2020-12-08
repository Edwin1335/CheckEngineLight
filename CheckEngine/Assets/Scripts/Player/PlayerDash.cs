using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    [SerializeField]
    private float _dashSpeed = 1500;
    private float _dashTime;
    [SerializeField]
    private float _startDashTime = 0.15f;
    
    private float _direction;
    private bool _dashKey;
    private bool _atkKey;
    private bool _isDashing;

    private bool _currDashing;
    private int _hitCnt;

    private PlayerAttack _atk;
    
    private void Awake(){
        _rigidBody = GetComponent<Rigidbody2D>();
        _atk = GetComponent<PlayerAttack>();

        //Player dashes for a certain time (_startDashTime), given in Unity inspector
        _dashTime = _startDashTime;
    }

    private void FixedUpdate(){

        //When current dash time goes below zero, indicating that the timeer has en1d.
        //Resets player movement direction and velocity to zero and sets current dash time back to full dash time length.
        if (_currDashing == true && _dashTime > 0){
            //Performs dash when a direction is being pressed
            if (_direction != 0){
                //Begins counting down until current dash time reaches zero
                _dashTime -= Time.deltaTime;
                //Applies high force to player to greatly increase their movement speed for dash duration
            //---------------------------------------------------------------------------
                //Dashing Occurs Here
                _rigidBody.AddForce(new Vector2(_direction * _dashSpeed, _rigidBody.velocity.y));
            //---------------------------------------------------------------------------
                _isDashing = true;
                //_atkKey = Input.GetKey(KeyCode.X);
            }
        }
        else if (_dashTime <= 0){
            _direction = 0;
            _isDashing = false;
            //Debug.Log(_currDashing + ", " + _dashTime);
        }
        if (_currDashing == false && _dashTime <= 0){
            _dashTime = _startDashTime;
            _isDashing = false;
            //Debug.Log("Dash Time Reset");
        }
        _atk.Attack(_atkKey, _isDashing);
    }

    public void Dash(bool _dashState, float _input, bool _atkKeyState){
        //Passes directional input from PlayerController script
        _currDashing = _dashState;
        _direction = _input;
        _atkKey = _atkKeyState;
        //Debug.Log("Currently Dashing: " + _currDashing);
    }
}
