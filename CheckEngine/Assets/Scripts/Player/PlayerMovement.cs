using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 15;
    private float _direction;
    private bool _playerDirection;

    private Rigidbody2D _rigidBody;
    private Animator animator;
    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float _dirSign = transform.localScale.x;
        if ((_direction > 0 && _dirSign < 0) || (_direction < 0 && _dirSign > 0))
        {
            transform.localScale *= new Vector2(-1, 1);
        }
    }

    private void FixedUpdate()
    {
        //---------------------------------------------------------------------------
            //Movement Occurs Here
        _rigidBody.velocity = new Vector2(_direction * _moveSpeed, _rigidBody.velocity.y);
        //---------------------------------------------------------------------------
    }

    public void Move(float _input)
    {
        _direction = _input;
        if (_input != 0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }
}