using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    [SerializeField]
    private float _dashSpeed;
    private float _dashTime;
    [SerializeField]
    private float _startDashTime;
    private float _direction;

    private void Awake(){
        _rigidBody = GetComponent<Rigidbody2D>();

        //Player dashes for a certain time (_startDashTime), given in Unity inspector
        _dashTime = _startDashTime;
    }

    private void FixedUpdate(){

        //When current dash time goes below zero, indicating that the timeer has ended.
        //Resets player movement direction and velocity to zero and sets current dash time back to full dash time length.
        if (_dashTime <= 0){
            _direction = 0;
            _dashTime = _startDashTime;
            _rigidBody.velocity = Vector2.zero;
        }
        else{
            //Performs dash when a direction is being pressed
            if (_direction != 0){
                //Begins counting down until current dash time reaches zero
                _dashTime -= Time.deltaTime;
                //Applies high force to player to greatly increase their movement speed for dash duration
                _rigidBody.AddForce(new Vector2(_direction * _dashSpeed, _rigidBody.velocity.y));
            }
        }
    }

    public void Dash(float _input){
        //Passes directional input from PlayerController script
        _direction = _input;
    }
}
