using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleBoss : MonoBehaviour
{
    //Modifiable Variables
    [SerializeField] private float _speed = 0.5f;
    [SerializeField] private float _lineOfSight = 10;
    [SerializeField] private float _attackRange = 5;

    //Hidden Variables
    private bool _playerFound = false;
    private bool _flyTowardsPlayer = false;
    private bool _attacked = false;

    //Get Components
    private Animator animator;
    // private Transform _player;
    private Vector2 _originalPosition;
    private TakeDamage _damage;

    private GameObject _player;

    private Rigidbody2D _rigidBody;
    private Collider2D _collider;

    private void Awake(){
        // if (GameObject.FindGameObjectWithTag("Player")){
        //     _damage = GetComponent<TakeDamage>();
        //     _damage.enemyName = "Tabee";
        //     _player = GameObject.FindGameObjectWithTag("Player").transform;
        //     _playerFound = true;
        //     animator = GetComponent<Animator>();
        //     _originalPosition = new Vector2(this.transform.position.x, this.transform.position.y);
        // }
        _player = GameObject.Find("Gloomy");

        _rigidBody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    private void  FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _speed * Time.deltaTime);
        if (this.transform.position.x < _player.transform.position.x){
            this.transform.localScale = new Vector2(1, 1);
        }
        else if (this.transform.position.x > _player.transform.position.x){
            this.transform.localScale = new Vector2(-1, 1);
        }
        // if (_playerFound && !_attacked)
        // {
        //     float _distanceFromPlayer = Vector2.Distance(_player.position, transform.position);

        //     if (_distanceFromPlayer < _lineOfSight && _distanceFromPlayer > _attackRange)
        //     {
        //         _flyTowardsPlayer = true;
        //         animator.SetBool("isAttacking", false);
        //         transform.position = Vector2.MoveTowards(new Vector2(this.transform.position.x, 0), _player.position, _speed * Time.deltaTime);
        //     }
        //     else if (_distanceFromPlayer > _lineOfSight && _distanceFromPlayer > _attackRange)
        //     {
        //         // Move Boss back to where he was.
        //         _flyTowardsPlayer = false;
        //         transform.position = Vector2.MoveTowards(this.transform.position, _originalPosition, (_speed / 2) * Time.deltaTime);
        //     }
        //     else if (_distanceFromPlayer <= _attackRange)
        //     {
        //         // Attack Animation.
        //         animator.SetBool("isAttacking", true);

        //         // Must still move towards the player so that it could collide and the player can take damage on collision.
        //         transform.position = Vector2.MoveTowards(this.transform.position, _player.position, _speed * Time.deltaTime);
        //         //reduce player health
        //         //attack player once every x amount of secs
        //     }

        //     // Flip Boss to face player or previous position.
        //     if (_flyTowardsPlayer)
        //     {
        //         if (this.transform.position.x < _player.position.x)
        //         {
        //             this.transform.localScale = new Vector2(1, 1);
        //         }
        //         else if (this.transform.position.x > _player.position.x)
        //         {
        //             this.transform.localScale = new Vector2(-1, 1);
        //         }
        //     }
        //     else
        //     {
        //         if (this.transform.position.x < _originalPosition.x)
        //         {
        //             this.transform.localScale = new Vector2(1, 1);
        //         }
        //         else if (this.transform.position.x > _originalPosition.x)
        //         {
        //             this.transform.localScale = new Vector2(-1, 1);
        //         }

        //     }
        // }
    }
}
