using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingEd : MonoBehaviour
{
    public float speed;
    private float moveInput;
    private bool facingRight;
    private Rigidbody2D rb;

    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        Debug.Log(moveInput);
        if (moveInput != 0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if (facingRight == false && moveInput < 0)
        {
            flipPLayer();
        }
        else if (facingRight == true && moveInput > 0)
        {
            flipPLayer();
        }
    }

    void flipPLayer()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
