using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyingBehavior : StateMachineBehaviour
{
    [SerializeField] private float timer;
    [SerializeField] private float minTime;
    [SerializeField] private float maxTime;

    private Transform _playerPos;
    [SerializeField] private float _speed;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerPos = GameObject.Find("Gloomy").GetComponent<Transform>();
        timer = Random.Range(minTime, maxTime);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if (timer <= 0){
           animator.SetTrigger("landing");
       }
       else {
           timer -= Time.deltaTime;
       }

       Vector2 _target = new Vector2 (_playerPos.position.x, animator.transform.position.y);
       animator.transform.position = Vector2.MoveTowards(animator.transform.position, _target, _speed);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
