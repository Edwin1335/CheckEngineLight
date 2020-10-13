using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJump : MonoBehaviour
{
    [SerializeField]
    private LayerMask _groundLayer;
    [SerializeField]
    private Transform _front;
    [SerializeField]
    private float checkRadius;

    private bool _touchingFront;
    private bool _isSliding;
    private bool _isJumping;

    [SerializeField]
    private float _wallSlidingSpeed;
    private float _dirInput;
    private bool _jumpKey;

    private Rigidbody2D _rigidBody;
    //private Collider2D _collider;
    private PlayerJump _jumping;

    private void Awake(){
        _rigidBody = GetComponent<Rigidbody2D>();
        //_collider = GetComponent<Collider2D>();
        _jumping = GetComponent<PlayerJump>();
    }

    private void Update()
    {
        //Throws hitbox detection in "front" of player instead of downward to allow for wall sliding.
        _touchingFront = Physics2D.OverlapCircle(_front.position, checkRadius, _groundLayer);

        //If player's "front" is against a wall, slow down fall velocity.
        //Fall velocity can be changed in Unity Inspector with _wallSlidingSpeed parameter
        if (_touchingFront == true && _dirInput != 0){
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, Mathf.Clamp(_rigidBody.velocity.y, -_wallSlidingSpeed, float.MaxValue));
            _isSliding = true;
            
        }
        else{
            _isSliding = false;
        }

        if (_isSliding == true && _jumpKey == true){
            _jumping.Jump(_jumpKey, _isSliding, _dirInput);
        }
    }

    public void SlideJump(bool _jumpKeyState, float _input){
        //Passes jump key state and direction from other scripts to this script.
        //Debug.Log("Slide Triggered");
        _jumpKey = _jumpKeyState;
        _dirInput = _input;
    }
}