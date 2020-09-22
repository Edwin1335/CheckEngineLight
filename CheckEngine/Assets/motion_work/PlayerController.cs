using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private float moveInput;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<RigidBody2D>();
    }

    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal"); //Built in Unity input field
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
