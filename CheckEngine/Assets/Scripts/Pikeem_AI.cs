using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TakeDamage))]
public class Pikeem_AI : MonoBehaviour
{
    public float pikeemMovespeed;
    Rigidbody2D pkrigbody;
    public float circleRadius;
    public GameObject edgeCheck;
    public LayerMask groundLayer;
    public bool facingRight;
    private bool onEdge;
    private bool isGrounded = false;

    private TakeDamage damage;
    private GameObject groundCheck;


    void Start()
    {
        pkrigbody = GetComponent<Rigidbody2D>();
        damage = GetComponent<TakeDamage>();
        groundCheck = this.gameObject.transform.GetChild(3).gameObject;
        damage.enemyName = "Pikeem";
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, circleRadius, groundLayer);

        if (isGrounded)
        {
            pkrigbody.velocity = Vector2.right * pikeemMovespeed * Time.deltaTime;
            onEdge = Physics2D.OverlapCircle(edgeCheck.transform.position, circleRadius, groundLayer);
            if (!onEdge && facingRight)
            {
                Flip();
            }
            else if (!onEdge && !facingRight)
            {
                Flip();
            }
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
        Gizmos.DrawWireSphere(edgeCheck.transform.position, circleRadius);
    }

}
