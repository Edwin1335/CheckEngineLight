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
        _dashTime = _startDashTime;
    }

    private void FixedUpdate(){
        if (_dashTime <= 0){
            _direction = 0;
            _dashTime = _startDashTime;
            _rigidBody.velocity = Vector2.zero;
        }
        else{
            if (_direction != 0){
                _dashTime -= Time.deltaTime;
                //_rigidBody.velocity = new Vector2(_direction * _dashSpeed, _rigidBody.velocity.y);
                _rigidBody.AddForce(new Vector2(_direction * _dashSpeed, _rigidBody.velocity.y));
                //Debug.Log("Decreasing: " + _dashTime + ", Direction: " + _direction + ", Velocity: " + _rigidBody.velocity);
            }
        }
    }

    public void Dash(float _input){
        _direction = _input;
    }
}
