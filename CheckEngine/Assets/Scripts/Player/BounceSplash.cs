using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceSplash : MonoBehaviour
{
    [SerializeField] float _jumpForceMultiplier = 1.001f;
    private int _groundLayer;

    private float _xVelocity;
    private float _yVelocity;
    private float _rotation;
    private float _jumpTrue = 0.5f;

    private bool _splashRun;
    private bool _prevSplashRun;

    private Rigidbody2D _rigidBody;
    // private Collider2D _collider;
    private BoxCollider2D _collider;

    private void Awake(){
        _rigidBody = GetComponent<Rigidbody2D>();
        // _collider = GetComponent<Collider2D>();
        _collider = GetComponent<BoxCollider2D>();

        _groundLayer =~ LayerMask.GetMask("Ground");
        _splashRun = true;
    }

    private void FixedUpdate(){
        if (_splashRun == true && _prevSplashRun == false && Physics2D.IsTouchingLayers(_collider, _groundLayer)){
            _rotation = transform.rotation.eulerAngles.z;
            Collider2D[] _objectsToBounce = Physics2D.OverlapBoxAll(gameObject.transform.position, _collider.size / 2, _rotation, _groundLayer);

            for (int i = 0; i < _objectsToBounce.Length; i++){
                Rigidbody2D _objectRB = _objectsToBounce[i].attachedRigidbody;
                // _xVelocity = _objectRB.velocity.x;
                _yVelocity = _objectRB.velocity.y;

                if (_rotation == 0){
                    // Debug.Log("Up Splash");
                    if (_yVelocity > 0){
                        _objectRB.AddForce(new Vector2(0, _jumpTrue * _jumpForceMultiplier * _yVelocity), ForceMode2D.Impulse);
                    }
                    else if (_yVelocity < 0){
                        _objectRB.velocity = new Vector2(0, 0);
                        _objectRB.AddForce(new Vector2(0, _jumpForceMultiplier * Mathf.Abs(_yVelocity)), ForceMode2D.Impulse);
                    }
                }
                if (_rotation == -180){
                    Debug.Log("Down Splash");
                    _objectRB.velocity = new Vector2(0, 0);
                    _objectRB.AddForce(new Vector2(0, -1 * _jumpForceMultiplier * _yVelocity), ForceMode2D.Impulse);
                }
                // if (_rotation == 90 || _rotation == -90){
                //     Debug.Log("Sideways Splash");
                //     Debug.Log(_objectRB.velocity.x);
                //     _objectRB.velocity = new Vector2(0, 0);
                //     _objectRB.AddForce(new Vector2(1000 * _jumpForceMultiplier * _xVelocity, 0), ForceMode2D.Impulse);
                // }
            }
        }
        _prevSplashRun = _splashRun;

        if (Physics2D.IsTouchingLayers(_collider, _groundLayer)){
            _splashRun = true;
        }
        else if (!Physics2D.IsTouchingLayers(_collider, _groundLayer)){
            _splashRun = false;
        }

    }
}