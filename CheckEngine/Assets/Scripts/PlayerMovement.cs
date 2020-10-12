using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed;
    private float _direction;
    private bool _playerDirection;

    private Rigidbody2D _rigidBody;

    private void Awake(){
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update(){
        //Flips player direction depending on what arrow key is pressed.
        //If right is pressed, then the player will face right, and vice versa.
        float _dirSign = transform.localScale.x;
        if ((_direction > 0 && _dirSign < 0) || (_direction < 0 && _dirSign > 0)){
            transform.localScale *= new Vector2(-1, 1);
        }
    }

    private void FixedUpdate(){
        //Applies fixed velocity movement to player
        //_moveSpeed can be defined in Unity Editor
        _rigidBody.velocity = new Vector2(_direction * _moveSpeed, _rigidBody.velocity.y);

    }

    public void Move(float _input){
        //Passes directional input from other scripts to this script.
        _direction = _input;
    }
}
