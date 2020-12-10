using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPaintBomb : MonoBehaviour
{
    private bool _atkKey;
    private bool _prevAtkKey;
    private float _yInput;

    [SerializeField] private float _bombSpawnOffset;
    [SerializeField] private GameObject pfBounceBomb;
    private GameObject pfCloneBomb;
    private GameObject pfCloneSplash;

    private Rigidbody2D _rigidBody;
    private Collider2D _collider;

    private BounceBomb _bounceBomb;
    private Animator animator;

    private void Awake(){
        _rigidBody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();

        _bounceBomb = GetComponent<BounceBomb>();
        animator = GetComponent<Animator>();

        // animator = GetComponent<Animator>();
    }

    private void Update(){
        if (_atkKey == true && _prevAtkKey == false && !GameObject.Find("PaintBomb(Clone)")){
            Destroy(pfCloneSplash);
            Destroy(pfCloneBomb);
            pfCloneBomb = Instantiate(pfBounceBomb, new Vector2(transform.position.x - _bombSpawnOffset, transform.position.y), transform.rotation);
            animator.SetBool("isThrowBomb", true);
        }
        _prevAtkKey = _atkKey;
    }

    public void ThrowBomb(bool _specialKeyState, float _dirInputY){
        _atkKey = _specialKeyState;
        _yInput = _dirInputY;
    }
}