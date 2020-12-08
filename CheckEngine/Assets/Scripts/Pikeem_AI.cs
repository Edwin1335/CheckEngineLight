using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pikeem_AI : MonoBehaviour
{
    public float pikeemMovespeed;
    Rigidbody2D pkrigbody;
    public float circleRadius;
    public GameObject groundcheck;
    public LayerMask groundLayer;
    public bool facingRight;
    public bool isGround;

    private TakeDamage damage;
    
    void Start()
    {
        pkrigbody = GetComponent<Rigidbody2D>();
        damage = GetComponent<TakeDamage>();
        damage.enemyName = "Pikeem";
    }

    void Update()
    {
        pkrigbody.velocity = Vector2.right * pikeemMovespeed * Time.deltaTime;
        isGround = Physics2D.OverlapCircle(groundcheck.transform.position, circleRadius, groundLayer);
        if(!isGround && facingRight)
        {
            Flip();
        }
        else if(!isGround && !facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(new Vector3(0, -180, 0));
        pikeemMovespeed = -pikeemMovespeed;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundcheck.transform.position, circleRadius);
    }

}
