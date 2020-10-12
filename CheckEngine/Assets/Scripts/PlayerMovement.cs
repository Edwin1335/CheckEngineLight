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

    // Wil be used to animate the player
    private Animator animator;


    private void Awake()
    {
        // Get componenets when scene awakes
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
        _rigidBody.velocity = new Vector2(_direction * _moveSpeed, _rigidBody.velocity.y);
        if (_direction != 0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    public void Move(float _input)
    {
        _direction = _input;
    }
}
