using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    [SerializeField]
    private float _health;
    public float vertKnockback = 100;
    public float horKnockback = 100;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_health <= 0)
        {
            Destroy(gameObject, 0.0f);
        }
    }

    public void EnemyDamage(float _damage)
    {
        _health -= _damage;
        Debug.Log("Damage Taken");
    }

    public void knockBack(Transform tran)
    {
        if (tran.position.x < this.transform.position.x)
        {
            this.GetComponent<Rigidbody2D>().AddForce(transform.up * vertKnockback + transform.right * horKnockback);

        }
        else
        {
            this.GetComponent<Rigidbody2D>().AddForce(transform.up * vertKnockback + (transform.right * horKnockback) * -1);
        }
    }
}
