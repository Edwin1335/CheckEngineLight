using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBomb : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private Collider2D _collider;

    private void Awake(){
        _rigidBody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();

        // animator = GetComponent<Animator>();
    }

    private void FixedUpdate(){
    }

    private void Update(){

    }

    public void BombThrow(){
    }
}