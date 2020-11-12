using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pikeem_AI : MonoBehaviour
{
    public float pikeemHealth = 2f;

    public float pikeemMovespeed;
    Rigidbody2D pkrigbody;
    public float circleRadius;
    public GameObject groundcheck;
    public LayerMask groundLayer;
    public bool facingRight;
    public bool isGround;

    void Start()
    {
        pkrigbody = GetComponent<Rigidbody2D>();
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

        if (pikeemHealth <= 0)
        {
            Destroy(gameObject, 0.0f);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(new Vector3(0, 180, 0));
        pikeemMovespeed = -pikeemMovespeed;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundcheck.transform.position, circleRadius);
    }

}
