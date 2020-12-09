using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //Each variable immediately below a [SerializeField] is editable in the Unity Inspector
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
            //---------------------------------------------------------------------------
            //Attacking Begins Here
            //---------------------------------------------------------------------------
            float[] attackDetails = new float[2];
            attackDetails[0] = _finalDmg;
            attackDetails[1] = this.GetComponent<Transform>().transform.position.x;
            Collider2D[] _enemiesToDmg = Physics2D.OverlapCircleAll(_atkPos.position, _atkRange, _isEnemy);

            //Deals damage to all enemies within attack range
            for (int i = 0; i < _enemiesToDmg.Length; i++){
                Debug.Log("Attacking");
                //Damage reduction is performed enemy side
                _enemiesToDmg[i].SendMessageUpwards("Damage", attackDetails);
            }

            //Debug.Log(_isDashing);
            //Debug.Log("Damage Output: " + _finalDmg);
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
