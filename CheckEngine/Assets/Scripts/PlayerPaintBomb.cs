using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPaintBomb : MonoBehaviour
{
    private BounceBomb _bounceBomb;

    private bool _atkKey;
    private bool _prevAtkKey;

    private Rigidbody2D _rigidBody;
    private Collider2D _collider;

    private void Awake(){
        _rigidBody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();

        _bounceBomb = GetComponent<BounceBomb>();

        // animator = GetComponent<Animator>();
    }

    private void FixedUpdate(){
    }

    private void Update(){
        if ((_atkKey == true && _prevAtkKey == false)){
            _bounceBomb.BombThrow();
        }
    }

    public void ThrowBomb(bool _specialKeyState){
        _atkKey = _specialKeyState;
    }
}