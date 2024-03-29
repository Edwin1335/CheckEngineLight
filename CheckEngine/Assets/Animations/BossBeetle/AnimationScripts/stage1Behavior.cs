﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage1Behavior : StateMachineBehaviour
{
    // [SerializeField] private float _speed;
    private int _random;
    // private GameObject _player;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _random = Random.Range(0, 2);

        if(_random == 0){
            animator.SetTrigger("landing");
            animator.SetBool("isLanded", true);
            animator.SetBool("isFlying", false);
        }
        else {
            animator.SetTrigger("flying");
            animator.SetBool("isFlying", true);
            animator.SetBool("isLanded", false);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // _random = Random.Range(0, 5);

        // if (_random < 2){
        //     animator.SetTrigger("fastCharge");
        //     animator.transform.position = Vector3.MoveTowards(animator.transform.position, _player.transform.position, _speed * Time.deltaTime);
        // }
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
