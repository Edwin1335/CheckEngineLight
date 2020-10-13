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
    Animator _animator;

    private void Awake(){
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = gameObject.GetComponent<Animator>();
    }

    private void Update(){
        float _dirSign = transform.localScale.x;
        if ((_direction > 0 && _dirSign < 0) || (_direction < 0 && _dirSign > 0)){
            transform.localScale *= new Vector2(-1, 1);
        }
    }

    private void FixedUpdate(){
        _rigidBody.velocity = new Vector2(_direction * _moveSpeed, _rigidBody.velocity.y);

    }

    public void Move(float _input){
        _direction = _input;
    }
}