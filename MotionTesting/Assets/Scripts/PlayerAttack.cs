using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    private float _timeBtwAtk;
    [SerializeField]
    private float _startTimeBtwAtk;

    public Transform _atkPos;
    [SerializeField]
    private float _atkRange;
    [SerializeField]
    private LayerMask _isEnemy;

    public float _dmg;
    [SerializeField]
    private float _dashDmg; //Dash damage multiplier
    private float _finalDmg;
    private bool _currAtkState;
    private bool _prevAtkState; //Used to prevent holding attack to hit repeatedly
    private bool _isDashing;

    private float _dashTime;

    private PlayerDash _dash;

    private void Awake(){
        _dash = GetComponent<PlayerDash>();
    }

    private void FixedUpdate()
    { 

        if (_isDashing == true){
            _finalDmg = _dmg * _dashDmg;
        }
        else{
            _finalDmg = _dmg;
        }

        if ((_currAtkState == true && _prevAtkState == false) || (_currAtkState == true && _isDashing == true)){
            //Checks to see if enemy is within attack range
            Collider2D[] _enemiesToDmg = Physics2D.OverlapCircleAll(_atkPos.position, _atkRange, _isEnemy);
            //Deals damage to all enemies within attack range
            for (int i = 0; i < _enemiesToDmg.Length; i++){
                //Damage reduction is performed enemy side
                _enemiesToDmg[i].GetComponent<TestEnemyScript>().TakeDamage(_finalDmg);
            }
            //Debug.Log(_isDashing);
            //Debug.Log(_finalDmg);
            Debug.Log(_finalDmg);
        }
        _prevAtkState = _currAtkState;
    }

    public void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_atkPos.position, _atkRange);
    }

    public void Attack(bool _atkKeyState, bool _dashState){
        _currAtkState = _atkKeyState;
        _isDashing = _dashState;
        //Debug.Log("AtkKey: " + _currAtkState + ", isDashing: " + _isDashing);
    }
}
