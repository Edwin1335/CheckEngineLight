using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyScript : MonoBehaviour
{
    public float _health;
    private float _finalHealth;
    [SerializeField]
    private float _speed;
    [SerializeField]

    public GameObject _enemy;
    public Transform _enemySpawn;

    private void Start(){
        _finalHealth = _health;
    }

    private void Update()
    {
        if (_finalHealth <= 0){
            Destroy(_enemy);
            GameObject _clone = Instantiate(_enemy, _enemySpawn.transform.position, Quaternion.identity);
            _clone.name = _clone.name.Replace("(Clone)", "");
            _clone.GetComponent<Collider2D>().enabled = true;
            _clone.GetComponent<TestEnemyScript>().enabled = true;
            _finalHealth = _health;
        }
        gameObject.transform.Translate(Vector2.left * _speed * Time.deltaTime);
    }

    public void TakeDamage(float _dmg){
        _finalHealth -= _dmg;
        //Debug.Log("Damage Taken: " + _dmg);
    }

}
