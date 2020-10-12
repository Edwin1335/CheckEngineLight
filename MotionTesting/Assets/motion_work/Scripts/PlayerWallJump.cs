using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJump : MonoBehaviour
{
    [SerializeField]
    private LayerMask _groundLayer;
    [SerializeField]
    private float _raycastFront;

    private bool _touchingFront;
    private bool _isSliding;
    private bool _isJumping;

    [SerializeField]
    private float _wallSlidingSpeed;
    private float _wallInput;
    private float _dirInput;
    private bool _jumpInput;

    private Rigidbody2D _rigidBody;
    private Collider2D _collider;
    private PlayerJump _jumping;

    private void Awake(){
        _rigidBody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _jumping = GetComponent<PlayerJump>();
    }

    private void Update()
    {
        Vector2 _raycastDirection = new Vector2(_dirInput, 0);
        RaycastHit2D _front = Physics2D.Raycast(transform.position, _raycastDirection, _raycastFront, _groundLayer);
        if (_front.collider != null){
            _touchingFront = true;
        }
        else{
            _touchingFront = false;
        }

        if (_touchingFront == true && _dirInput != 0){
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, Mathf.Clamp(_rigidBody.velocity.y, -_wallSlidingSpeed, float.MaxValue));
            _isSliding = true;
        }
        else{
            _isSliding = false;
        }

        _jumping.Jump(_jumpInput, _isSliding, _dirInput);
        /*
        if (_jumpInput == true && _isSliding == true){
            _isJumping = true;
            //Debug.Log(_isJumping);
            _jumping.Jump(_jumpInput, _isJumping, _dirInput);
        }
        //Debug.Log(_isSliding + ", " + _touchingFront + ", " + _raycastDirection);
        */
    }

    public void SlideJump(bool _jumpKeyState, float _input){
        Debug.Log("Slide Triggered");
        _jumpInput = _jumpKeyState;
        _dirInput = _input;
        //Debug.Log(_jumpKeyState);
    }
}
