using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleBoss : MonoBehaviour
{
    [SerializeField] private float timer;
    [SerializeField] private float standingMinTime = 3;
    [SerializeField] private float standingMaxTime = 6;
    [SerializeField] private float flyingMinTime = 5;
    [SerializeField] private float flyingMaxTime = 8;

    [SerializeField] private float sleepTime = 3;
    [SerializeField] private float minDistance = 3;
    [SerializeField] private float maxDistance = 0;
    [SerializeField] private float maxOutDistance = 25;
    [SerializeField] private float speed = 7;

    [SerializeField] private GameObject _player;
    private LayerMask _ground;


    private Rigidbody2D _rigidBody;
    private Collider2D _collider;
    private Animator animator;

    private void Awake(){
        _player = GameObject.Find("Gloomy");

        _rigidBody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();

        animator.SetBool("isLanded", true);
        animator.SetBool("isFlying", false);
    }

    private void  FixedUpdate()
    {


        // Phase Two (Half Health)


        // Charge Attack


        // Lighting Strike Telegraph and Attack
    }

    private void Update(){
        // Landed -> Flying
        // if (timer <= 0 && animator.GetBool("isLanded") == true){
        //     animator.SetTrigger("flying");
        //     animator.SetBool("isFlying", true);
        //     animator.SetBool("isLanded", false);
        //     timer = Random.Range(flyingMinTime, flyingMaxTime);
        // }
        // else {
        //     timer -= Time.deltaTime;
        // }


        if (animator.GetBool("isLanded")){
            StartCoroutine(sleepTimer(sleepTime));
            animator.SetTrigger("flying");
            animator.SetBool("isFlying", true);
            animator.SetBool("isLanded", false);
        }


        // Flying -> Landing
        // if (timer <= 0 && animator.GetBool("isFlying") == true){
        //     animator.SetTrigger("landing");
        //     animator.SetBool("isLanded", true);
        //     animator.SetBool("isFlying", false);
        //     timer = Random.Range(standingMinTime, standingMaxTime);
        // }
        // else {
        //     timer -= Time.deltaTime;
        // }

        if (animator.GetBool("isFlying")){

            animator.SetTrigger("flying");

            if (this.transform.position.x < _player.transform.position.x){
                this.transform.localScale = new Vector2(1, 1);
            }
            else if (this.transform.position.x > _player.transform.position.x){
                this.transform.localScale = new Vector2(-1, 1);
            }

            if (Vector2.Distance(transform.position, _player.transform.position) >= minDistance && Vector2.Distance(transform.position, _player.transform.position) <= maxOutDistance){
                Vector2 follow = _player.transform.position;

                // Maintains Y Position
                // follow.y = this.transform.position.y;
                this.transform.position = Vector2.MoveTowards((this.transform.position), follow, speed * Time.deltaTime);
            }
        }

        // Landed Idle State
        if (animator.GetBool("isLanded") == true){

        }
        
        // Flying Idle State
        if (animator.GetBool("isFlying") == true){

        }
    }

    private void toFloor(){
        // RaycastHit groundHit;

        // if (groundHit.transform.gameObject.layer == LayerMask.NameToLayer("Ground")){

        // }
    }

    IEnumerator sleepTimer(float sleepTime){
        yield return new WaitForSeconds(sleepTime);
    }
}
