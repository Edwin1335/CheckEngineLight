using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    [SerializeField]
    private float _health;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_health <= 0){
            Destroy(gameObject, 0.0f);
        }
    }

    public void EnemyDamage(float _damage){
        _health -= _damage;
        Debug.Log("Damage Taken");
    }
}
