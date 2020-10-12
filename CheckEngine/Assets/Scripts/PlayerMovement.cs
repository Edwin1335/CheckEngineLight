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

<<<<<<< HEAD
    private void Update()
    {
=======
    private void Update(){
        //Flips player direction depending on what arrow key is pressed.
        //If right is pressed, then the player will face right, and vice versa.
>>>>>>> 22e13e5403a02145cee41498d0d81d85b66bb508
        float _dirSign = transform.localScale.x;
        if ((_direction > 0 && _dirSign < 0) || (_direction < 0 && _dirSign > 0))
        {
            transform.localScale *= new Vector2(-1, 1);
        }
    }

<<<<<<< HEAD
    private void FixedUpdate()
    {
=======
    private void FixedUpdate(){
        //Applies fixed velocity movement to player
        //_moveSpeed can be defined in Unity Editor
>>>>>>> 22e13e5403a02145cee41498d0d81d85b66bb508
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

<<<<<<< HEAD
    public void Move(float _input)
    {
=======
    public void Move(float _input){
        //Passes directional input from other scripts to this script.
>>>>>>> 22e13e5403a02145cee41498d0d81d85b66bb508
        _direction = _input;
    }
}
