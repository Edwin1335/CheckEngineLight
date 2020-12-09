using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TakeDamage))]
public class Rocky_AI : MonoBehaviour
{
    public float knockback = 10;
    public float knockbackLength = 0.2f;
    public float knockbackCount;
    public bool knockfromRight;

    private TakeDamage damage;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        damage = GetComponent<TakeDamage>();
        rb = GetComponent<Rigidbody2D>();
        damage.enemyName = "Rocky";
    }

    // Update is called once per frame
    void Update()
    {
        if(knockbackCount <= 0)
        {
            
        }
        else
        {

        }
    }

    public void KnockBack()
    {

    }
}
