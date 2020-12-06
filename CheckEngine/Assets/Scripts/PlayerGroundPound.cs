using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundPound : MonoBehaviour
{
    private bool _atkKey;
    private float _yInput;
    private bool _prevAtkKey;

    [SerializeField]
    private int _poundForce;

    private Rigidbody2D _rigidBody;
    private Collider2D _collider;

    private void Awake(){
        _rigidBody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();

        // animator = GetComponent<Animator>();
    }

    private void FixedUpdate(){
        if ((_atkKey == true && _prevAtkKey == false) && _yInput < 0){
            _rigidBody.velocity = new Vector2(0, 0);
            _rigidBody.AddForce(new Vector2(0, (-1*_poundForce)));
        }
        _prevAtkKey = _atkKey;
    }

    public void GroundPound(bool _atkKeyState, float _dirInputY){

        //Passes Jump key state and direction
        _atkKey = _atkKeyState;
        _yInput = _dirInputY;
    }
}